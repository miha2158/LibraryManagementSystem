using Generator;

namespace LibraryManagementSystem
{
    public class Author: Person
    {
        public string Patronimic { get; set; } = string.Empty;
        public WriterType WriterType = WriterType.Other;

        public Author() { }
        public Author(string First, string Last, string Patronimic, WriterType WriterType) : base(First, Last)
        {
            this.Patronimic = Patronimic;
            this.WriterType = WriterType;
        }

        public new static Author FillBlanks(Gender gender)
        {
            var p = (Author) Person.FillBlanks(gender);
            p.Patronimic = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)] + (gender == Gender.Female? "овна": "ович");
            return p;
        }

        public override string ToString() => $"{Last} {First} {Patronimic}{(WriterType == WriterType.HseTeacher? " (ВШЭ)": string.Empty)}";
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var author = obj as Author;
            return author != null &&
                   base.Equals(obj) &&
                   Patronimic == author.Patronimic &&
                   WriterType == author.WriterType;
        }

        public static bool operator ==(Author a, object b) => a != null && a.Equals(b);
        public static bool operator !=(Author a, object b) => !(a == b);
    }
}