using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "Data/Area")]

public class Area : ScriptableObject
{
    [TextArea(3, 10)]
    public string Entering;
    public List<Shop> shops;
    public float shopProba;
    public List<Empty> empties;
    public float emptyProba;
    public List<Loot> loots;
    public float lootProba;
    public List<Fight> fights;
    public float fightProba;
    public Lore lore;
    public Fork fightLoose;
}
