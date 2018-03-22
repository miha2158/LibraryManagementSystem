using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{
    public partial class DbAuthor
    {
        public static ObservableCollection<DbAuthor> All { get; set; } = Ex.Lib.DbAuthorSet1.Local;


        public DbAuthor(string First, string Last, string Patronimic, eWriterType WriterType) : this()
        {
            this.First = First;
            this.Last = Last;
            this.Patronimic = Patronimic;
            this.WriterType = WriterType.e();
        }

        public static DbAuthor FillBlanks() => FillBlanks((Gender)NewValue.Int(2));
        public static DbAuthor FillBlanks(Gender gender)
        {
            var p = NewPerson.FillBlanks(gender);
            var b = new DbAuthor(p.First, p.Last, p.Patronimic, (eWriterType)NewValue.Int(2));
            All.Add(b);
            return b;
        }
        
        public override string ToString() => $"{Last} {First[0]}.{Patronimic[0]}.";
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var author = obj as DbAuthor;
            return author != null &&
                   base.Equals(obj) &&
                   Patronimic == author.Patronimic &&
                   WriterType == author.WriterType;
        }
    }

    public partial class DbAuthor
    {
        public DbAuthor ToAuthor()
        {
            return new DbAuthor(First, Last, Patronimic, (eWriterType)WriterType)
            {
                Id = this.Id,
                Publications = this.Publications,
            };
        }
    }
}