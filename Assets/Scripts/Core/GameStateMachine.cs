using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private GameController gameController;

    void Start(){
        //Get GameController reference
        try{
            gameController = GameController.Singleton;
        }catch(System.Exception ex){
            Debug.Log(ex);
        }
    }

    //Update State and check if it's a state transiftion
    //Call the end of previous state and the next state start
    //Only the necessary Starts and Ends were writed
    void UpdateState(State nextState){
        
        //Simplified state machine

        State currentState = gameController.state;
        if(nextState != currentState){

            switch(nextState){
                case State.playersTurn : PlayersTurnStart();
                break;
                case State.battling : BattingTurnStart();
                break;
            }

        }

        gameController.state = nextState;

    }

    void PlayersTurnStart(){
        gameController.PlayersDrawCards();
    }

    void BattingTurnStart(){
        //Invoke the end of this state after specified time
        gameController.RemoveSelectedCards();
        Invoke(nameof(BattingTurnEnd), gameController.idleGameTime);
    }

    void BattingTurnEnd(){
        gameController.CheckRoundPoint();
        gameController.ResetSelectedCards();

        //If a player has more than 2 points and it's not tied, the player wins
        //If the sum of player points are greater than 3 so one of the players has more than 2 points
        if(gameController.playerPoint[0] + gameController.playerPoint[1] > 3 && gameController.playerPoint[0] != gameController.playerPoint[1]){
            int winnedId = gameController.playerPoint[0] > gameController.playerPoint[1] ? 0 : 1;
            gameController.FinishGame(winnedId);
            UpdateState(State.finished);
        }
    }
}
