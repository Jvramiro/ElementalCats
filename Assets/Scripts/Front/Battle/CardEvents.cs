using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private CardUnit cardUnit;

    public void OnPointerEnter(PointerEventData eventData){
        if(GameController.Singleton == null){ return; }
        cardUnit.selected = GameController.Singleton.state == State.playersTurn;
    }

    public void OnPointerExit(PointerEventData eventData){
        if(GameController.Singleton == null){ return; }
        if(cardUnit.selected){
            cardUnit.selected = false;
        }
        VisualController.Singleton.ResetTooltipCardUI();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(eventData.pointerId == -1) {

            if(GameController.Singleton.state != State.playersTurn){ return; }
            
            cardUnit.selected = false;
            VisualController.Singleton.ResetTooltipCardUI();
            VisualController.Singleton.UpdateHand(cardUnit.canvasId);
            PlayerInputHandler.Singleton.Input_SelectCard(cardUnit.canvasId - 1);
        }
        else if(eventData.pointerId == -2) {
            VisualController.Singleton.UpdateTooltipCardUI();
        }
    }
}
