using System.Collections.Generic;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{

    public class Reader: NewPerson
    {
        public static HashSet<string> Groups;

        public AccessLevel AccessLevel { get; set; } = AccessLevel.Sutdent;
        public string Group { get; set; } = string.Empty;
        public HashSet<Publication> TakenPublications = new HashSet<Publication>();

        public Reader(): base() { }
        public Reader(string First, string Last, string Patronimic): base(First, Last, Patronimic)
        {
            this.AccessLevel = AccessLevel;
        }
        public Reader(string First, string Last, string Patronimic, string Group) : base(First, Last, Patronimic)
        {
            AccessLevel = AccessLevel.Sutdent;
            this.Group = Group;
        }

        public new static Reader FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public new static Reader FillBlanks(Gender gender)
        {
            var p = NewPerson.FillBlanks(gender);
            return new Reader(p.First, p.Last, p.Patronimic)
            {
                AccessLevel = (AccessLevel)NewValue.Int(2)
            };
        }

        public override string ToString() => $"{Last} {First} {Patronimic}, {AccessLevel}";
        public string Text => ToString();
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var reader = obj as Reader;
            return reader != null &&
                   base.Equals(obj) &&
                   Patronimic == reader.Patronimic &&
                   AccessLevel == reader.AccessLevel;
        }
    }

    public static partial class Extentions
    {
        public static Reader[] Add(this Reader[] array, Reader item) => array.Append(item).ToArray();
    }
}