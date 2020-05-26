using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    public static FightHandler Instance { get; private set; }
    public int CalculateFight(List<CharacterData> ennemies)
    {
        int teamPower = 0;
        int ennemyPower = 0;
        float killGap;
        float winProba;
        foreach (var item in StateManager.Instance.team)
        {
            teamPower += item.power;
        }
        foreach (var item in StateManager.Instance.artefacts)
        {
            if(item is Weapon)
            {
                teamPower += (item as Weapon).power;
            }
        }
        foreach (var item in ennemies)
        {
            ennemyPower += item.power;
        }
        winProba = (float)teamPower / ((float)teamPower + (float)ennemyPower);
        killGap = (1f - winProba) / ((float)ennemyPower);
        float rand = Random.value;
        if (rand < winProba)
        {
            return -1;
        }
        else
        {
            rand -= winProba;
            return Mathf.FloorToInt(rand/killGap);
        }

    }
}
