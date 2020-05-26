using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Data/Room/Loot")]
public class Loot : Room
{
    public LootType type;
    public Fork amountFork;
    public List<Artefact> artefacts;
    public List<CharacterData> mobType;
    [TextArea(3, 10)]
    public string entered;
}
