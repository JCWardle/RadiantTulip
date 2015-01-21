using RadiantTulip.Model;
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
using PlayerSize = RadiantTulip.Model.Size;

namespace RadiantTulip.View
{
    /// <summary>
    /// Interaction logic for ContextPanel.xaml
    /// </summary>
    public partial class ContextPanel : UserControl
    {
        private enum State { None, Multiple, Single };
        private State _state = State.None;

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
            
            if(context.SelectedPlayers.Count == 1)
            {
                context.SinglePlayerContext();
            }
            else if (context.SelectedPlayers.Count > 1)
            {
                context.MutliplayerContext();
            }
            else
            {
                context.MultiPlayer.Visibility = Visibility.Collapsed;
                context.SinglePlayer.Visibility = Visibility.Collapsed;
                context.Tools.Visibility = Visibility.Hidden;
            }

        }

        private void MutliplayerContext()
        {
            _state = State.Multiple;
            MultiPlayer.Visibility = Visibility.Visible;
            SinglePlayer.Visibility = Visibility.Collapsed;
            Tools.Visibility = Visibility.Visible;

            SizeSelector.SelectedItem = null;
        }

        private void SinglePlayerContext()
        {
            _state = State.Single;
            var player = SelectedPlayers.First();
            MultiPlayer.Visibility = Visibility.Collapsed;
            SinglePlayer.Visibility = Visibility.Visible;
            Tools.Visibility = Visibility.Visible;

            PlayerName.Content = player.Name;
            SizeSelector.SelectedItem = player.Size;
            ColourSelector.SelectedColor = player.Colour;
        }

        public ContextPanel()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            SizeSelector.ItemsSource = Enum.GetValues(typeof(PlayerSize));
        }
    }
}
