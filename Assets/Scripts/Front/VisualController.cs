using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private CardUnit[] cards = new CardUnit[5];
    private GameController gameController;
    private List<Card>[] playerHand = new List<Card>[2];
    private int mouseSelection = 0;

    void Start(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch(System.Exception ex){
            Debug.Log(ex);
        }
    }

    void FixedUpdate(){
        CheckMouseSelection();
    }

    void CheckMouseSelection(){
        var getCard = cards.FirstOrDefault(c => c.selected);
        mouseSelection = getCard != null ? getCard.canvasId : 0;
        animator.SetInteger("mouseSelection", mouseSelection);
    }

    public void UpdateCards(){
        if(playerHand[0] != gameController.playerHand[0]){
            
        }

    }

    void RemoveCard(){

    }

    void AddCard(){

    }

}
