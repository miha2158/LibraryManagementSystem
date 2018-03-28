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
                    return db.DbDisciplineSet.Where(d => d != null).ToList();
                }
            }
        }

        public string DDate => DatePublished.ToNiceDate();
        public string DAuthors
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).Authors.Aggregate(string.Empty, (c, d) => c += $"{d}, ");
                }
            }
        }
        public int DCount
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).PhysicalLocations.Count;
                }
            }
        }
        public int DNowTakenCount
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).PhysicalLocations.Count(e => e.IsTaken);
                }
            }
        }
        public int DTotalTakenCount
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).Stats.Count;
                }
            }
        }
        public string DCourses
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).Course.Aggregate(string.Empty, (c, d) => c += $"{d.Course}, ");
                }
            }
        }
        public string DDisciplines
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbPublicationSet1.Find(Id).Discipline.Aggregate(string.Empty, (p, d) => p += $"{d.Name}, ");
                }
            }
        }
        public ePublicationType toEnumPT => (ePublicationType) PublicationType;
        public eBookPublication toEnumBP => (eBookPublication) BookPublication;


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