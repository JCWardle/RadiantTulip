using System.Windows;
using Microsoft.Practices.Unity;
using System;

namespace RadiantTulip.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootStrapper = new BootStrapper();
            bootStrapper.Run();

            var exceptionHandler = bootStrapper.Container.Resolve<IGlobalExceptionHandler>();

            this.DispatcherUnhandledException += exceptionHandler.HandleException;
            throw new Exception();
        }
    }
}
