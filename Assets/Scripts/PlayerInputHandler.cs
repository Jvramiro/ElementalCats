using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    
    #region Singleton
    public static PlayerInputHandler Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    //Offline system

    public void Input_SelectCard(int handCardId){
        GameController.Singleton.PlayerAction(VisualController.Singleton.playerId, handCardId);
    }
}
