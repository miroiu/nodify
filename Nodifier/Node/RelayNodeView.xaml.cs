﻿using Nodifier.XAML;
using System.Windows.Controls;

namespace Nodifier.Views
{
    /// <summary>
    /// Interaction logic for RelayNodeView.xaml
    /// </summary>
    public partial class RelayNodeView : UserControl, IViewFor<IRelayNode>
    {
        public RelayNodeView()
        {
            InitializeComponent();
        }
    }
}
