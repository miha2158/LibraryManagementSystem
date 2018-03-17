using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Publication
    {
        public static HashSet<string> AllPublishers { get; set; } = new HashSet<string>();

        public string Name { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public HashSet<Author> Writer { get; set; } = new HashSet<Author>();
        public PublicationType Type { get; set; }
        public HashSet<Reader> Readers { get; set; } = new HashSet<Reader>();
        public HashSet<BookLocation> PhysicalLocations { get; set; } = new HashSet<BookLocation>();
        public Uri InternetLocation { get; set; } = new Uri("");

        public Publication() { }
        public Publication(string Name, PublicationType Type, DateTime DatePublished, string Publisher)
        {
            this.Name = Name;
            this.Type = Type;
            this.DatePublished = DatePublished;
            this.Publisher = Publisher;
            AllPublishers.Add(Publisher);
        }
        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher):
            this(Name, Type, DatePublished, Publisher)
        {
            this.Writer.Add(Writer);
        }
        public Publication(string Name, IEnumerable<Author> Writer, PublicationType Type, DateTime DatePublished, string Publisher) :
            this(Name, Type, DatePublished, Publisher)
        {
            this.Writer.UnionWith(Writer);
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