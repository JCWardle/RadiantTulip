using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using RadiantTulip.Model;
using RadiantTulip.Model.Converter;
using RadiantTulip.Model.Input;
using RadiantTulip.View.Drawing;
using RadiantTulip.View.ViewModels;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using RadiantTulip.View.Drawing.VisualAffects;
using log4net;
using RadiantTulip.View.Settings;

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
            Container.RegisterType(typeof (IPlayerDrawer), typeof (PlayerDrawer));
            Container.RegisterType(typeof(IAffectFactory), typeof(AffectFactory));
            Container.RegisterType(typeof(IGroundDrawerFactory), typeof(GroundDrawerFactory));
            Container.RegisterType(typeof(IBallDrawer), typeof(BallDrawer));
            Container.RegisterType(typeof(ISizeSettings), typeof(SizeSettings));

            //Views
            Container.RegisterType(typeof (IGameViewModel), typeof (GameViewModel));
            Container.RegisterType(typeof(IGameSetupViewModel), typeof(GameSetupViewModel));
            Container.RegisterType(typeof(IGlobalExceptionHandler), typeof(GlobalExceptionHandler));
            Container.RegisterType(typeof(ILog), new InjectionFactory(x => LogManager.GetLogger("Radiant Tulip")));
        }

        protected override DependencyObject CreateShell()
        {
            var setupWindow = Container.Resolve<GameSetup>();
            setupWindow.Show();
            return setupWindow;
        }
    }
}
