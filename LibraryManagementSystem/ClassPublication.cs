using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbPublication
    {
        public static List<DbPublication> All
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.ToList();
                }
            }
        }
        public static IEnumerable<string> AllPublishers => All.Select(e => e.Publisher).Distinct();
        public static List<DbDiscipline> AllDisciplines
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbDisciplineSet.ToList();
                }
            }
        }


        public IEnumerable<DbReader> Readers
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).PhysicalLocations.Where(e => e.IsTaken).Select(e => e.Reader).Distinct();
                }
            }
        }
        public IEnumerable<DbBookLocation> Locations
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).PhysicalLocations.Where(e => !e.IsTaken).Distinct();
                }
            }
        }

        private DbPublication(string Name, ePublicationType PublicationType, eBookPublication BookPublication, DateTime DatePublished, string Publisher)
        {
            this.Name = Name;
            this.PublicationType = PublicationType.e();
            this.BookPublication = BookPublication.e();
            this.DatePublished = DatePublished;
            this.Publisher = Publisher;
        }
        public DbPublication(string Name, DbAuthor Author, ePublicationType PublicationType, eBookPublication BookPublication, DateTime DatePublished, string Publisher):
            this(Name, PublicationType, BookPublication, DatePublished, Publisher)
        {
            Authors = new []{ Author };
        }
        public DbPublication(string Name, IEnumerable<DbAuthor> Authors, ePublicationType PublicationType, eBookPublication BookPublication, DateTime DatePublished, string Publisher) :
            this(Name, PublicationType, BookPublication, DatePublished, Publisher)
        {
            this.Authors = Authors.ToArray();
        }
        
        public override bool Equals(object obj)
        {
            using (var db = new LibraryDBContainer())
            {

                var publication = obj as DbPublication;

                return  db.DbPublicationSet1.Any(e => e.Name == publication.Name && 
                                              e.DatePublished == publication.DatePublished && 
                                              e.Publisher == publication.Publisher &&
                                              e.PublicationType == publication.PublicationType &&
                                              e.BookPublication == publication.BookPublication);
                
            }
        }
        public override string ToString() => $"{Name}, {DatePublished}, {Publisher}";
        public override int GetHashCode() => ToString().GetHashCode();
    }
}