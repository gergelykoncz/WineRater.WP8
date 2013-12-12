using System;
using System.Data.Linq.Mapping;

namespace WineRater.Entities
{
    [Table]
    public class Wine : ObservableModel
    {
        private int _wineId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int WineId
        {
            get
            {
                return _wineId;
            }
            set
            {
                if (_wineId != value)
                {
                    NotifyPropertyChanging("WineId");
                    _wineId = value;
                    NotifyPropertyChanged("WineId");
                }
            }
        }

        private string _name;

        [Column]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private int _rating;

        [Column]
        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (_rating != value)
                {
                    NotifyPropertyChanging("Rating");
                    _rating = value;
                    NotifyPropertyChanged("Rating");
                }
            }
        }

        private WineType _wineType;

        [Column]
        public WineType WineType
        {
            get
            {
                return _wineType;
            }
            set
            {
                if (_wineType != value)
                {
                    NotifyPropertyChanging("WineType");
                    _wineType = value;
                    NotifyPropertyChanged("WineType");
                }
            }
        }

        private byte[] _picture;

        [Column(DbType="image")]
        public byte[] Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                if (_picture != value)
                {
                    NotifyPropertyChanging("Picture");
                    _picture = value;
                    NotifyPropertyChanged("Picture");
                }
            }
        }

        private DateTime _dateAdded;

        public  DateTime DateAdded
        {
            get
            {
                return _dateAdded;
            }
            set
            {
                if (_dateAdded != value)
                {
                    NotifyPropertyChanging("DateAdded");
                    _dateAdded = value;
                    NotifyPropertyChanged("DateAdded");
                }
            }
        }
        
    }
}