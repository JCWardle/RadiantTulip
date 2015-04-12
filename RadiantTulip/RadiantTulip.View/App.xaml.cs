﻿using System.Windows;

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

            var exceptionHandler = bootStrapper.Container.Resolve(typeof(IGlobalExceptionHandler), null, null) as IGlobalExceptionHandler;

            this.DispatcherUnhandledException += exceptionHandler.HandleException;
        }
    }
}
