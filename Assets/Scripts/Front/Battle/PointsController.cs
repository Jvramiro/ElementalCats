using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PointsController : MonoBehaviour
{

    [SerializeField] private TMP_Text[] playerPoint_01 = new TMP_Text[3], playerPoint_02 = new TMP_Text[3];

    void OnEnable(){
        if(GameObject.FindObjectOfType<GameEvents>() == null){ return; }
        
        GameObject.FindObjectOfType<GameEvents>().UpdatePoints += UpdatePoints;
    }
    void OnDisable(){
        if(GameEvents.Singleton == null){ return; }
        
        GameEvents.Singleton.UpdatePoints -= UpdatePoints;
    }

    void UpdatePoints(){
        if(GameController.Singleton == null){
            Debug.Log("There's no GameController to manage the game");
            return;
        }

        for(int pointId = 0; pointId < 3; pointId++){
            playerPoint_01[pointId].text = GameController.Singleton.playerPoint[0,pointId].ToString();
            playerPoint_02[pointId].text = GameController.Singleton.playerPoint[1,pointId].ToString();
        }
    }
}
