using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static bool CompareDictValues<TKey, TValue>(Dictionary<TKey, TValue> dictA,
        Dictionary<TKey, TValue> dictB)
    {
        foreach (KeyValuePair<TKey, TValue> pair in dictA)
        {
            if (!dictB.ContainsKey(pair.Key))
                return false;
        }

        return true;
    }

    public static List<T> GetSubset<T>(List<T> set, T removeVal)
    {
        List<T> subset = new List<T>();
        foreach (T val in set)
        {
            if (!val.Equals(removeVal))
                subset.Add(val);
        }

        return subset;
    }
}
