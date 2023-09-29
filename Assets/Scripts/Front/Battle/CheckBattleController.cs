using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckBattleController : MonoBehaviour
{
    [SerializeField] private CardUnit[] cardUnits = new CardUnit[2];
    [SerializeField] private Animator animator;
    [SerializeField] private SO_CardTemplate cardTemplate;

    void OnEnable(){
        try{
            GameObject.FindObjectOfType<GameEvents>().BattlingStart += SetCards;
        }catch{}
    }
    void OnDisable(){
        try{
            GameObject.FindObjectOfType<GameEvents>().BattlingStart -= SetCards;
        }catch{}
    }

    public void SetCards(){
        var getCards = GameController.Singleton.selectedCard;
        for(int i = 0; i < 2; i++){
            var updatedCard = getCards[i];
            cardUnits[i].title.text = updatedCard.title;
            cardUnits[i].text.text = updatedCard.text;
            cardUnits[i].value.text = updatedCard.value.ToString();
            cardUnits[i].image.sprite = updatedCard.image;
            cardUnits[i].typeId = (int)updatedCard.type;
            cardUnits[i].background.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == updatedCard.type).cardTemplate ?? null;
            cardUnits[i].type.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == updatedCard.type).cardIcon ?? null;
        }
        PlayAnimation();
    }

    public void PlayAnimation(){
        switch(GetTurnPointFront()){
            case 0 : animator.Play("CheckBattlePlayer");
            break;
            case 1 : animator.Play("CheckBattleOpponent");
            break;
            default : animator.Play("CheckBattleDraw");
            break;
        }
    }

    int GetTurnPointFront(){

        if(cardUnits[0].typeId == cardUnits[1].typeId){
            int value_01 = 0, value_02 = 0;
            int.TryParse(cardUnits[0].value.text, out value_01);
            int.TryParse(cardUnits[1].value.text, out value_02);
            return value_01 == value_02 ? -1 : value_01 > value_02 ? 0 : 1;
        }

        int toReturn = -1;
        switch(cardUnits[0].typeId){
            case 0 : toReturn = cardUnits[1].typeId == 1 ? 0 : 1;
            break;
            case 1 : toReturn = cardUnits[1].typeId == 2 ? 0 : 1;
            break;
            case 2 : toReturn = cardUnits[1].typeId == 0 ? 0 : 1;
            break;
            default : throw new System.Exception("Error while getting the turn point Owner");
        }
        return toReturn;
    }
}
