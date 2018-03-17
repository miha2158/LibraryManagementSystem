using System.Collections.Generic;
using System.IO;
using Generator;

namespace LibraryManagementSystem
{
    public class NewPerson: Person
    {
        public string Patronimic { get; set; } = string.Empty;

        public NewPerson() { }
        public NewPerson(string First, string Last, string Patronimic)
        {
            this.First = First;
            this.Last = Last;
            this.Patronimic = Patronimic;
        }

        public static List<string> FemalePatronymics => femalePatronymics ?? (femalePatronymics = new List<string>(File.ReadAllLines(@"femalefirstnames.txt")));
        public static List<string> MalePatronymics => malePatronymics ?? (malePatronymics = new List<string>(File.ReadAllLines(@"femalelastnames.txt")));
        private static List<string> malePatronymics;
        private static List<string> femalePatronymics;

        public new static NewPerson FillBlanks(Gender gender)
        {
            var p = Person.FillBlanks(gender);
            var b = new NewPerson()
            {
                First = p.First,
                Last = p.Last
            };

            if (gender == Gender.Female)
                b.Patronimic = FemalePatronymics[NewValue.Int(FemalePatronymics.Count)];
            else
                b.Patronimic = MalePatronymics[NewValue.Int(MalePatronymics.Count)];

            return b;
        }
    }
}