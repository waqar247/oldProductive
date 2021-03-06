﻿using System;
using System.Collections.Generic;
using DLToolkit.Forms.Controls;

namespace IAmProductive.Helpers
{
    public class Grouping<K, T> : FlowObservableCollection<T>
    {
        public K Key { get; private set; }
        public int ColumnCount { get; private set; }

        public Grouping(K key)
        {
            Key = key;
        }

        public Grouping(K key, IEnumerable<T> items)
            : this(key)
        {
            AddRange(items);
        }

        public Grouping(K key, IEnumerable<T> items, int columnCount)
            : this(key, items)
        {
            ColumnCount = columnCount;
        }
    }
}
