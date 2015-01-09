using System;
using RadiantTulip.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace RadiantTulip.View
{
    /// <summary>
    /// Interaction logic for TeamList.xaml
    /// </summary>
    public partial class TeamList : UserControl
    {
        public readonly static DependencyProperty PlayersProperty = DependencyProperty.Register("Players",
            typeof(List<Player>),
            typeof(TeamList),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [BindableAttribute(true)]
        public List<Player> Players
        {
            get
            {
                return (List<Player>)GetValue(PlayersProperty);
            }

            set
            {
                SetValue(PlayersProperty, value);
            }
        }

        public static DependencyProperty SelectedPlayersProperty = DependencyProperty.Register("SelectedPlayers",
            typeof(List<Player>),
            typeof(TeamList),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SelectPlayer));

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
        
        public TeamList()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        private static void SelectPlayer(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            return;
        }
    }
}
