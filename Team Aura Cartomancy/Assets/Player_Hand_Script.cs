using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hand_Script : MonoBehaviour
{
    public List<GameObject> CardsInHand;
    public float CardOffset = 1f;

    private Player_Deck_Script Deck_Script;

    public void Start()
    {
        Deck_Script = FindObjectOfType<Player_Deck_Script>();

        int Card1 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card1Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card1]);
        CardsInHand.Add(Card1Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card1);

        int Card2 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card2Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card2]);
        CardsInHand.Add(Card2Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card2);

        int Card3 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card3Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card3]);
        CardsInHand.Add(Card3Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card3);
    }

    public void Update()
    {
        for(int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].transform.position = new Vector3(i * CardOffset - CardOffset, -4.2f, 0);
        }
    }
}
