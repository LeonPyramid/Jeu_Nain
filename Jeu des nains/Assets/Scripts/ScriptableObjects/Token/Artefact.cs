using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Artefact", menuName = "Data/Artefact")]
public class Artefact : ScriptableObject
{
    public string artName;
    [TextArea(3, 10)]
    public string description;
    public Image image;
    public int price;
    //TODO:Effets de l'artefact
}


