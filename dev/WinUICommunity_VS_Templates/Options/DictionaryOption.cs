using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.Options
{
    public class DictionaryOption
    {
        public void ConfigDictionary(Dictionary<string, string> replacementsDictionary, bool hasNavigationView, bool useHomeLandingPage, bool useColorsDic, bool useStylesDic, bool useConvertersDic, bool useFontsDic)
        {
            if (useColorsDic || (hasNavigationView && useHomeLandingPage))
            {
                replacementsDictionary.AddIfNotExists("$AppDicColors$", Environment.NewLine + "<ResourceDictionary Source=\"Themes/Colors.xaml\" />");
            }
            else
            {
                replacementsDictionary.AddIfNotExists("$AppDicColors$", "");
            }

            if (useFontsDic)
            {
                replacementsDictionary.AddIfNotExists("$AppDicFonts$", Environment.NewLine + "<ResourceDictionary Source=\"Themes/Fonts.xaml\" />");
            }
            else
            {
                replacementsDictionary.AddIfNotExists("$AppDicFonts$", "");
            }

            if (useConvertersDic)
            {
                replacementsDictionary.AddIfNotExists("$AppDicConverters$", Environment.NewLine + "<ResourceDictionary Source=\"Themes/Converters.xaml\" />");
            }
            else
            {
                replacementsDictionary.AddIfNotExists("$AppDicConverters$", "");
            }

            if (useStylesDic)
            {
                replacementsDictionary.AddIfNotExists("$AppDicStyles$", Environment.NewLine + "<ResourceDictionary Source=\"Themes/Styles.xaml\" />");
            }
            else
            {
                replacementsDictionary.AddIfNotExists("$AppDicStyles$", "");
            }
        }
    }
}
