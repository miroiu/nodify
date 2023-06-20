using System;
using System.ComponentModel;

namespace Nodify.Playground
{
    public enum SettingsType
    {
        Boolean,
        Number,
        Option, //For ComboBox
        Point
    }

    public interface ISettingViewModel
    {
        string Name { get; }
        string? Description { get; } // This is the tooltip
        object? Value { get; set; }

        public SettingsType Type { get; set; }
    }
}