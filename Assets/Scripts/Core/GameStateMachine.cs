using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private GameEvents gameEvents;
    private GameController gameController;

    void Start(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch(System.Exception ex){
            Debug.Log(ex);
        }
    }

    //Event Handlers
    void OnEnable(){
        if(GameObject.FindObjectOfType<GameEvents>() == null){ return; }

        GameObject.FindObjectOfType<GameEvents>().BattlingStart += CallBattleState;
        GameObject.FindObjectOfType<GameEvents>().FinishGame += CallFinishedState;
    }
    void OnDisable(){
        if(GameEvents.Singleton == null){ return; }

        GameEvents.Singleton.BattlingStart -= CallBattleState;
        GameEvents.Singleton.FinishGame -= CallFinishedState;
    }
    void CallBattleState(){
        UpdateState(State.battling);
    }
    void CallPlayersTurnState(){
        UpdateState(State.playersTurn);
    }
    void CallFinishedState(){
        UpdateState(State.finished);
    }

    //Update State and check if it's a state transiftion
    //Call the end of previous state and the next state start
    //Only the necessary Starts and Ends were writed
    void UpdateState(State nextState){
        
        //Simplified state machine

        State currentState = gameController.state;
        if(nextState != currentState){

            switch(nextState){
                case State.playersTurn : PlayersTurnState_Start();
                break;
                case State.battling : BattingState_Start();
                break;
            }

            switch(currentState){
                case State.battling : BattingState_End();
                break;
            }

        }


        gameController.state = nextState;

    }

    void PlayersTurnState_Start(){
        
    }

    void BattingState_Start(){
        //Invoke the end of this state after specified time
        gameController.PlayersDrawCards();

        Invoke(nameof(CallPlayersTurnState), gameController.idleGameTime);
    }

    void BattingState_End(){
        gameController.FinishRound();

        //Call the function that check if the game is finished
        gameController.CheckFinishGame();
    }

}
