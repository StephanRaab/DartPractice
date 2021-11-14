using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DartsPractice.Models
{
    public class A1Target
    {
        public ObservableCollection<bool> Hits { get; set; } = new ObservableCollection<bool> { false, false, false, false, false };
        public bool IsActive { get; set; } = false;
        public bool IsClosed { get; set; } = false;
    }
}
