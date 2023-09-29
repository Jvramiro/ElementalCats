using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneControllerService;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private string sceneToUpdate;
    public void UpdateScene(){
        StartCoroutine(SceneController.LoadSceneAsync(sceneToUpdate));
    }
}
