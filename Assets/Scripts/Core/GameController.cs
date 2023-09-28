using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum State{none, awaiting, playersTurn, battling, finished}
public enum Player{none, playerOne, playerTwo}
public class GameController : MonoBehaviour
{

    #region Singleton
    public static GameController Singleton;
    void Awake() {
        if (Singleton != null && Singleton != this){
            Destroy(this.gameObject);
        }
        else{
            Singleton = this;
        }
    }
    #endregion

    [Tooltip("Idle time to show the cards awaiting to next turn")]
    public float idleGameTime = 5f;
    [SerializeField] private SO_Card cardData;
    //Deck for both players
    private List<Card>[] deck = new List<Card>[2];
    [HideInInspector] public State state = State.awaiting;

    //Player Points, to increase each round and check if the player reached tree
    [HideInInspector] public int[] playerPoint = new int[2];

    //Card Ids of each player
    [HideInInspector] public List<Card>[] playerHand = new List<Card>[2];

    //Current turn selected card
    private Card?[] selectedCard = new Card[2]; 
    private Player winner = Player.none;


    //Offline and Multiplayer region
    private IGameAI gameAI = new GameAI();
    private bool isMultiplayer = false;
    private int[] lastType = new int[2]{ -1, -1 };

    void Start(){
        if(!isMultiplayer){
            StartGame();
        }
    }

    void StartGame(){
        
        //Initializing the variables to use
        //Deck is initiated in ResetDecks
        playerHand[0] = new List<Card>();
        playerHand[1] = new List<Card>();

        //Setting the cards according SO
        ResetDecks();
        //Reseting Points
        playerPoint = new int[2]{0,0};
        
        //Setting players turn
        state = State.playersTurn;

        for(int i = 0; i < 5; i++){
            AddCard(0);
            AddCard(1);
        }

    }

    void ResetDecks(){
        deck = new List<Card>[2];
        deck[0] = cardData.Cards.ToList();
        deck[1] = cardData.Cards.ToList();
    }

    void AddCard(int playerId){

        //Check if there's space to add one card, max hand length is 5
        if(playerHand[playerId].Count >= 5){
            Debug.Log($"There's no space in Player {playerId} hand to add card");
        }
        //Check if there's card in deck
        //Reseting de deck if there's no card
        if(deck[playerId].Count <= 0){
            deck[playerId] = cardData.Cards.ToList();
        }

        //Get a random card from deck
        int cardId = Random.Range(0,deck[playerId].Count);
        Card card = deck[playerId][cardId];

        //Set card on hand and remove from deck
        deck[playerId].RemoveAt(cardId);
        playerHand[playerId].Add(card);


    }

    void RemoveCard(int playerId, int handCardId){

        //Check if the card by index is not null
        if(playerHand[playerId].ElementAtOrDefault(handCardId) == null){
            Debug.Log($"Card at {handCardId} position does not exist");
        }

        playerHand[playerId].RemoveAt(handCardId);

    }

    public void RemoveSelectedCards(){
        // --------> To improve later
        playerHand[0].RemoveAt(selectedCard[0].handCardId);
        playerHand[1].RemoveAt(selectedCard[1].handCardId);
    }

    public void PlayersDrawCards(){
        AddCard(0);
        AddCard(1);
    }

    public void PlayerAction(int playerId, int handCardId){

        //Check if it's a correct player Id
        if(playerId != 0 && playerId != 1){
            Debug.Log("It's not a correct player Id");
            return;
        }

        //Check if it's players Turn
        if(state != State.playersTurn){
            Debug.Log("It's not turn to select card");
            return;
        }

        //Check if it's a avaiable hand card Id
        if(handCardId < 0 || handCardId >= playerHand[playerId].Count){
            Debug.Log($"Invalid hand card Id {handCardId} {playerHand[playerId].Count}");
            return;
        }

        //Get card info to selected card and remove from hand
        selectedCard[playerId] = playerHand[playerId][handCardId];
        selectedCard[playerId].handCardId = handCardId;

        lastType[playerId] = (int)selectedCard[playerId].type;

        if(!isMultiplayer){
            Call_IA();
        }

    }

    int GetTurnPointOwner(){

        //If the both cards have the same element check the biggest value
        //If the both cards have the same value, return a invalid number to set tied turn
        if(selectedCard[0].type == selectedCard[1].type){
            return selectedCard[0].value == selectedCard[1].value ? 3 : selectedCard[0].value > selectedCard[1].value ? 0 : 1;
        }

        int toReturn = 0;
        //Checking the winner based on elements
        //Fire wins against ice
        //Ice wins against water
        //Water wind against fire
        switch(selectedCard[0].type){
            case CardType.fire : toReturn = selectedCard[1].type == CardType.ice ? 0 : 1;
            break;
            case CardType.ice : toReturn = selectedCard[1].type == CardType.water ? 0 : 1;
            break;
            case CardType.water : toReturn = selectedCard[1].type == CardType.fire ? 0 : 1;
            break;
            default : throw new System.Exception("Error while getting the turn point Owner");
        }

        return toReturn;

    }

    public void ResetSelectedCards(){
        selectedCard[0] = null;
        selectedCard[1] = null;
    }

    void Call_IA(){
        gameAI.AI_TurnHandler(
            playerPoint[1], playerPoint[0],
            playerHand[1].Select(c => (int)c.type).ToArray(),
            playerHand[1].Select(c => c.value).ToArray()
        );
    }

    public void FinishRound(){

        int pointOwner = GetTurnPointOwner();

        //Checking tied turn
        if(pointOwner != 0 && pointOwner != 1){
            return;
        }

        playerPoint[pointOwner]++;

        if(!isMultiplayer){
            gameAI.RoundHandler(pointOwner, lastType[0], lastType[1]);
        }

    }

    public void FinishGame(int winnerId){
        winner = winnerId == 0 ? Player.playerOne : Player.playerTwo;
    }

}
