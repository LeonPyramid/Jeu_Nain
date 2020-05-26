using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Function;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
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
                //TODO:UI
                break;
            case LootType.beer:
                info.dialog = RandomGet(shop.beerDialog);
                info.amount = Random.Range(shop.beerFork.min, shop.beerFork.max);
                info.price = info.amount * shop.beerValue;
                //TODO:UI
                break;
            case LootType.stuff:
                info.dialog = RandomGet(shop.stuffDialog);
                info.amount = Random.Range(shop.stuffFork.min, shop.stuffFork.max);
                info.price = info.amount * shop.stuffValue;
                //TODO:UI
                break;
            case LootType.character:
                info.dialog = RandomGet(shop.memberDialog);
                info.amount = Random.Range(shop.memberFork.min, shop.memberFork.max);
                CharacterData data = info.dialog.data;
                info.price = 0;
                for (int i = 0; i < info.amount; i++)
                {
                    info.price += data.price;
                    info.team.Add(data);
                }
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