using System.Collections;
using System.Collections.Generic;
using SceneControllerService;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void NextTutorialAnim(){
        for(int i = 1; i <= 8; i++){
            if(animator.GetCurrentAnimatorStateInfo(0).IsName($"TutorialAnimation_0{i}")){
                if(i == 8){
                    FinishTutorial();
                    break;
                }
                animator.Play($"TutorialAnimation_0{i+1}");
                break;
            }
        }
    }

    void Update(){
        if(Input.anyKey && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f){
            NextTutorialAnim();
        }
    }

    void FinishTutorial(){
        StartCoroutine(SceneController.LoadSceneAsync("GameScene"));
    }
}
