using Generator;

namespace LibraryManagementSystem
{
    public class Reader: Person
    {
        public string Patronimic { get; set; } = string.Empty;
        public AccessLevel AccessLevel { get; set; } = AccessLevel.Sutdent;

        public Reader() { }
        public Reader(string First, string Last, string Patronimic, AccessLevel AccessLevel): base(First, Last)
        {
            this.Patronimic = Patronimic;
            this.AccessLevel = AccessLevel;
        }

        public new static Reader FillBlanks(Gender gender)
        {
            var p = (Reader)Person.FillBlanks(gender);
            p.Patronimic = MaleFirstNames[NewValue.Int(MaleFirstNames.Count)] + (gender == Gender.Female ? "овна" : "ович");
            return p;
        }

        public override string ToString() => $"{Last} {First} {Patronimic}, {AccessLevel}";
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var reader = obj as Reader;
            return reader != null &&
                   base.Equals(obj) &&
                   Patronimic == reader.Patronimic &&
                   AccessLevel == reader.AccessLevel;
        }

        public static bool operator ==(Reader a, object b) => a != null && a.Equals(b);
        public static bool operator !=(Reader a, object b) => !(a == b);
    }
}