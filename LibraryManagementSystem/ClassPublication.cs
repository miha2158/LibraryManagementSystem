using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class BookLocation
    {
        public string Room { get; set; } = string.Empty;
        public string Bookcase { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;

        public BookLocation() { }
        public BookLocation(string Room, string Bookcase, string Shelf) : this()
        {
            this.Room = Room;
            this.Bookcase = Bookcase;
            this.Shelf = Shelf;
        }
        public BookLocation(string All): this()
        {
            var p = All.Split(new[] {',', ' '}, 3, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length > 0)
                Room = p[0];
            if (p.Length > 1)
                Bookcase = p[1];
            if (p.Length > 2)
                Shelf = p[2];
        }

        public override string ToString() => $"{Room}, {Bookcase}, {Shelf}";
    }

    public class Publication
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public List<Author> Writer { get; set; } = new List<Author>();
        public PublicationType Type { get; set; }
        public List<Reader> Reader { get; set; } = new List<Reader>();
        public BookLocation PhysicalLocation { get; set; } = new BookLocation();
        public Uri InternetLocation { get; set; } = new Uri("");

        public Publication() { }
        public Publication(string Name, PublicationType Type, DateTime DatePublished, string Publisher)
        {
            this.Name = Name;
            this.Type = Type;
            this.DatePublished = DatePublished;
            this.Publisher = Publisher;
        }
        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher):
            this(Name, Type, DatePublished, Publisher)
        {
            this.Writer.Add(Writer);
        }
        public Publication(string Name, IEnumerable<Author> Writer, PublicationType Type, DateTime DatePublished, string Publisher) :
            this(Name, Type, DatePublished, Publisher)
        {
            this.Writer.AddRange(Writer);
        }

        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher, BookLocation PhysicalLocation) :
            this(Name, Writer, Type, DatePublished, Publisher)
        {
            this.PhysicalLocation = PhysicalLocation;
        }
        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher, Uri InternetLocation) :
            this(Name, Writer, Type, DatePublished, Publisher)
        {
            this.InternetLocation = InternetLocation;
        }

        public static implicit operator Publication(DBPublication item)
        {
            return new Publication(item.Name, item.Writer.Cast<Author>(), (PublicationType)item.Type, item.DatePublished, item.Publisher)
            {
                Reader = item.Reader.Cast<Reader>().ToList()
            };
        }
        public static implicit operator DBPublication(Publication item)
        {
            return new DBPublication()
            {
                DatePublished = item.DatePublished,
                InternetLocation = item.InternetLocation.AbsolutePath,
                Name = item.Name,
                PhysicalLocation = item.ToString(),
                Publisher = item.Publisher,
                Type = (byte)item.Type,
                Reader = new HashSet<DBReader>(item.Reader.Cast<DBReader>()),
                Writer = new HashSet<DBAuthor>(item.Writer.Cast<DBAuthor>())
            };
        }

        public override bool Equals(object obj)
        {
            var publication = obj as Publication;
            return publication != null &&
                   Name == publication.Name &&
                   DatePublished == publication.DatePublished &&
                   Publisher == publication.Publisher &&
                   Writer.Equals(publication.Writer) &&
                   Type == publication.Type;
        }
        public override string ToString() => $"{Name} - {Writer}, {DatePublished}, {Type}. {Publisher}";
        public string Text => ToString();
        public override int GetHashCode() => ToString().GetHashCode();
    }
}