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
    [SerializeField] private CardTooltipController cardTooltip;
    [SerializeField] private SO_CardTemplate cardTemplate;
    private GameController gameController;
    private List<Card> playerHand;
    private int mouseSelection = 0, lastSelection = 0;

    //Player identifier, 0 or 1
    [HideInInspector] public int playerId;

    //Get StartGame by backend event
    void OnEnable(){
        if(GameObject.FindObjectOfType<GameEvents>() != null){ GameObject.FindObjectOfType<GameEvents>().StartGame += StartGame; }
    }
    void OnDisable(){
        if(GameEvents.Singleton != null){ GameEvents.Singleton.StartGame -= StartGame; }
    }

    void StartGame(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch{
            Debug.Log("GameController does not exist to manage the game");
            return;
        }

        //Initializing the playerHand
        playerHand = gameController.playerHand[0].ToList();
        UpdateCardsUI();

        //Start Hand
        animator.Play("HandStart");

    }

    void FixedUpdate(){
        if(playerHand == null){ return; }

        CheckMouseSelection();
    }

    void CheckMouseSelection(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("UpdatingCard")){ return; }

        var getCard = cardsUI.FirstOrDefault(c => c.selected);

        mouseSelection = getCard != null ? getCard.canvasId : 0;
        animator.SetInteger("mouseSelection", mouseSelection);
    }

    public void UpdateHand(int handCardId){
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("HandUpdate") || animator.GetCurrentAnimatorStateInfo(0).IsName("HandStart")){ return; }

        animator.Play($"HandUpdate_0{handCardId}");
        playerHand = gameController.playerHand[0].ToList();

    }

    public void UpdateCardsUI(){

        var getCards = gameController.playerHand[0];

        for(int i = 0; i < getCards.Count; i++){
            if(i <= getCards.Count){

                cardsUI[i].title.text = getCards[i].title;
                cardsUI[i].text.text = getCards[i].text;
                cardsUI[i].value.text = getCards[i].value.ToString();
                cardsUI[i].image.sprite = getCards[i].image;

                cardsUI[i].background.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == getCards[i].type).cardTemplate ?? null;
                cardsUI[i].type.sprite = cardTemplate.CardTemplates.FirstOrDefault(t => t.type == getCards[i].type).cardIcon ?? null;

            }
            else{
                Debug.Log("uai");
            }
        }
    }

    public void UpdateTooltipCardUI(){
        var getCard = cardsUI.FirstOrDefault(c => c.selected);
        if(getCard != null){
            cardTooltip.UpdateTooltipCardUI(getCard);
        }
    }

    public void ResetTooltipCardUI(){
        cardTooltip.ResetTooltipCardUI();
    }

}
