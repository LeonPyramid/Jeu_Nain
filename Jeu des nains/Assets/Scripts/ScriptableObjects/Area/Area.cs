using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Area", menuName = "Data/Area")]

public class Area : ScriptableObject
{
    [TextArea(3, 10)]
    public string Entering;
    public Lore lore;
    [Header("Shops")]
    public List<Shop> shops;
    public float shopProba;
    public Sprite shopLogo;
    [Header("Empty")]
    public List<Empty> empties;
    public float emptyProba;
    public Sprite emptyLogo;
    [Header("Loot")]
    public List<Loot> loots;
    public float lootProba;
    public Sprite lootLogo;
    [Header("Fight")]
    public List<Fight> fights;
    public float fightProba;
    public Sprite figtLogo;
    public Fork fightLoose;
}
