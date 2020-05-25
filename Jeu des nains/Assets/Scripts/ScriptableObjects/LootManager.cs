using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    private static LootManager _instance;
    public static LootManager Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Ajoute ou enlève quelque chose des stats du joueur, artefact ou item
    /// un seul à mettre avec value: , artefact: ou members:
    /// </summary>
    /// <param name="type">le type de loot</param>
    /// <param name="value">la valeur si pas artefact</param>
    /// <param name="artefact">l'artefact</param>
    /// <param name="members">l'artefact</param>
    public void LootHandler(LootType type,int value = 0,Artefact artefact = null,List<CharacterData> members = null)
    {
        switch (type)
        {
            case LootType.artefact:
                StateManager.Instance.AddArtefact(artefact);
                break;
            case LootType.megaArtefact:
                //TODO : Mega Atefact
                break;
            case LootType.beer:
                StateManager.Instance.AddBeer(value);
                break;
            case LootType.stuff:
                StateManager.Instance.AddStuff(value);
                break;
            case LootType.character:
                foreach (CharacterData item in members)
                {
                    StateManager.Instance.AddMember(item);
                }
                break;
            case LootType.gold:
                StateManager.Instance.AddGold(value);
                break;
            default:
                Debug.LogError("Type pas connu!");
                break;
        }
    }
}

public enum LootType
{
    artefact,
    megaArtefact,
    beer,
    stuff,
    character,
    gold
}
