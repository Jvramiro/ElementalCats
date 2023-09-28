using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private CardUnit cardUnit;

    public void OnPointerEnter(PointerEventData eventData){
        cardUnit.selected = true;
    }

    public void OnPointerExit(PointerEventData eventData){
        cardUnit.selected = false;
        VisualController.Singleton.ResetTooltipCardUI();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(eventData.pointerId == -1) {

        }
        else if(eventData.pointerId == -2) {
            VisualController.Singleton.UpdateTooltipCardUI();
        }
    }
}
