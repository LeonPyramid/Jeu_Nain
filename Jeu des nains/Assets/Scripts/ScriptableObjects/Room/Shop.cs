using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Data/Room/Shop")]
public class Shop : Room
{
    public List<LootType> typeAvailable;
    [Header("Beer")]
    public int beerValue;
    public Fork beerFork;
    public List<SellDialog> beerDialog;
    [Header("Artefact")]
    public List<Artefact> artefactList;
    public List<SellDialog> artefactDialog;
    //TODO: Gérer les retours du gosse bizarre
    [Header("Member")]
    public Fork memberFork;
    public List<SellDialog> memberDialog;
    [Header("Stuff")]
    public int stuffValue;
    public Fork stuffFork;
    public List<SellDialog> stuffDialog;
}

[System.Serializable]
public class Fork
{
    public int min;
    public int max;
}

[System.Serializable]
public struct SellDialog
{
    [TextArea(3, 10)]
    public string inTxt;
    [TextArea(3, 10)]
    public string outTxt;
    public bool isDistrib;
    [TextArea(3, 10)]
    public string distribLoose;
    public CharacterData data;
}