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
        public MainWindow()
        {
            InitializeComponent();

            Regions = new ObservableCollection<MVVM.RegionModel>();
            TownTypes = new ObservableCollection<MVVM.TownTypeModel>();
            Towns = new ObservableCollection<MVVM.TownModel>();

            lbRegions.ItemsSource = Regions;
            lbTowns.ItemsSource = Towns;

            Read();
        }
        public void Read()
        {
            string strConn = ConfigurationManager.ConnectionStrings["connectionName"].ConnectionString;
            using (SqlConnection sqlConnection = new SqlConnection(strConn))
            {
                sqlConnection.Open();

                string sqlQuery = "Select * From tblRegions";
                SqlCommand comm = new SqlCommand(sqlQuery, sqlConnection);
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    Regions.Add(new MVVM.RegionModel { Id = id, Name = name });
                }
                reader.Close();

                sqlQuery = "Select * From tblTownTypes";
                comm = new SqlCommand(sqlQuery, sqlConnection);
                reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    TownTypes.Add(new MVVM.TownTypeModel { Id = id, Name = name });
                }
                reader.Close();

                sqlQuery = "Select * From tblTowns";
                comm = new SqlCommand(sqlQuery, sqlConnection);
                reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = reader["Name"].ToString();
                    int regionId = Convert.ToInt32(reader["RegionId"]);
                    int townTypeId = Convert.ToInt32(reader["TownTypeId"]);
                    Towns.Add(new MVVM.TownModel { Id = id, Name = name, RegionId = regionId, TownTypeId = townTypeId });
                }
                sqlConnection.Close();
            }
        }
    }
}
