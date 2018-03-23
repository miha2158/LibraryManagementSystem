using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbBookLocation
    {
        public static List<DbBookLocation> All
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbBookLocationSet.ToList();
                }
            }
        }
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

        public DbBookLocation Clone()
        {
            return new DbBookLocation(Room, Place){ Reader = Reader, IsTaken = IsTaken, Publication = Publication };
        }

        public override string ToString() => IsTaken? $"{Reader}" : $"{Room}, {Place}";
    }
}