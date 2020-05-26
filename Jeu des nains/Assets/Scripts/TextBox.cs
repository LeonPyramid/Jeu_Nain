using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.


public class TextBox : MonoBehaviour, IPointerDownHandler
{
    public static TextBox Instance { get; private set; }
    public Text textArea;
    private bool disabled = false;
    private List<string> text;
    private bool isImage;
    //Boutons texte
    private string leftText;
    private string rightText;
    public TextButton leftTextButton;
    public TextButton rightTextButton;
    //Boutons image
    private Sprite leftImage;
    private Sprite rightImage;
    public ImageButton leftImageButton;
    public ImageButton rightImageButton;

    private List<Button> buttons = new List<Button>();

    public void Awake()
    {
        Instance = this;
        buttons.Add(leftTextButton.button);
        buttons.Add(rightTextButton.button);
        buttons.Add(leftImageButton.button);
        buttons.Add(rightImageButton.button);
    }

    public void AddText (List<string> str,bool isImg,string leftTxt=null,string rightTxt=null, Sprite leftImg = null, Sprite rightImg = null)
    {
        text = str;
        isImage = isImg;
        if (isImg)
        {
            leftImage = leftImg ;
            rightImage = rightImg;
        }
        else
        {
            leftText = leftTxt;
            rightText = rightTxt;
        }
        UpdateDialog();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateDialog();
    }

    private void UpdateDialog()
    {
        textArea.text = text[0];
        text.RemoveAt(0);
        if(text.Count > 1 && !disabled)
        {
            DisableButtons();
        }
        else if(text.Count == 1)
        {
            EnableButtons();
        }
    }

    public void DisableButtons()
    {
        disabled = true;
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
    }

    public void EnableButtons()
    {
        disabled = false;
        //Fait apparaite les bons boutons et disparaitre les autres
        leftTextButton.button.gameObject.SetActive(!isImage);
        rightTextButton.button.gameObject.SetActive(!isImage);
        leftImageButton.button.gameObject.SetActive(isImage);
        rightImageButton.button.gameObject.SetActive(isImage);

        foreach (var item in buttons)
        {
            item.interactable = true;
        }
        if (isImage)
        {
            leftImageButton.image.sprite = leftImage;
            rightImageButton.image.sprite = rightImage;
        }
        else
        {
            leftTextButton.text.text = leftText;
            rightTextButton.text.text = rightText;
        }
    }
}

[System.Serializable]
public class TextButton
{
    public Text text;
    public Button button;
}

[System.Serializable]
public class ImageButton
{
    public Image image;
    public Button button;
}