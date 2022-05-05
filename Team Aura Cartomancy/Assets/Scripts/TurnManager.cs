using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    public bool IsFirstTurn = true;

    public bool PlayerDrawnForTurn;
    public bool PlayerCardPlayedForTurn;

    public GameObject EndTurnButton;
    public GameObject DrawButton;

    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();
    }

    public void ChangeTurn()
    {
        if(IsFirstTurn == true)
        {
            IsFirstTurn = false;
        }

        CardValues[] AllCards = FindObjectsOfType<CardValues>();
        for(int i = 0; i < AllCards.Length; i++)
        {
            AllCards[i].CardAttackedForTurn = false;
        }

        if (IsPlayerTurn == true)
        {
            EndTurnButton.SetActive(false);
            DrawButton.SetActive(false);
            IsPlayerTurn = false;
            PlayerDrawnForTurn = true;
            PlayerCardPlayedForTurn = true;
            enemyAI.StartTurn();
        }
        else
        {
            EndTurnButton.SetActive(true);
            DrawButton.SetActive(true);
            IsPlayerTurn = true;
            PlayerDrawnForTurn = false;
            PlayerCardPlayedForTurn = false;
        }
    }

}
