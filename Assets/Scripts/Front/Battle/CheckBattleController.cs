using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBattleController : MonoBehaviour
{
    [SerializeField] private CardUnit[] cardUnits = new CardUnit[2];
    [SerializeField] private Animator animator;

    public void SetCards(CardUnit[] cardUnits){
        this.cardUnits = cardUnits;
    }

    public void PlayAnimation(){
        animator.Play("CheckBattleStart");
    }
}
