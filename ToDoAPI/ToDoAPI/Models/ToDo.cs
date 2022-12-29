using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        int _id;
        public int Id 
        {
            get => _id;
            set
            {
                if (_id == value)
                    return;

                _id = value;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
