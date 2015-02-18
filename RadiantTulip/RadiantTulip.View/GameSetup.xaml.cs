using Microsoft.Practices.Unity;
using RadiantTulip.View.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RadiantTulip.View
{
    public partial class GameSetup : Window
    {
        public GameSetup(IUnityContainer container)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<IGameSetupViewModel>();
        }
    }
}
