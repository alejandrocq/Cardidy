﻿namespace Dedge.Model;

// Based on https://en.wikipedia.org/wiki/Payment_card_number
internal record AmericanExpress : ALuhnCard
{
    public AmericanExpress() : base(CardType.AmericanExpress,  new[] { 34, 37 }, Fifteen) { }
}

internal record Uatp : ACard
{
    public Uatp() : base(CardType.Uatp, 1, Fifteen) { }
}

internal record Mir : ALuhnCard
{
    public Mir() : base(CardType.Mir, new[] { new PaddedRange(2200, 2204) }, From16To19) { }
}

internal record VisaElectron : ALuhnCard
{
    public VisaElectron() : base(CardType.VisaElectron, new[] { 4026, 417500, 4508, 4844, 4913, 4917 }, Sixteen) { }
}

internal record Visa : ALuhnCard
{
    public Visa() : base(CardType.Visa, 4, new[] { 13, 16 }) { }
}

internal record UnionPay : ALuhnCard
{
    public UnionPay() : base(CardType.VisaElectron, 31, Sixteen) { }
}

internal record Verve : ACard
{
    public Verve() : base(CardType.Verve, new[] { new PaddedRange(506099, 506198), new PaddedRange(650002, 650027) }, new[] { 16, 19 })
    { }
}

internal record Discover : ALuhnCard
{
    public Discover() : base(CardType.Discover, new[] {
        new PaddedRange(6011), new PaddedRange(622126, 622925), new PaddedRange(644, 649), new PaddedRange(65)
    }, From16To19)
    { }
}

internal record MaestroUk : ALuhnCard
{
    public MaestroUk() : base(CardType.MaestroUk, new[] { 6759, 676770, 676774 }, From12To19) { }
}

internal record Maestro : ALuhnCard
{
    public Maestro() : base(CardType.Maestro, new[] { 5018, 5020, 5038, 5893, 6304, 6759, 6761, 6762, 6763 }, From12To19) { }
}

internal record MasterCard : ALuhnCard
{
    public MasterCard() : base(CardType.MasterCard, new[] { new PaddedRange(51, 55), new PaddedRange(2221, 2720) }, Sixteen) { }
}

internal record Jcb : ALuhnCard
{
    public Jcb() : base(CardType.Jcb, new[] { new PaddedRange(3528, 3589) }, From16To19) { }
}

internal record BankCard : ALuhnCard
{
    public BankCard() : base(CardType.BankCard, new[] { new PaddedRange(5610), new PaddedRange(560221, 560225) }, Sixteen) { }
}
