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
        public ObservableCollection<MVVM.RegionModel> Regions { get; set; }
        public ObservableCollection<MVVM.TownTypeModel> TownTypes { get; set; }
        public ObservableCollection<MVVM.TownModel> Towns { get; set; }
        private readonly EFContext context;
        public MainWindow()
        {
            InitializeComponent();

            Regions = new ObservableCollection<MVVM.RegionModel>();
            TownTypes = new ObservableCollection<MVVM.TownTypeModel>();
            Towns = new ObservableCollection<MVVM.TownModel>();
            context = new EFContext();
            lbRegions.ItemsSource = Regions;
            lbTowns.ItemsSource = Towns;

            UpdateRegions();
        }

        private void UpdateRegions()
        {
            var list = context.Regions.Select(r => new MVVM.RegionModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            Regions.Clear();
            Regions.AddRange(list);
        }

        private void UpdateTowns(int? typeId)
        {
            var query = context.Towns.AsQueryable();
            if (typeId != null)
            {
                query = query.Where(r => r.RegionId == typeId);
            }
            var list = query.Select(r => new MVVM.TownModel
            {
                Id = r.Id,
                Name = r.Name,
                RegionId = r.RegionId,
                TownTypeId = r.TownTypeId,
            }).ToList()
            .OrderBy(r => r.TownTypeId)
            .ThenBy(r => r.Name);

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
            //////
            Towns.Clear();
            Towns.AddRange(list);
        }
        private void LbRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = (sender as ListBox).SelectedItem as MVVM.RegionModel;
            UpdateTowns(data.Id);
        }
    }
}
