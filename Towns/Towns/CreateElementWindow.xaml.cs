using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Towns.Entity;
using Towns.MVVM;

namespace Towns
{
    /// <summary>
    /// Interaction logic for CreateElementWindow.xaml
    /// </summary>
    public partial class CreateElementWindow : Window
    {
        public ObservableCollection<RegionModel> _regions;
        public ObservableCollection<TownTypeModel> _townTypes;
        private readonly EFContext context;
        public CreateElementWindow()
        {
            InitializeComponent();

            _regions = new ObservableCollection<RegionModel>();
            _townTypes = new ObservableCollection<TownTypeModel>();
            context = new EFContext();

            UpdateRegions();
            UpdateTownTypes();

            cmbRegions.ItemsSource = _regions;
            cmbTownTypes.ItemsSource = _townTypes;
        }


        private void UpdateRegions()
        {
            var list = context.Regions.Select(r => new RegionModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            _regions.Clear();
            _regions.AddRange(list);
        }
        private void UpdateTownTypes()
        {
            var list = context.TownTypes.Select(r => new TownTypeModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            _townTypes.Clear();
            _townTypes.AddRange(list);
        }

        private void CmbRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbNewName.IsEnabled = true;
        }


        private void RbRegion_Checked(object sender, RoutedEventArgs e)
        {
            cmbRegions.IsEnabled = false;
            cmbRegions.Text = "";
            cmbTownTypes.IsEnabled = false;
            cmbTownTypes.Text = "";
            tbNewName.IsEnabled = true;
        }

        private void RbTown_Checked(object sender, RoutedEventArgs e)
        {
            cmbRegions.IsEnabled = true;
            cmbTownTypes.IsEnabled = true;
            tbNewName.IsEnabled = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (rbRegion.IsChecked == true && tbNewName.Text != "")
            {
                context.Regions.Add(new Region { Name = tbNewName.Text });
                context.SaveChanges();

                this.Close();
            }
            if (rbTown.IsChecked == true && tbNewName.Text != "" && cmbRegions.Text != "" && cmbTownTypes.Text != "")
            {
                var ttm = (cmbTownTypes.SelectedValue as TownTypeModel);
                var rm = (cmbRegions.SelectedValue as RegionModel);

                context.Towns.Add(new Town { Name = tbNewName.Text, RegionId = rm.Id, TownTypeId = ttm.Id });
                context.SaveChanges();

                this.Close();
            }
        }
    }
}
