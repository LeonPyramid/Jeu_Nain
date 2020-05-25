using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Function;

public class ShopManager : MonoBehaviour
{
    private static ShopManager _instance;
    public static ShopManager Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }


    public void GetSell(Shop shop)
    {
        //TODO: envoi des dialogues
        LootType type = RandomGet(shop.typeAvailable);
        int price;
        int amount;
        Artefact artefact;
        List<CharacterData> team;
        SellDialog dialog;
        switch (type)
        {
            case LootType.artefact:
                dialog = RandomGet(shop.artefactDialog);
                artefact = RandomGet(shop.artefactList);
                price = artefact.price;
                //TODO:UI
                break;
            case LootType.beer:
                dialog = RandomGet(shop.beerDialog);
                amount = Random.Range(shop.beerFork.min, shop.beerFork.max);
                price = amount * shop.beerValue;
                break;
            case LootType.stuff:
                dialog = RandomGet(shop.stuffDialog);
                amount = Random.Range(shop.stuffFork.min, shop.stuffFork.max);
                price = amount * shop.stuffValue;
                break;
            case LootType.character:
                dialog = RandomGet(shop.memberDialog);
                amount = Random.Range(shop.memberFork.min, shop.memberFork.max);

                break;
            default:
                Debug.LogError("Type pas connu!");
                break;
        }
    }

    public T RandomGet<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}