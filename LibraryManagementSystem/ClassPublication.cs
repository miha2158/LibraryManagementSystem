using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbPublication
    {
        public static ObservableCollection<DbPublication> All { get; set; } = Ex.Lib.DbPublicationSet1.Local;
        public static IEnumerable<string> AllPublishers => All.Select(e => e.Publisher).Distinct();
        public static ObservableCollection<DbDiscipline> AllDisciplines { get; set; } = Ex.Lib.DbDisciplineSet.Local;


        public IEnumerable<DbReader> Readers => PhysicalLocations.Where(e => e.IsTaken).Select(e => e.Reader).Distinct();

        public DbPublication(string Name, ePublicationType PublicationType, eBookPublication BookPublication, DateTime DatePublished, string Publisher)
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
            Authors = new List<DbAuthor>();
            this.Authors.Add(Author);
        }
        public DbPublication(string Name, IEnumerable<DbAuthor> Authors, ePublicationType PublicationType, eBookPublication BookPublication, DateTime DatePublished, string Publisher) :
            this(Name, PublicationType, BookPublication, DatePublished, Publisher)
        {
            this.Authors = new List<DbAuthor>(Authors);
        }
        
        public override bool Equals(object obj)
        {
            var publication = obj as DbPublication;
            return publication != null &&
                   Name == publication.Name &&
                   DatePublished == publication.DatePublished &&
                   Publisher == publication.Publisher &&
                   Authors.Equals(publication.Authors) &&
                   PublicationType == publication.PublicationType;
        }
        public override string ToString() => $"{Name} - {Authors}, {DatePublished}, {Publisher}";
        public override int GetHashCode() => ToString().GetHashCode();
    }
}