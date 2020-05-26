using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lore", menuName = "Data/Lore")]
public class Fight : Room
{
    [TextArea(3, 10)]
    public string win;
    [TextArea(3, 10)]
    public string loose;
    [TextArea(3, 10)]
    public string fleeLose;
    public Fork ennemyAmount;
    public CharacterData ennemy;
    [Header("Loot")]
    public LootType type;
    public Fork amountFork;
    public Artefact artefact;
}
