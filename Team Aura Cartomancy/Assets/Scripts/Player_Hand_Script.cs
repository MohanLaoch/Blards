using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hand_Script : MonoBehaviour
{
    public int MaxCardsInHand = 4;
    public List<GameObject> CardsInHand;

    public float CardOffset = 1f;

    public Player_Deck_Script Deck_Script;

    public void Start()
    {
        int Card1 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card1Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card1]);
        Card1Obj.GetComponent<Dragger>().PHS = this;
        if (Deck_Script.EnemyDeck) {Card1Obj.GetComponent<CardValues>().SetCardAsEnemyCard(); }
        else { Card1Obj.GetComponent<CardValues>().SetCardAsPlayerCard(); }
        CardsInHand.Add(Card1Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card1);

        int Card2 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card2Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card2]);
        Card2Obj.GetComponent<Dragger>().PHS = this;
        if (Deck_Script.EnemyDeck) { Card2Obj.GetComponent<CardValues>().SetCardAsEnemyCard(); }
        else { Card2Obj.GetComponent<CardValues>().SetCardAsPlayerCard(); }
        CardsInHand.Add(Card2Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card2);

        int Card3 = Random.Range(0, Deck_Script.CardsRemainingInDeck.Count);
        GameObject Card3Obj = Instantiate(Deck_Script.CardsRemainingInDeck[Card3]);
        Card3Obj.GetComponent<Dragger>().PHS = this;
        if (Deck_Script.EnemyDeck) { Card3Obj.GetComponent<CardValues>().SetCardAsEnemyCard(); }
        else {Card3Obj.GetComponent<CardValues>().SetCardAsPlayerCard();}
        CardsInHand.Add(Card3Obj);
        Deck_Script.CardsRemainingInDeck.RemoveAt(Card3);
        
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].transform.position = new Vector3(i * CardOffset - CardOffset, gameObject.transform.position.y, 0);
            CardsInHand[i].GetComponent<Dragger>().HandPosition = CardsInHand[i].transform.position;
        }
    }

    public void ResetHand()
    {
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if(i == 0)
            {
                float NewX = -((CardsInHand.Count - 1) / 2) * CardOffset;
                CardsInHand[i].transform.position = new Vector3(NewX, gameObject.transform.position.y, 0);
                CardsInHand[i].GetComponent<Dragger>().HandPosition = CardsInHand[i].transform.position;
            }
            else
            {
                float NewX = CardsInHand[0].transform.position.x + i * CardOffset;
                CardsInHand[i].transform.position = new Vector3(NewX, gameObject.transform.position.y, 0);
                CardsInHand[i].GetComponent<Dragger>().HandPosition = CardsInHand[i].transform.position;
            }
        }
    }
}
