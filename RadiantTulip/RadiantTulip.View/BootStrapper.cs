using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.View.Game;
using RadiantTulip.View.ViewModels;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using RadiantTulip.View.Game.VisualAffects;

namespace RadiantTulip.View
{
    public class BootStrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //Model
            Container.RegisterType(typeof(ISpatialReader), typeof(CsvVisualReader));
            Container.RegisterType(typeof(ICoordinateConverter), typeof(VisualDataConverter));
            Container.RegisterType(typeof(IGameCreator), typeof(GameCreator));
            Container.RegisterType(typeof (IModelUpdater), typeof (ModelUpdater));
            Container.RegisterType(typeof(IGroundReader), typeof(JsonGroundReader));
            Container.RegisterType(typeof(IGameCreatorFactory), typeof(GameCreatorFactory));

            //Drawing
            Container.RegisterType(typeof (IGameDrawer), typeof (GameDrawer));
            Container.RegisterType(typeof (GroundDrawer), typeof (AFLGroundDrawer));
            Container.RegisterType(typeof (IPlayerDrawer), typeof (PlayerDrawer));
            Container.RegisterType(typeof(IAffectFactory), typeof(AffectFactory));
            Container.RegisterType(typeof(IGroundDrawerFactory), typeof(GroundDrawerFactory));

            //Views
            Container.RegisterType(typeof (IGameViewModel), typeof (GameViewModel));
            Container.RegisterType(typeof(IGameSetupViewModel), typeof(GameSetupViewModel));
        }

        protected override DependencyObject CreateShell()
        {
            var setupWindow = Container.Resolve<GameSetup>();
            setupWindow.Show();
            return setupWindow;
        }
    }
}
