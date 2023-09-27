using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System;
using System.Linq;

public class CardsImport : MonoBehaviour
{
    [SerializeField] private SO_Card cardData;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private bool update = false;

    void OnValidate(){
        if(update){
            update = false;
            UpdateCards();
        }
    }

    void UpdateCards(){
        
        var lines = csvFile.text.Split('\n').ToList();
        //Dropping the header
        lines.RemoveAt(0);

        cardData.Cards = new List<Card>();

        for(int i = 0; i < 3; i++){
            foreach(var current in lines){
                
                string[] values = current.Split(',');
                Card card = new Card{
                    title = values[0].ToString(),
                    text = values[1].ToString(),
                    value = int.Parse(values[2]),
                    type = (CardType)i
                };

                cardData.Cards.Add(card);
            }
        }

    }

}
