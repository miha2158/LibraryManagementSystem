using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{
    public partial class DbAuthor
    {
        public static List<DbAuthor> All
        {
            get
            {
                using (var db = new LibraryDBContainer())
                {
                    return db.DbAuthorSet1.ToList();
                }
            }
        }


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
            var o = obj as DbAuthor;
            using (var db = new LibraryDBContainer())
            {
                return db.DbAuthorSet1.Any(d => d.Id == o.Id &&
                                               d.First == o.First &&
                                               d.Last == o.Last &&
                                               d.Patronimic == o.Patronimic &&
                                               d.WriterType == o.WriterType);
            }
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