using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    void OnEnable() {
        if(GameObject.FindObjectOfType<GameEvents>() == null){ return; }
        GameObject.FindObjectOfType<GameEvents>().UpdatePoints += OpponentDamage;
    }
    void OnDisable() {
        if(GameEvents.Singleton == null){ return; }
        GameEvents.Singleton.UpdatePoints -= OpponentDamage;
    }

    [SerializeField] private Animator opponentAnim;
    void OpponentDamage(){
        if(opponentAnim == null || GameController.Singleton == null){ return; }

        if(GameController.Singleton.lastRoundOwner == 0){
            opponentAnim.Play("OpponentDamage");
        }
    }
}
