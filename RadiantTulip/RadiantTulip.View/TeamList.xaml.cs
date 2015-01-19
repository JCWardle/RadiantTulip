using System;
using RadiantTulip.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Collections;
using System.Linq;

namespace RadiantTulip.View
{
    public partial class TeamList : UserControl
    {
        private DelegateCommand<object> _select;

        public ICommand SelectCommand
        {
            get { return _select ?? (_select = new DelegateCommand<object>(PlayerSelected)); }
        }

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
            var context = control as TeamList;
            context.SelectedPlayers = (List<Player>)args.NewValue;
            context.GetBindingExpression(TeamList.SelectedPlayersProperty).UpdateTarget();

            foreach(var p in context.SelectedPlayers)
            {
                context.PlayersList.SelectedItems.Add(p);
            }
        }
        
        public TeamList()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        private void PlayerSelected(object items)
        {
            var players = ((IList)items).Cast<Player>();
            SelectedPlayers.Clear();
            var list = new List<Player>();
            list.AddRange(players);
            SelectedPlayers = list;
        }
    }
}
