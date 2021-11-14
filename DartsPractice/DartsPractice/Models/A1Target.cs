using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DartsPractice.Models
{
    public class A1Target
    {
        public ObservableCollection<bool> Hits { get; set; }
        public bool IsActive { get; set; }
        public bool IsClosed { get; set; }
    }
}
