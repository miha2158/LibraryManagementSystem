using System.Collections.Generic;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{
    public class Author: Person
    {
        public new string First { get; set; } = string.Empty;
        public new string Last { get; set; } = string.Empty;
        public string Patronimic { get; set; } = string.Empty;
        public WriterType WriterType = WriterType.Other;
        public List<Publication> WrittenPublications = new List<Publication>();

        public Author() { }
        public Author(string First, string Last, string Patronimic, WriterType WriterType) : base(First, Last)
        {
            this.Patronimic = Patronimic;
            this.WriterType = WriterType;
        }

        public new static Author FillBlanks(Gender gender)
        {
            Author p = (Author) Person.FillBlanks(gender);
            p.Patronimic = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)] + (gender == Gender.Female? "овна": "ович");
            return p;
        }

        public static implicit operator Author(DBAuthor item)
        {
            return new Author(item.First, item.Last, item.Patronimic, (WriterType)item.WriterType)
            {
                WrittenPublications = item.WrittenPublications.Cast<Publication>().ToList()
            };
        }
        public static implicit operator DBAuthor(Author item)
        {
            return new DBAuthor()
            {
                First = item.First,
                Last =  item.Last,
                Patronimic = item.Patronimic,
                WriterType = (byte)item.WriterType,
                WrittenPublications = new HashSet<DBPublication>(item.WrittenPublications.Cast<DBPublication>())
            };
        }

        public override string ToString() => $"{Last} {First} {Patronimic}{(WriterType == WriterType.HseTeacher? " (ВШЭ)": string.Empty)}";
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