using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataController;
using SceneControllerService;
public class MenuButtons : MonoBehaviour{

    [SerializeField] private GameObject bt_Start, bt_Back, levels;
    private bool showMap = false;
    private int selection = 0;

    public void ShowMap(){
        showMap = !showMap;
        bt_Start.SetActive(!showMap);
        bt_Back.SetActive(showMap);
        levels.SetActive(showMap);
        MenuController.Singleton.UpdateYarnWorld(showMap);
    }
    public void SettingsMenu(){

    }
    public void ResetData(){
        Data.ResetData();
    }

    public void StartLevel_01(){
        Data.SetAILevel(0);
        StartCoroutine(SceneController.LoadSceneAsync("TutorialScene"));
    }
    public void StartLevel_02(){
        Data.SetAILevel(1);
        StartCoroutine(SceneController.LoadSceneAsync("GameScene"));
    }
}
