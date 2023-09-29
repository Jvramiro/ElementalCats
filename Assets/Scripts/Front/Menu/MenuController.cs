using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataController;
using TMPro;

public class MenuController : MonoBehaviour
{
    
    #region Singleton
    public static MenuController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Animator yarnWorldAnim;
    void Start(){
        pointsText.text = $"catnips: {Data.GetPoints().ToString("00")}";
    }

    public void UpdateYarnWorld(bool showMap){
        string animName = showMap ? "YarnWorldMap" : "YarnWorldBack";
        yarnWorldAnim.Play(animName);
    }

}
