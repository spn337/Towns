﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Towns.MVVM
{
    public class TownTypeModel : INotifyPropertyChanged
    {
        private string _name;
        public int Id { get; set; }
        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
