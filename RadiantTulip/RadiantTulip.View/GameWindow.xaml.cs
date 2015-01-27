using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
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

        protected void ReRender()
        {
            _drawer.DrawGame(Game, _table, _view.Game);
        }
    }
}
