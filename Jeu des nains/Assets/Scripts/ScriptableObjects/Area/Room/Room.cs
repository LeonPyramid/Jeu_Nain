using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : ScriptableObject
{
    [TextArea(3, 10)]
    public List<string> textIn;
    public int chance;//TODO: Soit mettre en place le système de comptage, soit enlever
    [TextArea(3, 10)]
    public string textOut;
}
