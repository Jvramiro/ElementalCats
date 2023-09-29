using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControllerService{
    public class SceneController : MonoBehaviour{
        public static IEnumerator LoadSceneAsync(string sceneName){
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone){
                yield return null;
            }
        }
    }
}
