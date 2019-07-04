using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private readonly EFContext context = new EFContext();
        public CreateElementWindow()
        {
            InitializeComponent();

            _regions = new ObservableCollection<RegionModel>();
            _townTypes = new ObservableCollection<TownTypeModel>();

            UpdateRegions();
            UpdateTownTypes();

            cmbRegions.ItemsSource = _regions;
            cmbTownTypes.ItemsSource = _townTypes;

            rbTown.IsChecked = true;
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
                context.Regions.AddOrUpdate(new Region { Name = tbNewName.Text });
                context.SaveChanges();

                this.Close();
            }
            if (rbTown.IsChecked == true && tbNewName.Text != "" && cmbRegions.Text != "" && cmbTownTypes.Text != "")
            {
                var ttm = (cmbTownTypes.SelectedValue as TownTypeModel);
                var rm = (cmbRegions.SelectedValue as RegionModel);

                context.Towns.AddOrUpdate(a => a.Id, new Town
                {
                    Name = tbNewName.Text,
                    TownTypeId = ttm.Id,
                    RegionId = rm.Id
                });
                context.SaveChanges();

                this.Close();
            }
        }
    }
}
