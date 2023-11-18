namespace iNKORE.UI.WPF.Modern
{
    internal class DefaultNumberRounder
    {
        public double RoundDouble(double value)
        {
            return double.Parse(value.ToString("G12"));
        }
    }
}
