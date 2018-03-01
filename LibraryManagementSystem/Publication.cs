using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class BookLocation
    {
        public string Room { get; set; }
        public string Bookcase { get; set; }
        public string Shelf { get; set; }

        public BookLocation() { }
        public BookLocation(string Room, string Bookcase, string Shelf)
        {
            this.Room = Room;
            this.Bookcase = Bookcase;
            this.Shelf = Shelf;
        }
    }

    public class Publication
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public Author Writer { get; set; } = new Author();
        public PublicationType Type { get; set; }
        public List<Reader> Readers { get; set; } = new List<Reader>();
        public object Location { get; set; } = string.Empty;

        public Publication() { }
        private Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher)
        {
            this.Name = Name;
            this.Writer = Writer;
            this.Type = Type;
            this.DatePublished = DatePublished;
            this.Publisher = Publisher;
        }

        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher, BookLocation Location):
            this(Name, Writer, Type, DatePublished, Publisher)
        {
            this.Location = Location;
        }
        public Publication(string Name, Author Writer, PublicationType Type, DateTime DatePublished, string Publisher, Uri Location) :
            this(Name, Writer, Type, DatePublished, Publisher)
        {
            this.Location = Location;
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
        public override int GetHashCode() => ToString().GetHashCode();
    }
}