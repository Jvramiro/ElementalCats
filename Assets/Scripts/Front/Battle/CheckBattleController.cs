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
            cardUnits[i].image.sprite = updatedCard.image;
            cardUnits[i].background.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == updatedCard.type).cardTemplate ?? null;
            cardUnits[i].type.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == updatedCard.type).cardIcon ?? null;
        }
    }

    public void PlayAnimation(){
        animator.Play("CheckBattleStart");
    }
}
