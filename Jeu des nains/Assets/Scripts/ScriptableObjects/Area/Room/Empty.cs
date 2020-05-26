using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty", menuName = "Data/Room/Empty")]
public class Empty : Room
{
    public float proba;
    public Fork goldAmmount;
    public int stuffCost;
    [TextArea(3, 10)]
    public string victory;
    [TextArea(3, 10)]
    public string defeat;
}
