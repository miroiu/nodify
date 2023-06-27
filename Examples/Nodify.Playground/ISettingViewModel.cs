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
        
        /// <value>
        /// Property <c>Description</c> represents the content within the tooltip.
        /// </value>
        string? Description { get; }
        object? Value { get; set; }

        SettingsType Type { get;}
    }
}