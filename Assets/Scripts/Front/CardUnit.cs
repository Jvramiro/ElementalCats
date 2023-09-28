using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUnit : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text text;
    public Image background;
    public Image image;
    public Image type;

    public int canvasId;
    public bool selected;

    public void SetCardUnit(string title, string text, Sprite background, Sprite image, Sprite type){
        this.title.text = title;
        this.text.text = text;
        this.background.sprite = background;
        this.image.sprite = image;
        this.type.sprite = type;
    }

}
