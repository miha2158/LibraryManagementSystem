using Generator;

namespace LibraryManagementSystem
{
    public class Author: Person
    {
        public string Patronimic { get; set; }

        public Author() { }
        public Author(string First, string Last, string Patronimic) : base(First, Last)
        {

        }
    }
}