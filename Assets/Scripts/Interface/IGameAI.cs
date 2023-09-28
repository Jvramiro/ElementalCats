using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameAI{
    public void RoundHandler(int winner, int usedType, int playerType);
    public void AI_TurnHandler(int[] cardTypes, int[] cardValues);
    void AI_Input(int handCardId);
}
