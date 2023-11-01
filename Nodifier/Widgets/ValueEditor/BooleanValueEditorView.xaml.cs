﻿using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for BooleanValueEditorView.xaml
    /// </summary>
    public partial class BooleanValueEditorView : UserControl, IViewFor<ValueEditor<bool>>
    {
        public BooleanValueEditorView()
        {
            InitializeComponent();
        }
    }
}
