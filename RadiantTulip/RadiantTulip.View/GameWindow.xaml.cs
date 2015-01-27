using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace RadiantTulip.View
{
    public partial class GameWindow
    {
        private readonly IGameDrawer _drawer;
        private Table _table;
        private IGameViewModel _view;

        public GameWindow(IUnityContainer container)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<IGameViewModel>();
            _drawer = container.Resolve<IGameDrawer>();

            _table = new Table();
            _table.RowGroups.Add(new TableRowGroup());
            _view = (IGameViewModel)this.DataContext;
            _view.UpdateView = new Action(ReRender);
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
                    var panel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal
                    };
                    panel.Children.Add(new CheckBox()
                    {
                        Margin = new Thickness(0, 5, 0, 0)
                    });
                    panel.Children.Add(new Label() 
                    {
                        Content = p.Name,
                        FontSize = 14
                    });
                    list.Items.Add(panel);
                }

                scrollView.Content = outterPanel;
                outterPanel.Children.Add(list);
                tab.Content = scrollView;
            }
        }

        protected void ReRender()
        {
            _drawer.DrawGame(Game, _table, _view.Game);
        }
    }
}
