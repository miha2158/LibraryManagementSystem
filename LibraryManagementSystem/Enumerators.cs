using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace LibraryManagementSystem
{
    public enum eAccessLevel
    {
        Student,
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
        public static byte e(this eAccessLevel o) => (byte)o;
        public static byte e(this eWriterType o) => (byte)o;
        public static byte e(this eBookPublication o) => (byte)o;
        public static byte e(this ePublicationType o) => (byte)o;

        public static string ToString(this ICollection<DbAuthor> o) => o.Count.ToString();
        public static string ToString(this ICollection<DbReader> o) => o.Count.ToString();
        public static string ToString(this ICollection<DbPublication> o) => o.Count.ToString();
        public static string ToString(this ICollection<DbCourse> o) => o.Aggregate(string.Empty, (p, d) => p += $"{d.Course} ");
        public static string ToString(this ICollection<DbStats> o) => o.Count.ToString();
        public static string ToString(this ICollection<DbDiscipline> o) => o.Aggregate(string.Empty, (p, d) => p += $"{d.Name}, ");
        public static string ToString(this ICollection<DbBookLocation> o) => o.Count(d => !d.IsTaken).ToString();

        public static string ToNiceDate(this DateTime o) => $"{o.Year}-{o.Month}-{o.Day}";
    }
}