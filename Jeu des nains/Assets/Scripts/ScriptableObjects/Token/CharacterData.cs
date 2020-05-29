using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string race;
    public float power;
    public int price;
    public Sprite Image;
    public string lore;
    public int beerConsumption;
    public bool immortal;
}
