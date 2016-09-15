﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.FilterBuilder
{
    using System.Windows;
    using System.Windows.Media;

    internal static class ColorExtensions
    {
        #region Constants
        private const string AccentBrush = "AccentBrush";
        private const string AccentColor = "AccentColor";
        private const string HighlightBrush = "HighlightBrush";
        private const string HighlightColor = "HighlightColor";
        private static ResourceDictionary _accentColorResourceDictionary;
        #endregion

        #region Methods 
        public static void CreateAccentColorResourceDictionary(this Color color, string controlName)
        {
            var accentColor = Application.Current.TryFindResource(controlName.GetAccentBrushName()) as SolidColorBrush;

            if (accentColor != null)
            {
                return;
            }

            if (_accentColorResourceDictionary != null)
            {
                _accentColorResourceDictionary.AddResources(color, controlName);
            }

            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.AddResources(color, controlName);

            var application = Application.Current;
            var applicationResources = application.Resources;
            applicationResources.MergedDictionaries.Insert(0, resourceDictionary);

            _accentColorResourceDictionary = resourceDictionary;
        }

        private static void AddResources(this ResourceDictionary resourceDictionary, Color color, string name)
        {
            resourceDictionary.Add(name.GetHighlightColorName(), color.CalculateHighlightColor());
            resourceDictionary.Add(name.GetAccentColorName(), color);

            resourceDictionary.Add(name.GetHighlightBrushName(), new SolidColorBrush((Color) resourceDictionary[name.GetHighlightColorName()]));
            resourceDictionary.Add(name.GetAccentBrushName(), new SolidColorBrush((Color) resourceDictionary[name.GetAccentColorName()]));
        }

        private static string GetAccentBrushName(this string controlName)
        {
            return controlName + AccentBrush;
        }

        private static string GetAccentColorName(this string controlName)
        {
            return controlName + AccentColor;
        }

        private static string GetHighlightBrushName(this string controlName)
        {
            return controlName + HighlightBrush;
        }

        private static string GetHighlightColorName(this string controlName)
        {
            return controlName + HighlightColor;
        }

        private static Color CalculateHighlightColor(this Color color)
        {
            return Color.FromArgb(51, color.R, color.G, color.B);
        }
        #endregion
    }
}