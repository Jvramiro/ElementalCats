using UnityEngine;
public enum CardType { fire, ice, water }

[System.Serializable]
public class Card {
    public string title;
    public string text;
    public int value;
    public CardType type;
    public Sprite image;
    public int handCardId;

}
