using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string race;
    public int power;
    public int price;
    public Image Image;
    public string lore;
    public int beerConsumption;
    public bool immortal;
}
