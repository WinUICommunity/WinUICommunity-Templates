using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.Options
{
    public class DictionaryOption
    {
        public void ConfigDictionary(Dictionary<string, string> replacementsDictionary, bool hasNavigationView, bool useHomeLandingPage, bool useColorsDic, bool useStylesDic, bool useConvertersDic, bool useFontsDic)
        {
            if (!hasNavigationView)
            {
                if (useColorsDic)
                {
                    replacementsDictionary.Add("$AppDicColors$", "<ResourceDictionary Source=\"Themes/Colors.xaml\" />");
                }
                else
                {
                    replacementsDictionary.Add("$AppDicColors$", "");
                }
            }
            
            if (useHomeLandingPage && hasNavigationView)
            {
                replacementsDictionary.Add("$AppDicColors$", "<ResourceDictionary Source=\"Themes/Colors.xaml\" />");
            }
            else
            {
                replacementsDictionary.Add("$AppDicColors$", "");
            }

            if (useFontsDic)
            {
                replacementsDictionary.Add("$AppDicFonts$", "<ResourceDictionary Source=\"Themes/Fonts.xaml\" />");
            }
            else
            {
                replacementsDictionary.Add("$AppDicFonts$", "");
            }

            if (useConvertersDic)
            {
                replacementsDictionary.Add("$AppDicConverters$", "<ResourceDictionary Source=\"Themes/Converters.xaml\" />");
            }
            else
            {
                replacementsDictionary.Add("$AppDicConverters$", "");
            }

            if (useStylesDic)
            {
                replacementsDictionary.Add("$AppDicStyles$", "<ResourceDictionary Source=\"Themes/Styles.xaml\" />");
            }
            else
            {
                replacementsDictionary.Add("$AppDicStyles$", "");
            }
        }
    }
}
