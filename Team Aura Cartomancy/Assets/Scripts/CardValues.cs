using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValues : MonoBehaviour
{
    public bool IsEnemyCard;
    public GameObject FrontGFX;
    public GameObject BackGFX;

    [HideInInspector] public bool OnBlueSide;
    [HideInInspector] public bool CardIsActive;

    public int BlueAttack;
    public int BlueHealth;

    public int RedAttack;
    public int RedHealth;

    [HideInInspector] public bool CardAttackedForTurn;

    private SelectRotation selRotation;
    private void Start()
    {
        selRotation = gameObject.GetComponent<SelectRotation>();
    }

    public void SetCardAsPlayerCard()
    {
        BackGFX.SetActive(false);
        FrontGFX.SetActive(true);
    }

    public void SetCardAsEnemyCard()
    {
        IsEnemyCard = true;
        BackGFX.SetActive(true);
        FrontGFX.SetActive(false);
    }

    public void FlipCard(int Direction)
    {
        BackGFX.SetActive(false);
        FrontGFX.SetActive(true);

        if(Direction > 0)
        {
            CardIsActive = true;
            OnBlueSide = true;
            selRotation.BlueSide();
        }
        else
        {
            CardIsActive = true;
            OnBlueSide = false;
            selRotation.RedSide();
        }
    }
}
