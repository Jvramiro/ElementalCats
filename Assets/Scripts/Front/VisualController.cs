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
    [SerializeField] private CardUnit cardTooltip;
    private GameController gameController;
    private List<Card> playerHand;
    private int mouseSelection = 0;

    [SerializeField] private Sprite backgroundFire, backgroundIce, backgroundWater, backgroundNone;
    [SerializeField] private Sprite iconFire, iconIce, iconWater;

    //Player identifier, 0 or 1
    [HideInInspector] public int playerId;

    void Start(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch(System.Exception ex){
            Debug.Log(ex);
        }

        //Initializing the playerHand
        playerHand = gameController.playerHand[0].ToList();
        UpdateCardsUI();

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
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("HandUpdate") || animator.GetCurrentAnimatorStateInfo(0).IsName("HandStart")){ return; }

        for(int i = 0; i < gameController.playerHand.Count(); i++){
            if(playerHand[i] != gameController.playerHand[0][i]){
                animator.Play($"HandUpdate_0{i}");
                playerHand[i] = gameController.playerHand[0][i];
            }
        }

    }

    public void UpdateCardsUI(){

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

    public void UpdateTooltipCardUI(){
        var getCard = cardsUI.FirstOrDefault(c => c.selected);
        if(getCard != null){
            cardTooltip.title.text = getCard.title.text;
            cardTooltip.text.text = getCard.text.text;
            cardTooltip.image.sprite = getCard.image.sprite;
            cardTooltip.background.sprite = getCard.background.sprite;
            cardTooltip.type.sprite = getCard.type.sprite;
            cardTooltip.GetComponent<Animator>().Play("TooltipCardStart");
        }
    }

    public void ResetTooltipCardUI(){
        if(cardTooltip.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TooltipCardStart")){
            cardTooltip.GetComponent<Animator>().Play("TooltipCardEnd");
        }
    }

}
