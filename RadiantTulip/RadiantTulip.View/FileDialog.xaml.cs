using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RadiantTulip.View
{
    public partial class FileDialog : UserControl
    {
        public static DependencyProperty SelectedFileProperty = DependencyProperty.Register("SelectedFile",
            typeof(string),
            typeof(FileDialog),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [BindableAttribute(true)]
        public string SelectedFile
        {
            get
            {
                return (string)GetValue(SelectedFileProperty);
            }

            set
            {
                SetValue(SelectedFileProperty, value);
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            var result = dialog.ShowDialog();

            if(result != null && result.Value)
            {
                SelectedFile = dialog.FileName;
            }
        }
        
        public FileDialog()
        {
            InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }
    }
}
