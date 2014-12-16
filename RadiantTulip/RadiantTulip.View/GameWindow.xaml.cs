using System.Windows;
using Microsoft.Practices.Unity;
using RadiantTulip.View.ViewModels;

namespace RadiantTulip.View
{
    public partial class GameWindow : Window
    {
        private readonly IUnityContainer _container;

        public GameWindow(IUnityContainer container)
        {
            UnityHelper.SetContainer(this, container);
            InitializeComponent();
            _container = container;
            this.DataContext = _container.Resolve<IGameViewModel>();
        }
    }
}
