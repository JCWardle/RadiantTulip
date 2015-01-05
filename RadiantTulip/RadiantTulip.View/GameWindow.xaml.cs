using Microsoft.Practices.Unity;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;

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
            this.GameControl.Drawer = _container.Resolve<IGameDrawer>();
        }
    }
}
