﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Controls;
using Towns.MVVM;
using Towns.Entity;

namespace Towns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<RegionModel> _regions;
        public ObservableCollection<TownTypeModel> _townTypes;
        public ObservableCollection<TownModel> _towns;
        private readonly EFContext context;

        private readonly string strRegion = "Область";
        private readonly string strTown = "Міста";

        public MainWindow()
        {
            InitializeComponent();

            _regions = new ObservableCollection<RegionModel>();
            _townTypes = new ObservableCollection<TownTypeModel>();
            _towns = new ObservableCollection<TownModel>();
            context = new EFContext();
            lbRegions.ItemsSource = _regions;
            lbTowns.ItemsSource = _towns;

            cmbSelectType.Items.Add(strRegion);
            cmbSelectType.Items.Add(strTown);

            tbSearch.IsEnabled = false;

            UpdateRegions();
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

        private void UpdateTowns(int? typeId)
        {
            if (typeId != null)
            {
                var query = context.Towns.AsQueryable()
                .Where(r => r.RegionId == typeId);

                var list = query
                    .OrderBy(r => r.TownTypeId)
                    .ThenBy(r => r.Name)
                    .Select(r => new TownModel
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
            var region = (sender as ListBox).SelectedItem as RegionModel;
            UpdateTowns(region?.Id);
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateElementWindow createElementWindow = new CreateElementWindow();
            createElementWindow.ShowDialog();

            UpdateRegions();
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbRegions.SelectedIndex != -1)
            {
                if (lbTowns.SelectedIndex == -1)
                {
                    var r = lbRegions.SelectedValue as RegionModel;

                    string msg = "Видалити регіон " + r.Name + "?";

                    if (MessageBox.Show(msg, "Видалення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Region deletedRegion = context.Regions
                            .Where(o => o.Id == r.Id)
                            .FirstOrDefault();

                        context.Regions.Remove(deletedRegion);
                        context.SaveChanges();
                        UpdateRegions();
                    }
                }

                if (lbTowns.SelectedIndex != -1)
                {
                    var t = lbTowns.SelectedValue as TownModel;

                    string msg = "Видалити місто " + t.Name + "?";

                    if (MessageBox.Show(msg, "Видалення", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Town deletedTown = context.Towns
                            .Where(o => o.Id == t.Id)
                            .FirstOrDefault();

                        context.Towns.Remove(deletedTown);
                        context.SaveChanges();
                        UpdateTowns(t.RegionId);
                    }
                }
            }
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbRegions.SelectedIndex != -1)
            {
                if (lbTowns.SelectedIndex == -1)
                {
                    var r = lbRegions.SelectedValue as RegionModel;
                    UpdateElementWindow updateElementWindow = new UpdateElementWindow(r.Name);
                    updateElementWindow.ShowDialog();

                    string newName = updateElementWindow.tbNewName.Text;
                    if (newName != "")
                    {
                        Region updatedRegion = context.Regions
                            .Where(u => u.Id == r.Id)
                            .FirstOrDefault();

                        updatedRegion.Name = newName;
                        context.SaveChanges();

                        UpdateRegions();
                    }
                }

                if (lbTowns.SelectedIndex != -1)
                {
                    var t = lbTowns.SelectedValue as TownModel;
                    UpdateElementWindow updateElementWindow = new UpdateElementWindow(t.Name);
                    updateElementWindow.ShowDialog();

                    string newName = updateElementWindow.tbNewName.Text;

                    Town updatedTown = context.Towns
                        .Where(u => u.Id == t.Id)
                        .FirstOrDefault();

                    updatedTown.Name = newName;
                    context.SaveChanges();

                    UpdateTowns(t.RegionId);
                }
            }
        }

        //private void BtnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    if (cmbSelectType.Text == strRegion)
        //    {
        //        var query = context.Regions.AsQueryable();
        //        query = query.Where(x => x.Name.Contains(tbSearch.Text));

        //        var list = query.Select(r => new RegionModel
        //        {
        //            Id = r.Id,
        //            Name = r.Name
        //        }).ToList();
        //        _regions.Clear();
        //        _regions.AddRange(list);
        //    }

        //    if (cmbSelectType.Text == strTown)
        //    {
        //        var query = context.Towns.AsQueryable();
        //        query = query.Where(x => x.Name.Contains(tbSearch.Text));

        //        var list = query.Select(r => new TownModel
        //        {
        //            Id = r.Id,
        //            Name = r.Name
        //        }).ToList();
        //        _towns.Clear();
        //        _towns.AddRange(list);
        //    }
        //}


        private void CmbSelectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbSearch.IsEnabled = true;
        }
        private void BtnResetSearch_Click(object sender, RoutedEventArgs e)
        {
            UpdateRegions();
            UpdateTowns(-1);

            cmbSelectType.Text = "";
            tbSearch.Text = "";
            tbSearch.IsEnabled = false;

            lblWarning.Visibility = Visibility.Hidden;
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cmbSelectType.Text == strRegion)
            {
                var query = context.Regions.AsQueryable();
                query = query.Where(x => x.Name.Contains(tbSearch.Text));

                var list = query.Select(r => new RegionModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
                _regions.Clear();
                _towns.Clear();
                _regions.AddRange(list);
            }

            if (cmbSelectType.Text == strTown)
            {
                var query = context.Towns.AsQueryable();
                query = query.Where(x => x.Name.Contains(tbSearch.Text))
                    .Take(10);

                var list = query.Select(r => new TownModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    RegionId = r.RegionId,
                    TownTypeId = r.TownTypeId
                }).ToList();
                _towns.Clear();
                _towns.AddRange(list);
            }

            lblWarning.Visibility = Visibility.Visible;
        }


    }
}
