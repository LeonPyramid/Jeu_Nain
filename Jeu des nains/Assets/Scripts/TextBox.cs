using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.


public class TextBox : MonoBehaviour, IPointerDownHandler
{
    private static TextBox _instance;
    public static TextBox Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<TextBox>();
            return _instance;
        }
    }
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
        _instance = this;
        buttons.Add(leftTextButton.button);
        buttons.Add(rightTextButton.button);
        buttons.Add(leftImageButton.button);
        buttons.Add(rightImageButton.button);
    }

    public void AddText (List<string> str,bool isImg,string leftTxt=null,string rightTxt=null, Sprite leftImg = null, Sprite rightImg = null)
    {
        if (text == null)
            text = new List<string>();
        text.AddRange(str);
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
        SwitchButton();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateDialog();
    }

    public void UpdateDialog()
    {
        if(text.Count > 0)
        {
            textArea.text = text[0];
            text.RemoveAt(0);
            SwitchButton();
        }
    }

    private void SwitchButton()
    {
        if (text.Count > 0 && !disabled)
        {
            DisableButtons();
        }
        else if (text.Count == 0)
        {
            EnableButtons();
        }
    }

    private void DisableButtons()
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