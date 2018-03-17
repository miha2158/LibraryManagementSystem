namespace LibraryManagementSystem
{
    public class BookLocation
    {
        public bool IsTaken;

        public int Room { get; set; } = 0;
        public string Place { get; set; } = string.Empty;
        public Reader Reader { get; set; } = new Reader();

        public BookLocation() { }
        public BookLocation(int Room, string Place) : this()
        {
            this.Room = Room;
            this.Place = Place;
        }

        public override string ToString() => $"{Room}, {Place}";
    }
}