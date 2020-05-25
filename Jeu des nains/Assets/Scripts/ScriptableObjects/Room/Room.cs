using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> text;
    public int chance;
}
