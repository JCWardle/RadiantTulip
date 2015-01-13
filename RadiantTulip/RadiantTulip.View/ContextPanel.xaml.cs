﻿using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadiantTulip.View
{
    /// <summary>
    /// Interaction logic for ContextPanel.xaml
    /// </summary>
    public partial class ContextPanel : UserControl
    {
        public readonly static DependencyProperty SelectedPlayersProperty = DependencyProperty.Register("SelectedPlayers",
            typeof(List<Player>),
            typeof(ContextPanel),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectedPlayersChanged));

        [BindableAttribute(true)]
        public List<Player> SelectedPlayers
        {
            get
            {
                return (List<Player>)GetValue(SelectedPlayersProperty);
            }

            set
            {
                SetValue(SelectedPlayersProperty, value);
            }
        }

        private static void SelectedPlayersChanged(DependencyObject control, DependencyPropertyChangedEventArgs args)
        {

            var context = control as ContextPanel;
            context.SelectedPlayers = (List<Player>)args.NewValue;
            context.Players.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        public ContextPanel()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string PlayerList
        {
            get
            {
                var builder = new StringBuilder();
                foreach(var p in SelectedPlayers)
                {
                    builder.AppendLine(p.Name);
                }

                return builder.ToString();
            }
        }
    }
}