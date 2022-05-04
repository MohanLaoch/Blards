using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Deck_Script : MonoBehaviour
{
    private TurnManager TurnMan;

    public bool EnemyDeck;

    [SerializeField] private List<GameObject> CardsInStartingDeck;
    public List<GameObject> CardsRemainingInDeck;

    public Player_Hand_Script PHS;

    public void Awake()
    {
        CardsRemainingInDeck = CardsInStartingDeck;
    }

    public void Start()
    {
        TurnMan = FindObjectOfType<TurnManager>();
    }

    public void Draw()
    {
        if (PHS.CardsInHand.Count < PHS.MaxCardsInHand)
        {
            TurnMan.PlayerDrawnForTurn = true;
            int Card = Random.Range(0, CardsRemainingInDeck.Count);
            GameObject Card1Obj = Instantiate(CardsRemainingInDeck[Card]);
            Card1Obj.GetComponent<Dragger>().PHS = PHS;
            if(EnemyDeck)
            {
                Card1Obj.GetComponent<CardValues>().SetCardAsEnemyCard();
            }
            else
            {
                Card1Obj.GetComponent<CardValues>().SetCardAsPlayerCard();
            }
            PHS.CardsInHand.Add(Card1Obj);
            CardsRemainingInDeck.RemoveAt(Card);

            PHS.ResetHand();
        }
        else
        {
            //Too Many Cards
            TurnMan.PlayerDrawnForTurn = true;
        }
    }
}
