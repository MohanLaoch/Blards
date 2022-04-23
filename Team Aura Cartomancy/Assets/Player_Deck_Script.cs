using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Deck_Script : MonoBehaviour
{
    [SerializeField] private List<GameObject> CardsInStartingDeck;
    public List<GameObject> CardsRemainingInDeck;

    public void Awake()
    {
        CardsRemainingInDeck = CardsInStartingDeck;
    }


}
