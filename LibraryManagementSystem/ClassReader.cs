﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Generator;

namespace LibraryManagementSystem
{

    public partial class DbReader
    {
        public static IEnumerable<string> Groups => All.Select(e => e.Group).Distinct();
        public static ObservableCollection<DbReader> All { get; set; } = Ex.Lib.DbReaderSet.Local;

        public HashSet<DbPublication> TakenPublications = new HashSet<DbPublication>();
        
        public DbReader(string First, string Last, string Patronimic): this()
        {
            this.First = First;
            this.Last = Last;
            this.Patronimic = Patronimic;
            this.AccessLevel = AccessLevel;
        }
        public DbReader(string First, string Last, string Patronimic, string Group) : this(First, Last, Patronimic)
        {
            AccessLevel = eAccessLevel.Sutdent.e();
            this.Group = Group;
        }

        private static string[] GroupNames = new[] { "А", "Ю", "Я", "Ж", "Ё", "ПИ" };
        public static DbReader FillBlanks() => FillBlanks((Gender) NewValue.Int(2));
        public static DbReader FillBlanks(Gender gender)
        {
            var p = NewPerson.FillBlanks(gender);
            var b = new DbReader(p.First, p.Last, p.Patronimic, $"{GroupNames.Random()}-{NewValue.Int(15,18)}-{NewValue.Int(1,3)}") { AccessLevel = (byte)NewValue.Int(2) };
            All.Add(b);
            return b;
        }
        
        public override string ToString() => $"{Last} {First[0]}. {Patronimic[0]}.";
        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object obj)
        {
            var reader = obj as DbReader;
            return reader != null &&
                   base.Equals(obj) &&
                   Patronimic == reader.Patronimic &&
                   AccessLevel == reader.AccessLevel;
        }
    }

    public static partial class Ex
    {
        public static DbReader[] Add(this DbReader[] array, DbReader item) => array.Append(item).ToArray();

        public static T Random<T>(this IEnumerable<T> o) => o.ElementAt(NewValue.Int(o.Count()));
    }
}