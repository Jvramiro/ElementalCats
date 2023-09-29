using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    [SerializeField] private Animator opponentAnim;
    [SerializeField] private CameraBobbing cameraMovement;

    void OnEnable() {
        if(GameObject.FindObjectOfType<GameEvents>() == null){ return; }
        GameObject.FindObjectOfType<GameEvents>().UpdatePoints += OpponentDamage;
    }
    void OnDisable() {
        if(GameEvents.Singleton == null){ return; }
        GameEvents.Singleton.UpdatePoints -= OpponentDamage;
    }

    void OpponentDamage(){
        if(opponentAnim == null || GameController.Singleton == null){ return; }

        switch(GameController.Singleton.lastRoundOwner){
            case 0 : opponentAnim.Play("OpponentDamage");;
            break;
            case 1 : cameraMovement.StartShake();
            break;
        }
    }
}
