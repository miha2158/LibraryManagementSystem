using System.Collections.Generic;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{
    public class Author: NewPerson
    {
        public WriterType WriterType { get; set; } = WriterType.Other;
        public HashSet<Publication> WrittenPublications = new HashSet<Publication>();

        public Author() { }
        public Author(string First, string Last, string Patronimic, WriterType WriterType) : base(First, Last, Patronimic)
        {
            this.WriterType = WriterType;
        }

        public new static Author FillBlanks() => FillBlanks((Gender)NewValue.Int(2));
        public new static Author FillBlanks(Gender gender)
        {
            var p = NewPerson.FillBlanks(gender);
            return new Author(p.First, p.Last, p.Patronimic, (WriterType)NewValue.Int(2));
        }
        
        public override string ToString() => $"{Last} {First[0]}.{Patronimic[0]}.";
        public string Text => ToString();
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var author = obj as Author;
            return author != null &&
                   base.Equals(obj) &&
                   Patronimic == author.Patronimic &&
                   WriterType == author.WriterType;
        }
    }
}