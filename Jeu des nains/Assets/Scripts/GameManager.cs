using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Function;



public class GameManager : MonoBehaviour
{
    public GameParameters parameters;
    public static GameManager Instance { get; private set; }
    public int depht { get; private set; }
    public Area actualArea;
    public Room actualRoom;
    private RoomType roomType;
    private RoomType None = RoomType.None;
    private RoomType room1;
    private Sprite room1logo;
    private RoomType room2;
    private Sprite room2logo;
    private SellInfo sellInfo;
    private List<string> dial;
    private int nbEnnemies;
    public List<Area> areas;
    public List<int> futuredepth;//TODO: Gestion des profondeurs
    private List<LoreList> loreList;
    private float loreproba;
    public bool entering;

    void Awake()
    {
        dial = new List<string>();
        areas = new List<Area>();
        areas.Add(parameters.Zone1);
        depht = 0;
        Instance = this;
        actualArea = parameters.Zone1;
        roomType = RoomType.None;
        TextBox.Instance.AddText(parameters.presentation, false, leftTxt: "entrer", rightTxt: "entrer");
        LoadNextArea();
    }

    public void LoadNextArea()
    {
        actualArea = areas[0];
        areas.RemoveAt(0);
        loreList = actualArea.lore.textList;
        loreproba = actualArea.lore.loreProba;
        entering = true;
    }

    public void EnterRoom(RoomType type)
    {
        dial = new List<string>();
        if (entering)
        {
            dial.Add(actualArea.Entering);
        }
        if(Random.value < loreproba)
        {
            dial.AddRange(loreList[0].text);
            loreList.RemoveAt(0);
        }
        //Choix de la salle
        switch (roomType)
        {
            case RoomType.Shop:
                actualRoom = RandomGet(actualArea.shops);
                Shop shopRoom = actualRoom as Shop;
                ShopManager.Instance.SetSell(shopRoom);
                sellInfo = ShopManager.Instance.GetSell();
                dial.Add(sellInfo.dialog.inTxt);
                TextBox.Instance.AddText(dial, false, rightTxt: parameters.buyButton, leftTxt: parameters.leaveButton);
                break;
            case RoomType.Fight:
                actualRoom = RandomGet(actualArea.empties);
                dial.AddRange(actualRoom.textIn);
                TextBox.Instance.AddText(dial, false, rightTxt: parameters.exploitBUtton, leftTxt: parameters.leaveButton);
                break;
            case RoomType.Empty:
                actualRoom = RandomGet(actualArea.loots);
                dial.AddRange(actualRoom.textIn);
                TextBox.Instance.AddText(dial, false, rightTxt: parameters.enterButton, leftTxt: parameters.leaveButton);
                break;
            case RoomType.Loot:
                actualRoom = RandomGet(actualArea.fights);
                dial.AddRange(actualRoom.textIn);
                nbEnnemies = Random.Range((actualRoom as Fight).ennemyAmount.min, (actualRoom as Fight).amountFork.max);
                dial.Add("Il y a en face de toi " + nbEnnemies.ToString() + " " + (actualRoom as Fight).ennemy.race + "!");
                TextBox.Instance.AddText(dial, false, rightTxt: parameters.fightEnterButton, leftTxt: parameters.fightFleeButton);
                break;
            default:
                Debug.LogError("Type de salle pas pris en charge! " + roomType.ToString());
                break;
        }
    }

    public void leftClick()
    {
        ClickHandler(false);
    }

    public void RightClick()
    {
        ClickHandler(true);
    }

