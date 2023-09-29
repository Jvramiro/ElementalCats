using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class FinishGameController : MonoBehaviour
{
    [SerializeField] private TMP_Text finishMessage;
    [SerializeField] private Animator FinishUIAnim;

    void OnEnable() {
        if(GameObject.FindObjectOfType<GameEvents>() == null){ return; }
        GameObject.FindObjectOfType<GameEvents>().FinishGame += FinishGame;
    }
    void OnDisable() {
        if(GameEvents.Singleton == null){ return; }
        GameEvents.Singleton.FinishGame -= FinishGame;
    }

    void FinishGame(){
        if(FinishUIAnim != null && GameController.Singleton != null){
            finishMessage.text = GameController.Singleton.winner == Player.playerTwo ? "you lose" : "you win";
            FinishUIAnim.Play("FinishUI");
        }
        else{
            Debug.Log("Missing parameters Finish Animation and GameController Singleton");
        }
    }
}
