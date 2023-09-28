using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VisualController : MonoBehaviour
{

    #region Singleton
    public static VisualController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion


    [SerializeField] private Animator animator;
    [SerializeField] private CardUnit[] cardsUI = new CardUnit[5];
    private GameController gameController;
    private List<Card> playerHand;
    private int mouseSelection = 0;

    [SerializeField] private Sprite backgroundFire, backgroundIce, backgroundWater, backgroundNone;
    [SerializeField] private Sprite iconFire, iconIce, iconWater;

    void Start(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch(System.Exception ex){
            Debug.Log(ex);
        }

        //Initializing the playerHand
        playerHand = new List<Card>(){ new Card(), new Card(), new Card(), new Card(), new Card() };
    }

    void FixedUpdate(){
        CheckMouseSelection();
        CheckHandUpdate();
    }

    void CheckMouseSelection(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("UpdatingCard")){ return; }

        var getCard = cardsUI.FirstOrDefault(c => c.selected);

        mouseSelection = getCard != null ? getCard.canvasId : 0;
        animator.SetInteger("mouseSelection", mouseSelection);
    }

    void CheckHandUpdate(){
        if(playerHand != gameController.playerHand[0]){
            UpdateHand();
        }
    }

    void UpdateHand(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("HandUpdate")){ return; }

        for(int i = 0; i < playerHand.Count; i++){
            if(playerHand[i] != gameController.playerHand[0][i]){
                animator.Play($"HandUpdate_0{i}");
                playerHand[i] = gameController.playerHand[0][i];
            }
        }

    }

    public void UpdateCardsUI(){
        Debug.Log("Update");
        for(int i = 0; i < cardsUI.Length; i++){
            if(i <= playerHand.Count){

                cardsUI[i].title.text = playerHand[i].title;
                cardsUI[i].text.text = playerHand[i].text;
                cardsUI[i].image.sprite = playerHand[i].image;

                cardsUI[i].background.sprite = playerHand[i].type == CardType.fire ? backgroundFire :
                                                playerHand[i].type == CardType.ice ? backgroundIce :
                                                playerHand[i].type == CardType.water ? backgroundWater : backgroundNone;
                cardsUI[i].type.sprite = playerHand[i].type == CardType.fire ? iconFire :
                                            playerHand[i].type == CardType.ice ? iconIce :
                                            playerHand[i].type == CardType.water ? iconWater : null;

            }
        }
    }

}
