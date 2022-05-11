using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public TurnManager TurnMan;
    public Player_Deck_Script EnemyDeck;
    public Player_Hand_Script EnemyHand;


    [HideInInspector] public GameObject[] PlayablePositions;
    [HideInInspector] public GameObject[] OpponentPositions;
    public List<GameObject> CurrentAvailablePositions;

    private void Start()
    {
        PlayablePositions = GameObject.FindGameObjectsWithTag("EnemyPlayablePosition");
        OpponentPositions = GameObject.FindGameObjectsWithTag("PlayablePosition");
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
        if (CurrentAvailablePositions.Count != 0) { chosenPosition = CurrentAvailablePositions[Random.Range(0, CurrentAvailablePositions.Count - 1)]; }
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
            chosenPosition.GetComponent<PlayablePosition>().Card = chosenCard.GetComponent<CardValues>();
            chosenCard.GetComponent<CardValues>().position = chosenPosition.GetComponent<PlayablePosition>();
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
        if(TurnMan.IsFirstTurn != true)
        {
            List<CardValues> AttackingCards = new List<CardValues>();
            for (int i = 0; i < PlayablePositions.Length; i++)
            {
                PlayablePosition position = PlayablePositions[i].GetComponent<PlayablePosition>();
                if (position.CardInPlay == true)
                {
                    AttackingCards.Add(position.Card);
                }
            }
            if (AttackingCards.Count != 0)
            {
                for (int i = 0; i < AttackingCards.Count; i++)
                {
                    List<PlayablePosition> AtkPosition = new List<PlayablePosition>();

                    for (int j = 0; j < OpponentPositions.Length; j++)
                    {
                        PlayablePosition oppPosition = OpponentPositions[j].GetComponent<PlayablePosition>();
                        if (oppPosition.CardInPlay == true)
                        {
                            AtkPosition.Add(oppPosition);
                        }
                    }
                    if(AtkPosition.Count != 0)
                    {
                        int target = Random.Range(0, AtkPosition.Count - 1);

                        int currentAttack;

                        if (AttackingCards[i].OnBlueSide == true)
                        {
                            currentAttack = AttackingCards[i].BlueAttack;
                        }
                        else
                        {
                            currentAttack = AttackingCards[i].RedAttack;
                        }
                        AtkPosition[target].Card.TakeDamage(currentAttack, AttackingCards[i]);
                    }
                    else
                    {
                        Player_HP PlayerHP = GameObject.Find("Player HP").GetComponent<Player_HP>();
                        AttackingCards[i].CardAttackedForTurn = true;
                        int currentAttack;

                        if (AttackingCards[i].OnBlueSide == true)
                        {
                            currentAttack = AttackingCards[i].BlueAttack;
                        }
                        else
                        {
                            currentAttack = AttackingCards[i].RedAttack;
                        }
                        PlayerHP.TakeDamage(currentAttack, AttackingCards[i]);
                    }
                }
            }
        }

        //End Turn
        CurrentAvailablePositions.Clear();
        TurnMan.ChangeTurn();
    }
}
