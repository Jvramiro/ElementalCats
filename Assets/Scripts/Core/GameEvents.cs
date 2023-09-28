using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    #region Singleton
    public static GameEvents Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    public delegate void VoidEvent();
    public VoidEvent StartGame, BattlingStart, UpdatePoints, FinishGame;
}
