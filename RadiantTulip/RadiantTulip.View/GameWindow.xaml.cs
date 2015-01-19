using Microsoft.Practices.Unity;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace RadiantTulip.View
{
    public partial class GameWindow
    {
        private readonly IUnityContainer _container;

        public GameWindow(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
            this.DataContext = _container.Resolve<IGameViewModel>();
            this.GameDisplay.Drawer = _container.Resolve<IGameDrawer>();
        }
    }
}
