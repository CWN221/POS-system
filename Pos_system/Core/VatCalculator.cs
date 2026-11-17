namespace Pos_system.Core
{
    /// <summary>
    /// VAT-inclusive pricing: selling price already includes VAT.
    /// VAT = gross × rate / (100 + rate), net = gross − VAT.
    /// </summary>
    public static class VatCalculator
    {
        public static decimal GrossTotal(decimal unitPriceInclusive, int quantity) =>
            unitPriceInclusive * quantity;

        public static decimal ExtractVat(decimal grossInclusive, decimal vatPercentage)
        {
            if (vatPercentage <= 0 || grossInclusive <= 0)
                return 0;

            return Math.Round(grossInclusive * vatPercentage / (100 + vatPercentage), 2, MidpointRounding.AwayFromZero);
        }

        public static decimal ExtractNet(decimal grossInclusive, decimal vatPercentage) =>
            grossInclusive - ExtractVat(grossInclusive, vatPercentage);
    }
}
