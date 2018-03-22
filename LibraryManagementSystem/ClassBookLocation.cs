using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbBookLocation
    {
        public static ObservableCollection<DbBookLocation> All { get; set; } = Ex.Lib.DbBookLocationSet.Local;
        public static IEnumerable<int> Rooms => All.Select(e => e.Room).Distinct();

        public DbBookLocation() { }
        public DbBookLocation(int Room, string Place) : this()
        {
            this.Room = Room;
            this.Place = Place;
        }
        public DbBookLocation(DbReader Reader)
        {
            this.Reader = Reader;
        }

        public override string ToString() => IsTaken? $"{Reader}" : $"{Room}, {Place}";
    }
}