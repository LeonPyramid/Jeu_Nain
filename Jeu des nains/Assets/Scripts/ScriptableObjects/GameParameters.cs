using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Data/GameParameters")]
public class GameParameters : ScriptableObject
{
    [Header("Carte")]
    public Area Zone1;
    public int depthZone1;
    public Area Zone2;
    public int depthZone2;
    public Area Zone3;
    public int depthZone3;
    public Area Zone4;
    public int depthZone4;
    [Header("Equipe de depart")]
    public List<CharacterData> team;
    public List<Artefact> artefacts;
    public int beer;
    public int stuff;
    public int gold;

    [Header("Parametre de calcul")]
    public int maxStuffByMember;
    public int maxBeerByMember;
    public float killingProba;//Pour quand plus de bière
    public Fork killingFork;
    public int stuffToDescend;
    public int multOfBeerConsumed;
    [Header("Paramètre d'UI")]
    public string fightEnterButton;
    public string fightFleeButton;
    public string buyButton;
    public string leaveButton;
    public string exploitBUtton;//pour les salles vides
    public string enterButton;//Pour les salles pièges/loots
    [TextArea(3, 10)]
    public string noMoney;
    [TextArea(3, 10)]
    public List<string> presentation;

}
