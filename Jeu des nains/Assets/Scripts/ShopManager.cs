using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Function;

public class ShopManager : MonoBehaviour
{
    private static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ShopManager>();
            return _instance;
        }
    }
    void Start()
    {
        _instance = this;
    }

        private SellInfo info;

    public void SetSell(Shop shop)
    {
        //TODO: envoi des dialogues
        info.type = RandomGet(shop.typeAvailable);
        switch (info.type)
        {
            case LootType.artefact:
                info.dialog = RandomGet(shop.artefactDialog);
                info.artefact = RandomGet(shop.artefactList);
                info.price = info.artefact.price;
                info.dialog.inTxt += "\nIl vous le vend pour " + info.price.ToString() + " or.";
                //TODO:UI
                break;
            case LootType.beer:
                info.dialog = RandomGet(shop.beerDialog);
                info.amount = Random.Range(shop.beerFork.min, shop.beerFork.max);
                info.price = info.amount * shop.beerValue;
                info.dialog.inTxt += "\nIl vous en vend " + info.amount.ToString() + " pour " + info.price.ToString() + " or.";
                //TODO:UI
                break;
            case LootType.stuff:
                info.dialog = RandomGet(shop.stuffDialog);
                info.amount = Random.Range(shop.stuffFork.min, shop.stuffFork.max);
                info.price = info.amount * shop.stuffValue;
                info.dialog.inTxt += "\nIl vous en vend " + info.amount.ToString() + " pour " + info.price.ToString() + " or.";
                //TODO:UI
                break;
            case LootType.character:
                info.dialog = RandomGet(shop.memberDialog);
                info.amount = Random.Range(shop.memberFork.min, shop.memberFork.max);
                CharacterData data = info.dialog.data;
                info.dialog.inTxt += "\nC'est une équipe composée de " + info.amount.ToString() + " " + data.race + "s.";
                info.price = 0;
                for (int i = 0; i < info.amount; i++)
                {
                    info.price += data.price;
                    info.team.Add(data);
                }
                info.dialog.inTxt +="\nIls se propose de ce joindre à vous pour " + info.price.ToString() + " or.";
                //TODO:UI
                break;
            default:
                Debug.LogError("Type pas connu!");
                break;
        }


    }

    public SellInfo GetSell()
    {
        return info;
    }

}

[System.Serializable]
public class SellInfo
{
    public LootType type;
    public int price = 0;
    public int amount = 0;
    public Artefact artefact;
    public List<CharacterData> team = new List<CharacterData>();
    public SellDialog dialog = new SellDialog();
}