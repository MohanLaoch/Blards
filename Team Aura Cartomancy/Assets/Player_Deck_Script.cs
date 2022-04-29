using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Deck_Script : MonoBehaviour
{
    private TurnManager TurnMan;

    [SerializeField] private List<GameObject> CardsInStartingDeck;
    public List<GameObject> CardsRemainingInDeck;

    private Player_Hand_Script PHS;

    public void Awake()
    {
        CardsRemainingInDeck = CardsInStartingDeck;
    }

    public void Start()
    {
        TurnMan = FindObjectOfType<TurnManager>();
        PHS = FindObjectOfType<Player_Hand_Script>();
    }

    public void Draw()
    {
        if (TurnMan.IsPlayerTurn == true && TurnMan.PlayerDrawnForTurn == false)
        {
            if (PHS.CardsInHand.Count < PHS.MaxCardsInHand)
            {
                TurnMan.PlayerDrawnForTurn = true;
                int Card = Random.Range(0, CardsRemainingInDeck.Count);
                GameObject Card1Obj = Instantiate(CardsRemainingInDeck[Card]);
                PHS.CardsInHand.Add(Card1Obj);
                CardsRemainingInDeck.RemoveAt(Card);

                PHS.ResetHand();
            }
        }
    }
}
