using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbBookLocation
    {
        public static IEnumerable<int> Rooms => DbPublication.All.SelectMany(e => e.PhysicalLocations).Select(e => e.Room).Distinct();


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