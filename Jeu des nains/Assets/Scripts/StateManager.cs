using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static StateManager _instance;
    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<StateManager>();
            return _instance;
        }
    }
    private GameParameters parameters;
    public List<CharacterData> team { get; private set; }
    public List<Artefact> artefacts { get; private set; }
    public int beer { get; private set; }
    public int beerMax { get; private set; }
    public int stuff { get; private set; }
    public int stuffMax { get; private set; }
    public int gold { get; private set; }
    private int beerConsumed;
    private void Awake()
    {
        _instance = this; 
    }
    void Start()
    {
        parameters = GameManager.Instance.parameters;
        team = parameters.team;
        StuffUIManager.Instance.UpdtateTeam(team);
        UpdateStats();
        artefacts = new List<Artefact>();
        AddBeer (parameters.beer);
        AddStuff (parameters.stuff);
        gold = parameters.gold;
        StuffUIManager.Instance.UpdateGold(gold);
    }

    public void AddBeer(int amount)
    {
        beer += amount;
        if (beer > beerMax) { beer = beerMax; }
        StuffUIManager.Instance.UpdateBeer(beer, beerMax);
        //TODO: Plus de bière
    }

    public void AddGold(int amount)
    {
        gold += amount;
        StuffUIManager.Instance.UpdateGold(gold);
    }

    public void AddStuff(int amount)
    {
        stuff += amount;
        if(stuff > stuffMax) { stuff = stuffMax; }
        StuffUIManager.Instance.UpdateStuff(stuff, stuffMax);
    }

    public void AddMember(CharacterData chr)
    {
        team.Add(chr);
        UpdateStats();
        StuffUIManager.Instance.UpdtateTeam(team);
    }

    public void KillMember(int amount)
    {
        int mb;
        while (amount > 0)
        {
            mb = Random.Range(0, team.Count);
            if (!team[mb].immortal) { team.RemoveAt(mb); }
        }
        UpdateStats();
        StuffUIManager.Instance.UpdtateTeam(team);
        //TODO:Game Over
    }

    public void ConsumeBeer(float mult = 1)
    {
        //TODO: Plus de bière
        //TODO: Plus d'item
        beer -= Mathf.FloorToInt(beerConsumed*mult);
        StuffUIManager.Instance.UpdateBeer(beer, beerMax);
    }
    public void BeerReset()
    {
        beer = 0;
        StuffUIManager.Instance.UpdateBeer(beer, beerMax);
    }

    public void StuffReset()
    {
        stuff = 0;
        StuffUIManager.Instance.UpdateStuff(stuff, stuffMax);
    }
    private void UpdateStats()
    {
        int cnt = 0;
        foreach (CharacterData item in team)
        {
            cnt += item.beerConsumption;
        }
        beerConsumed = cnt;
        stuffMax = team.Count * parameters.maxStuffByMember;
        beerMax = team.Count * parameters.maxBeerByMember;
        StuffUIManager.Instance.UpdateStuff(stuff, stuffMax);
        StuffUIManager.Instance.UpdateBeer(beer, beerMax);
    }


    public void AddArtefact(Artefact artefact)
    {
        artefacts.Add(artefact);
    }
}
