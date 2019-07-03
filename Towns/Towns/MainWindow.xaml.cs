using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Towns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<MVVM.RegionModel> _regions;
        public ObservableCollection<MVVM.TownTypeModel> _townTypes;
        public ObservableCollection<MVVM.TownModel> _towns;
        private readonly EFContext context;
        public MainWindow()
        {
            InitializeComponent();

            _regions = new ObservableCollection<MVVM.RegionModel>();
            _townTypes = new ObservableCollection<MVVM.TownTypeModel>();
            _towns = new ObservableCollection<MVVM.TownModel>();
            context = new EFContext();
            lbRegions.ItemsSource = _regions;
            lbTowns.ItemsSource = _towns;

            UpdateRegions();
        }

        private void UpdateRegions()
        {
            var list = context.Regions.Select(r => new MVVM.RegionModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            _regions.Clear();
            _regions.AddRange(list);
        }

        private void UpdateTowns(int? typeId)
        {
            if (typeId != null)
            {
                var query = context.Towns.AsQueryable()
                .Where(r => r.RegionId == typeId);

                var list = query
                    .OrderBy(r => r.TownTypeId)
                    .ThenBy(r => r.Name)
                    .Select(r => new MVVM.TownModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        RegionId = r.RegionId,
                        TownTypeId = r.TownTypeId,
                    }).ToList();

                //додаємо тип населеного пункту перед імям
                foreach (var item in list)
                {
                    switch (item.TownTypeId)
                    {
                        case 1: item.Name = item.Name.ToUpper(); break;
                        case 2: item.Name = "м." + item.Name; break;
                        case 3: item.Name = "c." + item.Name; break;
                    }
                }
                ////
                _towns.Clear();
                _towns.AddRange(list);
            }
        }
        private void LbRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var region = (sender as ListBox).SelectedItem as MVVM.RegionModel;
            UpdateTowns(region?.Id);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateElementWindow createElementWindow = new CreateElementWindow();
            createElementWindow.ShowDialog();

            UpdateRegions();
        }
    }
}
