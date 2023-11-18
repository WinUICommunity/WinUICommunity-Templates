﻿using System.Windows;

namespace iNKORE.UI.WPF.Modern
{
    public class CommandBarFlyoutCommandBarTemplateSettingsProxy : Freezable
    {
        public static readonly DependencyProperty FlyoutTemplateSettingsProperty =
            DependencyProperty.Register(
                nameof(FlyoutTemplateSettings),
                typeof(CommandBarFlyoutCommandBarTemplateSettings),
                typeof(CommandBarFlyoutCommandBarTemplateSettingsProxy),
                null);

        public CommandBarFlyoutCommandBarTemplateSettings FlyoutTemplateSettings
        {
            get => (CommandBarFlyoutCommandBarTemplateSettings)GetValue(FlyoutTemplateSettingsProperty);
            set => SetValue(FlyoutTemplateSettingsProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CommandBarFlyoutCommandBarTemplateSettingsProxy();
        }
    }
}
