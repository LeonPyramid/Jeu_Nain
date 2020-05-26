using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Function
{
    public static T RandomGet<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    public static List<T> CreateOf<T>(T obj,int amount)
    {
        List<T> lst = new List<T>();
        for (int i = 0; i < amount; i++)
        {
            lst.Add(obj);
        }
        return lst;
    }
}

