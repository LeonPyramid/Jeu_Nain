using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameParameters parameters;
    public static GameManager Instance { get; private set; }
    public int depht { get; private set; }
    public Area actualArea;
    void Awake()
    {
        depht = 0;
        Instance = this;
        actualArea = parameters.Zone1;
        //TODO: Envoyer le dialogue de début au dialogGest
    }

    public void EnterRoom()
    {
        float roomProba = Random.value;
        if (actualArea.shopProba > roomProba)
        {

        }
    }

    public void leftClick()
    {

    }

    public void RightClick()
    {

    }
}
