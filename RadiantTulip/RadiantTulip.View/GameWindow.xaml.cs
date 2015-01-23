using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RadiantTulip.View
{
    public partial class GameWindow
    {
        private readonly IGameDrawer _drawer;

        public GameWindow(IUnityContainer container)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<IGameViewModel>();
            _drawer = container.Resolve<IGameDrawer>();
        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs args)
        {
            foreach(var t in PlayerTabs.Items)
            {
                var team = (Team)t;
                var tab = (TabItem)PlayerTabs.ItemContainerGenerator.ContainerFromItem(t);
                var scrollView = new ScrollViewer();
                var outterPanel = new StackPanel( ){ Orientation = Orientation.Vertical };
                var list = new ListBox();

                foreach(var p in team.Players)
                {
                    list.Items.Add(new Label() { Content = p.Name });
                }

                scrollView.Content = outterPanel;
                outterPanel.Children.Add(list);
                tab.Content = scrollView;
            }
        }
    }
}
