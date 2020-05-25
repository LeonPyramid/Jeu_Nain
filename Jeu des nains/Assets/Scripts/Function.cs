using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Function
{
    public static T RandomGet<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}

