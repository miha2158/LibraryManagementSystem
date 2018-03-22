using System;
using System.Collections.Specialized;

namespace LibraryManagementSystem
{
    public enum eAccessLevel
    {
        Sutdent,
        Teacher,
        Admin
    }

    public enum eWriterType
    {
        HseTeacher,
        Other
    }

    public enum eBookPublication
    {
        None,
        Book,
        Publication, 
    }

    public enum ePublicationType
    {
        None,
        Educational,
        Scientific,
    }

    public static partial class Ex
    {
        public static LibraryDBContainer Lib = new LibraryDBContainer();

        public static void init()
        {
            Lib.DbAuthorSet1.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbReaderSet.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbBookLocationSet.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbCourseSet.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbDisciplineSet.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbPublicationSet1.Local.CollectionChanged += OnCollectionChanged;
            Lib.DbStatsSet.Local.CollectionChanged += OnCollectionChanged;
        }

        private static void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Lib.SaveChanges();
        }

        public static byte e(this eAccessLevel o) => (byte)o;
        public static byte e(this eWriterType o) => (byte)o;
        public static byte e(this eBookPublication o) => (byte)o;
        public static byte e(this ePublicationType o) => (byte)o;
    }
}