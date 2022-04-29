using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;

    public bool PlayerDrawnForTurn;
    public bool PlayerCardPlayedForTurn;

    public bool EnemyDrawnForTurn;
    public bool EnemyCardPlayedForTurn;

    public GameObject EndTurnButton;
    public GameObject DrawButton;

    public void ChangeTurn()
    {
        if(IsPlayerTurn == true)
        {
            EndTurnButton.SetActive(false);
            DrawButton.SetActive(false);
            IsPlayerTurn = false;
            PlayerDrawnForTurn = true;
            PlayerCardPlayedForTurn = true;
            EnemyDrawnForTurn = false;
            EnemyCardPlayedForTurn = false;
        }
        else
        {
            EndTurnButton.SetActive(true);
            DrawButton.SetActive(true);
            IsPlayerTurn = true;
            PlayerDrawnForTurn = false;
            PlayerCardPlayedForTurn = false;
            EnemyDrawnForTurn = true;
            EnemyCardPlayedForTurn = true;
        }
    }

}
