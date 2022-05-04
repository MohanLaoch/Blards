using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public TurnManager TurnMan;
    public Player_Deck_Script EnemyDeck;
    public Player_Hand_Script EnemyHand;


    [HideInInspector] public GameObject[] PlayablePositions;
    public List<GameObject> CurrentAvailablePositions;

    private void Start()
    {
        PlayablePositions = GameObject.FindGameObjectsWithTag("EnemyPlayablePosition");
    }

    public void StartTurn()
    {
        //Choose Play Position
        for (int i = 0; i < PlayablePositions.Length; i++)
        {
            if (PlayablePositions[i].GetComponent<PlayablePosition>().CardInPlay == false)
            {
                CurrentAvailablePositions.Add(PlayablePositions[i]);
            }
        }
        GameObject chosenPosition;
        if (CurrentAvailablePositions.Count != 0) { chosenPosition = CurrentAvailablePositions[Random.Range(0, CurrentAvailablePositions.Count)]; }
        else { chosenPosition = null; }
        StartCoroutine(Turn(chosenPosition));
    }

    IEnumerator Turn(GameObject chosenPosition)
    {
        EnemyDeck.Draw();

        yield return new WaitForSeconds(2);

        if (chosenPosition != null)
        {
            GameObject chosenCard = EnemyHand.CardsInHand[Random.Range(0, EnemyHand.CardsInHand.Count)];

            //Play Card
            chosenPosition.GetComponent<PlayablePosition>().CardInPlay = true;
            chosenCard.transform.position = chosenPosition.transform.position;
            chosenCard.GetComponent<BoxCollider2D>().enabled = false;

            int Direction = Random.Range(0, 1);
            chosenCard.GetComponent<CardValues>().FlipCard(Direction);

            for (int j = 0; j < EnemyHand.CardsInHand.Count; j++)
            {
                if (EnemyHand.CardsInHand[j] == chosenCard.gameObject)
                {
                    EnemyHand.CardsInHand.RemoveAt(j);
                    EnemyHand.ResetHand();
                    break;
                }
            }

            yield return new WaitForSeconds(2);
        }
        else
        {
            //Cant Play Cards
        }

        //Use Cards In Play Code Here


        //End Turn
        CurrentAvailablePositions.Clear();
        TurnMan.ChangeTurn();
    }
}
