using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameAI : MonoBehaviour, IGameAI
{
    public List<int> playerTypeHistory = new List<int>();

    public void RoundHandler(int winner, int usedType, int playerType){
        if(playerType != -1){
            playerTypeHistory.Add(playerType);
        }
    }

    public void AI_TurnHandler(int currentPoints, int playerPoints, int[] cardTypes, int[] cardValues){

        //Set all parameters
        int typeHint = 0;
        int selectedType = 0;
        int nextCard = 0;
        
        //Simple logic that use the last card type to presume the next hint
        //If the player has used the same card in the last two turns
        //the algorithm has a high chance of intervening by playing against this card's weakness

        if(playerTypeHistory.Count > 2){
            if(playerTypeHistory.ElementAt(playerTypeHistory.Count - 1) == playerTypeHistory.ElementAt(playerTypeHistory.Count - 2)){
                typeHint = Random.Range(1,11) < 8 ? GetTypeWeakness(playerTypeHistory.Count) : Random.Range(0,3);
            }
        }
        //If there's a last card stored, the algorithm has a moderate chance of intervening
        //sending a card with effectiveness against the last card
        else if(playerTypeHistory.Count > 1){
            typeHint = Random.Range(1,11) < 6 ? GetEffectiveness(playerTypeHistory.Count) : Random.Range(0,3);
        }
        //Random hint if there's not a history
        else{
            typeHint = Random.Range(0,3);
        }

        //Check if has type, case not get some avaiable type
        selectedType = cardTypes.Contains(typeHint) ? typeHint : cardTypes[Random.Range(0, cardTypes.Count())];

        //Moderate chances to choose the greater value between the same type cards
        bool decideByValue = Random.Range(1,11) < 6;
        if(decideByValue){
            for(int i = 0; i < cardTypes.Count(); i++){
                if(cardTypes[i] == selectedType && cardValues[i] > cardValues[nextCard]){
                    nextCard = i;
                }
            }
        }
        else{
            List<int> filteredByType = new List<int>();
            for(int i = 0; i < cardTypes.Count(); i++){
                if(cardTypes[i] == selectedType){
                    filteredByType.Add(i);
                }
            }

            nextCard = filteredByType[Random.Range(0, filteredByType.Count())];
        }

        string debugMessage = $"AI decides that the {(CardType)typeHint} type is the better action";
        debugMessage += typeHint != nextCard ? ", but had to choose another" : "";
        debugMessage += decideByValue ? $" and played by the higher value" : " and played without check card value";
        Debug.Log(debugMessage);
        AI_Input(nextCard);

    }

    public void AI_Input(int handCardId){
        GameController.Singleton.PlayerAction(1, handCardId);
    }

    int GetTypeWeakness(int typeId){
        return typeId == 0 ? 2 : typeId == 1 ? 0 : 1;
    }

    int GetEffectiveness(int typeId){
        return typeId == 0 ? 1 : typeId == 2 ? 0 : 0;
    }

}
