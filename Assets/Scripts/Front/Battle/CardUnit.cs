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
    public TMP_Text value;
    public Image background;
    public Image image;
    public Image type;
    public int typeId;

    public int canvasId;
    public bool selected;
    
}
