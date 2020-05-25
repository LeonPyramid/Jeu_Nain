using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static StateManager _instance;
    public static StateManager Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }
    public List<CharacterData> team { get; private set; }
    public List<Artefact> artefacts { get; private set; }
    public int beer { get; private set; }
    public int beerMax { get; private set; }
    public int stuff { get; private set; }
    public int stuffMax { get; private set; }
    public int gold { get; private set; }
    private int beerConsumed;

    public void AddBeer(int amount)
    {
        beer += amount;
        if (beer > beerMax) { beer = beerMax; }
        //TODO: CHanger l'affichage
        //TODO: Plus de bière
    }

    public void AddGold(int amount)
    {
        gold += amount;
        //TODO: CHanger l'affichage
    }

    public void AddStuff(int amount)
    {
        stuff += amount;
        if(stuff > stuffMax) { stuff = stuffMax; }
        //TODO: CHanger l'affichage
    }

    public void AddMember(CharacterData chr)
    {
        team.Add(chr);
        UpdateBeerConsuption();
        //TODO: CHanger l'affichage
    }

    public void KillMember(int amount)
    {
        int mb;
        while (amount > 0)
        {
            mb = Random.Range(0, team.Count);
            team.RemoveAt(mb);
        }
        UpdateBeerConsuption();
        //TODO: CHanger l'affichage
    }

    public void ConsumeBeer()
    {
        //TODO: Plus de bière
        //TODO: Plus d'item
        beer -= beerConsumed;
    }

    private void UpdateBeerConsuption()
    {
        int cnt = 0;
        foreach (CharacterData item in team)
        {
            cnt += item.beerConsumption;
        }
        beerConsumed = cnt;
    }

    public void AddArtefact(Artefact artefact)
    {
        artefacts.Add(artefact);
    }
}
