using Dedge;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests;
public class IdentifyTests
{
    [TestCase(null, ExpectedResult = new CardType[] { })]
    [TestCase("", ExpectedResult = new CardType[] { })]
    [TestCase("a", ExpectedResult = new CardType[] { })]
    [TestCase("1", ExpectedResult = new CardType[] { })]
    [TestCase("4", ExpectedResult = new CardType[] { })]
    public IEnumerable<CardType> ShouldIdentifyAsUnknownFalsyCase(string cardNumber) => Cardidy.Identify(cardNumber);

    [TestCase("4169773331987017", ExpectedResult = CardType.Visa)]
    [TestCase("4658958254583145", ExpectedResult = CardType.Visa)]
    [TestCase("4771320594031", ExpectedResult = CardType.Visa)]
    public CardType ShouldIdentifyAsVisa(string cardNumber) => Cardidy.Identify(cardNumber).First();

    [TestCase("4026-773331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("417500-3331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("4508-773331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("4844-773331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("4913-773331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("4917-773331987017", ExpectedResult = new[] { CardType.VisaElectron, CardType.Visa })]
    [TestCase("4026-320594033", ExpectedResult = new[] { CardType.Visa })]
    [TestCase("417500-3331917", ExpectedResult = new[] { CardType.Visa })]
    public IEnumerable<CardType> ShouldIdentifyAsVisaElectron(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true);

    [TestCase("5110710000901089", ExpectedResult = CardType.MasterCard)]
    [TestCase("5289675573349651", ExpectedResult = CardType.MasterCard)]
    [TestCase("5389675555733434", ExpectedResult = CardType.MasterCard)]
    [TestCase("5489675573349651", ExpectedResult = CardType.MasterCard)]
    [TestCase("5582128534772839", ExpectedResult = CardType.MasterCard)]
    [TestCase("2221-774331987017", ExpectedResult = CardType.MasterCard)]
    [TestCase("2330-773331987017", ExpectedResult = CardType.MasterCard)]
    [TestCase("2631-775331987017", ExpectedResult = CardType.MasterCard)]
    [TestCase("2720-773331987017", ExpectedResult = CardType.MasterCard)]
    public CardType ShouldIdentifyAsMasterCard(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true).First();

    [TestCase("5018-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("5020-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("5038-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("5893-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("6304-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("6761-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("6762-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("6763-710000901089", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-71000090", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-710000901", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-7100009012", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-71000090123", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-710000901234", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-7100009012345", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-71000090123456", ExpectedResult = CardType.Maestro)]
    [TestCase("5018-710000901234567", ExpectedResult = CardType.Maestro)]
    public CardType ShouldIdentifyAsMaestro(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true).First();

    [TestCase("676770-0000901089", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-0000901089", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-000090", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-0000901", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-00009012", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-000090123", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-0000901234", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-00009012345", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-000090123456", ExpectedResult = CardType.MaestroUk)]
    [TestCase("676774-0000901234567", ExpectedResult = CardType.MaestroUk)]
    public CardType ShouldIdentifyAsMaestroUk(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true).First();

    // note that: 6759 is shared by both MaestroUK & Maestro ¯\_(ツ)_/¯
    [TestCase("6759710000901011", ExpectedResult = new[] { CardType.MaestroUk, CardType.Maestro })]
    [TestCase("6759710000901086", ExpectedResult = new[] { CardType.MaestroUk, CardType.Maestro })]
    public IEnumerable<CardType> ShouldIdentifyAsMaestros(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: true);

    [TestCase("340000000000000", ExpectedResult = CardType.AmericanExpress)]
    [TestCase("341071000090108", ExpectedResult = CardType.AmericanExpress)]
    [TestCase("378967557334965", ExpectedResult = CardType.AmericanExpress)]
    public CardType ShouldIdentifyAsAmericanExpress(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false).First();

    [TestCase("100000000000000", ExpectedResult = CardType.Uatp)]
    [TestCase("141071000090108", ExpectedResult = CardType.Uatp)]
    public CardType ShouldIdentifyAsUatp(string cardNumber) => Cardidy.Identify(cardNumber).First();

    [TestCase("2200200000000000", ExpectedResult = CardType.Mir)]
    [TestCase("2201200000000000", ExpectedResult = CardType.Mir)]
    [TestCase("2202200000000000", ExpectedResult = CardType.Mir)]
    [TestCase("2203200000000000", ExpectedResult = CardType.Mir)]
    [TestCase("2204200000000000", ExpectedResult = CardType.Mir)]
    public CardType ShouldIdentifyAsMir(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false).First();

    [TestCase("6011-773331987017", ExpectedResult = CardType.Discover)]
    [TestCase("65-18958254583145", ExpectedResult = CardType.Discover)]
    [TestCase("622126-1230594033", ExpectedResult = CardType.Discover)]
    [TestCase("622225-1230594033", ExpectedResult = CardType.Discover)]
    [TestCase("622925-1230594033", ExpectedResult = CardType.Discover)]
    [TestCase("644-4441230594033", ExpectedResult = CardType.Discover)]
    [TestCase("646-4441230594033", ExpectedResult = CardType.Discover)]
    [TestCase("649-4441230594033", ExpectedResult = CardType.Discover)]
    public CardType ShouldIdentifyAsDiscover(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true).First();

    [TestCase("5060990000000000", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("5061230000000000", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("5061980000000000", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("6500020000000001", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("6500100000000001", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("6500270000000000", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("6500270000000000000", true, ExpectedResult = new[] { CardType.Verve })]
    [TestCase("6500020000000000", false, ExpectedResult = new[] { CardType.Verve, CardType.Discover })]
    [TestCase("6500100000000000", false, ExpectedResult = new[] { CardType.Verve, CardType.Discover })]
    [TestCase("6500270000000000", false, ExpectedResult = new[] { CardType.Verve, CardType.Discover })]
    [TestCase("65002700000000000", false, ExpectedResult = new[] { CardType.Discover })]
    [TestCase("650027000000000000", false, ExpectedResult = new[] { CardType.Discover })]
    [TestCase("6500270000000000000", false, ExpectedResult = new[] { CardType.Verve, CardType.Discover })]
    public IEnumerable<CardType> ShouldIdentifyAsVerve(string cardNumber, bool useCheck) => Cardidy.Identify(cardNumber, useCheck: useCheck).ToArray();

    [TestCase("5610553000273614", ExpectedResult = CardType.BankCard)]
    [TestCase("5602213166347852", ExpectedResult = CardType.BankCard)]
    [TestCase("5602253004948429", ExpectedResult = CardType.BankCard)]
    public CardType ShouldIdentifyAsBankCard(string cardNumber) => Cardidy.Identify(cardNumber, useCheck: false, ignoreNoise: true).First();
}