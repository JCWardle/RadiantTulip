using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.View.Game;
using System.Windows;

namespace RadiantTulip.View
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            //Model
            Container.RegisterType(typeof(ISpatialReader), typeof(ExcelReader));
            Container.RegisterType(typeof(ICoordinateConverter), typeof(GPSConverter));
            Container.RegisterType(typeof(IGameCreator), typeof(GameCreator));

            //Drawing
            Container.RegisterType(typeof (IGameDrawer), typeof (GameDrawer));
            Container.RegisterType(typeof (IGroundDrawer), typeof (GroundDrawer));
            Container.RegisterType(typeof (IPlayerDrawer), typeof (PlayerDrawer));
        }

        protected override DependencyObject CreateShell()
        {
            var gameWindow = Container.Resolve<GameWindow>();
            gameWindow.Show();
            return gameWindow;
        }
    }
}
