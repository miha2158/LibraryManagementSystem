using System.Linq;
using Generator;

namespace LibraryManagementSystem
{
    public class Reader: Person
    {
        public new string First { get; set; } = string.Empty;
        public new string Last { get; set; } = string.Empty;
        public string Patronimic { get; set; } = string.Empty;
        public AccessLevel AccessLevel { get; set; } = AccessLevel.Sutdent;

        public Reader() { }
        public Reader(string First, string Last, string Patronimic, AccessLevel AccessLevel): base(First, Last)
        {
            this.Patronimic = Patronimic;
            this.AccessLevel = AccessLevel;
        }

        public new static Reader FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public new static Reader FillBlanks(Gender gender)
        {
            var b = Person.FillBlanks(gender);
            Reader p = new Reader
            {
                First = b.First,
                Last = b.Last
            };

            p.Patronimic = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)] + (gender == Gender.Female ? "овна" : "ович");
            return p;
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