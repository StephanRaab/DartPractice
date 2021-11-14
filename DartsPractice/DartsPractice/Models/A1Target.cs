using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MvvmHelpers;

namespace DartsPractice.Models
{
    public class A1Target : INotifyPropertyChanged
    {

        private ObservableCollection<bool> _hits { get; set; } = new ObservableCollection<bool> { false, false, false, false, false };
        public ObservableCollection<bool> Hits
        {
            get => _hits;
            set {
                if (value != _hits)
                    _hits = value;
                    OnPropertyChanged(nameof(Hits));
            } 
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                    _isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public bool IsClosed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
