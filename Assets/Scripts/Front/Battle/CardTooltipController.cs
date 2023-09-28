using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTooltipController : MonoBehaviour
{
    [SerializeField] private CardUnit cardUnit;
    [SerializeField] private Animator animator;

    public void UpdateTooltipCardUI(CardUnit updatedCard){
        cardUnit.title.text = updatedCard.title.text;
        cardUnit.text.text = updatedCard.text.text;
        cardUnit.image.sprite = updatedCard.image.sprite;
        cardUnit.background.sprite = updatedCard.background.sprite;
        cardUnit.type.sprite = updatedCard.type.sprite;
        animator.Play("TooltipCardStart");
    }

    public void ResetTooltipCardUI(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("TooltipCardStart")){
            animator.Play("TooltipCardEnd");
        }
    }


}