    private void ClickHandler(bool isRight)
    {
        dial = new List<string>();
        switch (roomType)
        {
            case RoomType.Shop:
                if (isRight)
                {
                    if(StateManager.Instance.gold >= sellInfo.price)
                    {
                        LootManager.Instance.AddLoot(LootType.gold, value: (-sellInfo.price));
                        if(sellInfo.type == LootType.artefact)
                        {
                            dial.Add("Vous avez acheté l'artefact" + sellInfo.artefact.artName + ":\n" + sellInfo.artefact.description);
                        }
                    }
                    else
                    {
                        dial.Add(parameters.noMoney);
                    }
                }
                dial.Add(sellInfo.dialog.outTxt);
                break;
            case RoomType.Fight:
                if (isRight)
                {
                    int battle = FightHandler.Instance.CalculateFight(CreateOf((actualRoom as Fight).ennemy, nbEnnemies));
                    if (battle == -1)
                    {
                        dial.Add((actualRoom as Fight).win);
                        LootManager.Instance.AddLoot((actualRoom as Fight).type,
                            value: Random.Range((actualRoom as Fight).amountFork.min, (actualRoom as Fight).amountFork.min),
                            artefact: (actualRoom as Fight).artefact);
                    }
                    else
                    {
                        dial.Add((actualRoom as Fight).loose + "\nVous avez perdu " + battle.ToString() + " membres de l'équipe");
                        StateManager.Instance.KillMember(battle);
                    }
                }
                if (Random.value > actualArea.fightProba)
                {
                    string dialog = "";
                    int amount = -Random.Range(actualArea.fightLoose.min, actualArea.fightLoose.max);
                    int loose = Random.Range(0, 2);
                    switch (loose)
                    {
                        case 0:
                            dialog += "\nVous avez perdu " + amount + "L de bière!";
                            LootManager.Instance.AddLoot(LootType.beer, amount);
                            break;
                        case 1:
                            dialog += "\nVous avez perdu " + amount + "pièces d'équipement!";
                            LootManager.Instance.AddLoot(LootType.stuff, amount);
                            break;
                        case 2:
                            dialog += "\nVous avez perdu " + amount + " d'or!";
                            LootManager.Instance.AddLoot(LootType.gold, amount);
                            break;
                        default:
                            break;
                    }
                }
                break;
            case RoomType.Empty:
                if (isRight)
                {
                    dial.Add("Vous aves utilisé " + (actualRoom as Empty).stuffCost.ToString() + " d'équipement.");
                    if(Random.value < (actualRoom as Empty).proba)
                    {
                        int win = Random.Range((actualRoom as Empty).goldAmmount.min, (actualRoom as Empty).goldAmmount.max);
                        dial.Add((actualRoom as Empty).victory + "\nVous avez gagné " + win.ToString() + " or!");
                    }
                    else
                    {
                        dial.Add(((actualRoom as Empty).defeat));
                    }
                }
                dial.Add(actualRoom.textOut);
                break;
            case RoomType.Loot:
                if (isRight)
                {
                    Loot lroom = (actualRoom as Loot);
                    string dialog = lroom.entered;
                    if (lroom.amountFork.min >= 0)
                    {
                        int amount = Random.Range(lroom.amountFork.min, lroom.amountFork.max);
                        LootManager.Instance.AddLoot(lroom.type, amount, lroom.artefact, CreateOf(lroom.mobType, 1));
                        switch (lroom.type)
                        {
                            case LootType.artefact:
                                dialog += "\nVous avez gagné l'artefact " + lroom.artefact.artName + ":\n" + lroom.artefact.description;
                                break;
                            case LootType.beer:
                                dialog += "\nVous avez gagné " + amount + "L de bière!";
                                break;
                            case LootType.stuff:
                                dialog += "\nVous avez gagné " + amount + "pièces d'équipement!";
                                break;
                            case LootType.character:
                                dialog += "\nVous avez gagné " + amount + " " + lroom.mobType.race +"s!";
                                break;
                            case LootType.gold:
                                dialog += "\nVous avez gagné " + amount + " d'or!";
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        int amount = Random.Range(lroom.amountFork.min, lroom.amountFork.max);
                        switch (lroom.type)
                        {
                            case LootType.beer:
                                dialog += "\nVous avez perdu " + amount + "L de bière!";
                                LootManager.Instance.AddLoot(lroom.type, amount);
                                break;
                            case LootType.stuff:
                                dialog += "\nVous avez perdu " + amount + "pièces d'équipement!";
                                LootManager.Instance.AddLoot(lroom.type, amount);
                                break;
                            case LootType.character:
                                dialog += "\nVous avez gagné " + amount + " Menbres de votre équipe!";
                                StateManager.Instance.KillMember(amount);
                                break;
                            case LootType.gold:
                                dialog += "\nVous avez perdu " + amount + " d'or!";
                                LootManager.Instance.AddLoot(lroom.type, amount);
                                break;
                            default:
                                break;
                        }
                    }
                    dial.Add(dialog);
                }
                dial.Add(actualRoom.textOut);
                break;
            case RoomType.None:
                if (isRight)
                {
                    EnterRoom(room1);
                    roomType = room1;
                }
                else
                {
                    EnterRoom(room2);
                    roomType = room2;
                }
                depht += 1;
                return;
            default:
                break;
        }
        if(roomType != None)
        {
            roomType = None;
            float roomProba = Random.value;
            if (actualArea.shopProba > roomProba)
            {
                room1 = RoomType.Shop;
                room1logo = actualArea.shopLogo;
            }
            else { roomProba -= actualArea.shopProba; }
            if (roomType != None && actualArea.emptyProba > roomProba)
            {
                room1 = RoomType.Empty;
                room1logo = actualArea.emptyLogo;
            }
            else { roomProba -= actualArea.emptyProba; }
            if (roomType != None && actualArea.lootProba > roomProba)
            {
                room1 = RoomType.Loot;
                room1logo = actualArea.lootLogo;
            }
            else { roomProba -= actualArea.lootProba; }
            if (roomType != None && actualArea.fightProba > roomProba)
            {
                room1 = RoomType.Fight;
                room1logo = actualArea.fightLogo;
            }
            roomProba = Random.value;
            if (actualArea.shopProba > roomProba)
            {
                room2 = RoomType.Shop;
                room2logo = actualArea.shopLogo;
            }
            else { roomProba -= actualArea.shopProba; }
            if (roomType != None && actualArea.emptyProba > roomProba)
            {
                room2 = RoomType.Empty;
                room2logo = actualArea.emptyLogo;
            }
            else { roomProba -= actualArea.emptyProba; }
            if (roomType != None && actualArea.lootProba > roomProba)
            {
                room2 = RoomType.Loot;
                room2logo = actualArea.lootLogo;
            }
            else { roomProba -= actualArea.lootProba; }
            if (roomType != None && actualArea.fightProba > roomProba)
            {
                room2 = RoomType.Fight;
                room2logo = actualArea.fightLogo;
            }
            TextBox.Instance.AddText(dial, true,leftImg:room1logo,rightImg:room2logo);
        }
    }
}
public enum RoomType
{
    Shop,
    Fight,
    Empty,
    Loot,
    None
}