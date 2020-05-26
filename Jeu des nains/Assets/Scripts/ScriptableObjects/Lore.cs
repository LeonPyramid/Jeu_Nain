using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Function;
[CreateAssetMenu(fileName = "Lore", menuName = "Data/Lore")]
public class Lore : ScriptableObject
{
    
    public List<LoreList> textList;

}

[System.Serializable]
public class LoreList
{
    [TextArea(3, 10)]
    public List<string> text;
}