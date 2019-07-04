using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Towns
{
    /// <summary>
    /// Логика взаимодействия для UpdateElementWindowxaml.xaml
    /// </summary>
    public partial class UpdateElementWindow : Window
    {
        public UpdateElementWindow(string oldName)
        {
            InitializeComponent();

            tbOldName.Text = oldName;
            tbOldName.IsEnabled = false;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
