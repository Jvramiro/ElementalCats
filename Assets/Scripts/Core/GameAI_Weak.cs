using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class GameAI_Weak : MonoBehaviour, IGameAI
{
    public List<int> playerTypeHistory = new List<int>();

    public void RoundHandler(int winner, int usedType, int playerType){
        if(playerType != -1){
            playerTypeHistory.Add(playerType);
        }
    }

    public void AI_TurnHandler(int[] cardTypes, int[] cardValues){

        //Set all parameters
        int typeHint = 0;
        int selectedType = 0;
        int nextCard = 0;
        
        //Simple logic that use the last player card type and get the effectiveness
        //Case there's no history get a random card
        if(playerTypeHistory.Count > 0){
            typeHint = GetEffectiveness(playerTypeHistory[playerTypeHistory.Count - 1]);
        }


        //Check if has type, case not get some avaiable type
        selectedType = cardTypes.Contains(typeHint) ? typeHint : cardTypes[Random.Range(0, cardTypes.Count())];

        List<int> filteredByType = new List<int>();
            for(int i = 0; i < cardTypes.Count(); i++){
                if(cardTypes[i] == selectedType){
                    filteredByType.Add(i);
                }
            }

        nextCard = filteredByType[Random.Range(0, filteredByType.Count())];

        string debugMessage = $"AI decides that the {(CardType)typeHint} type is the better action";
        debugMessage += typeHint != selectedType ? ", but had to choose another" : "";
        Debug.Log(debugMessage);
        AI_Input(nextCard);

    }

    public void AI_Input(int handCardId){
        GameController.Singleton.PlayerAction(1, handCardId);
    }

    int GetWeakness(int typeId){
        return typeId == 0 ? 2 : typeId == 1 ? 0 : 1;
    }

    int GetEffectiveness(int typeId){
        return typeId == 0 ? 1 : typeId == 2 ? 0 : 0;
    }
}
