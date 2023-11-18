namespace iNKORE.UI.WPF.Modern
{
    public interface INumberBoxNumberFormatter
    {
        string FormatDouble(double value);
        double? ParseDouble(string text);
    }
}
