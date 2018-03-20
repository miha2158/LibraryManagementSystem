using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManagementSystem
{
    public partial class DbPublication
    {
        public static ObservableCollection<DbPublication> All => Ex.Lib.DbPublicationSet1.Local;
        public static IEnumerable<string> AllPublishers => All.Select(e => e.Publisher).Distinct();
        public static IEnumerable<DbDiscipline> AllDisciplines => Ex.Lib.DbDisciplineSet.Local.Distinct();


        public ePublicationType Type { get; set; }
        public IEnumerable<DbReader> Readers => PhysicalLocations.Where(e => e.IsTaken).Select(e => e.Reader).Distinct();

        public DbPublication(string Name, ePublicationType Type, eBookPublication BookPublication, DateTime DatePublished, string Publisher)
        {
            this.Name = Name;
            this.Type = Type;
            this.BookPublication = BookPublication.e();
            this.DatePublished = DatePublished;
            this.Publisher = Publisher;
        }
        public DbPublication(string Name, DbAuthor Author, ePublicationType Type, eBookPublication BookPublication, DateTime DatePublished, string Publisher):
            this(Name, Type, BookPublication, DatePublished, Publisher)
        {
            this.Authors.Add(Author);
        }
        public DbPublication(string Name, IEnumerable<DbAuthor> Authors, ePublicationType Type, eBookPublication BookPublication, DateTime DatePublished, string Publisher) :
            this(Name, Type, BookPublication, DatePublished, Publisher)
        {
            this.Authors.Union(Authors);
        }
        
        public override bool Equals(object obj)
        {
            var publication = obj as DbPublication;
            return publication != null &&
                   Name == publication.Name &&
                   DatePublished == publication.DatePublished &&
                   Publisher == publication.Publisher &&
                   Authors.Equals(publication.Authors) &&
                   Type == publication.Type;
        }
        public override string ToString() => $"{Name} - {Authors}, {DatePublished}, {Type}. {Publisher}";
        public string Text => ToString();
        public override int GetHashCode() => ToString().GetHashCode();
    }
}