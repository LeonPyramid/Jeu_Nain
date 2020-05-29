using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StuffUIManager : MonoBehaviour
{
    public static StuffUIManager Instance { get; private set;}
    public GameObject teamUI;
    public GameObject teamList;
    private GridLayoutGroup grid;
    private float width;
    private Image img;
    public Text beer;
    public Text stuff;
    public Text gold;
    public List<GameObject> teamUiList;
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        img = teamUI.GetComponentInChildren<Image>();
        teamUiList = new List<GameObject>();
        grid = teamList.GetComponent<GridLayoutGroup>();
        width = teamList.GetComponent<RectTransform>().sizeDelta.x - grid.padding.left - grid.padding.right;
    }

    public void UpdtateTeam(List<CharacterData> team)
    {
        DestroyTeam();
        foreach (var item in team)
        {
            img.sprite = item.Image;
            GameObject obj = Instantiate(teamUI, teamList.transform);
            teamUiList.Add(obj);
        }
        if(teamUiList.Count < 11)
        {
            grid.spacing = new Vector2(width/(teamUiList.Count - 1),grid.spacing.y);
        }
        else if (teamUiList.Count < 21)
        {
            grid.spacing = new Vector2(width*2 / (teamUiList.Count - 1), grid.spacing.y);
        }
        else
        {
            grid.spacing = new Vector2(width*3 / (teamUiList.Count - 1), grid.spacing.y);
        }
    }

    public void DestroyTeam()
    {
        foreach (var item in teamUiList)
        {
            GameObject.Destroy(item);
        }
        teamUiList.Clear();
    }

    public void UpdateBeer(int amount,int max)
    {
        beer.text = amount.ToString() + "/" + max.ToString();
    }
    public void UpdateStuff(int amount, int max)
    {

        stuff.text = amount.ToString() + "/" + max.ToString();
    }
    public void UpdateGold(int amount)
    {

        gold.text = amount.ToString();
    }
}
