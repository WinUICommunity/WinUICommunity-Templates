using System;
using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.Options
{
    public class AppCenterOption
    {
        public void ConfigAppCenter(bool useAppCenter, Dictionary<string, string> replacementsDictionary)
        {
            if (useAppCenter)
            {
                replacementsDictionary.Add("$AppCenterKey$", Environment.NewLine + Environment.NewLine + """public const string AppCenterKey = "APPCENTER_KEY";""");
                replacementsDictionary.Add("$AppCenter$", Environment.NewLine + Environment.NewLine + """Microsoft.AppCenter.AppCenter.Start(Constants.AppCenterKey, typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Microsoft.AppCenter.Crashes.Crashes));""");
            }
            else
            {
                replacementsDictionary.Add("$AppCenterKey$", "");
                replacementsDictionary.Add("$AppCenter$", "");
            }
        }
    }
}
