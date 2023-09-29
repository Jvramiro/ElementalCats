using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBattleTrigger : MonoBehaviour
{
    [SerializeField] private Animator opponentAnim;
    public void OpponentDamage(){
        if(opponentAnim == null){ return; }
        opponentAnim.Play("opponentDamage");
    }
}
