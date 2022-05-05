using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardValues : MonoBehaviour
{
    public bool IsEnemyCard;
    public GameObject FrontGFX;
    public GameObject BackGFX;

    public bool OnBlueSide;
    public bool CardIsActive;

    public int BlueAttack;
    public int BlueHealth;

    public int RedAttack;
    public int RedHealth;

    [HideInInspector] public bool CardAttackedForTurn;
    [HideInInspector] public bool selected;

    private SelectRotation selRotation;
    public TMP_Text BlueHP_Display;
    public TMP_Text BlueAtk_Display;
    public TMP_Text RedHP_Display;
    public TMP_Text RedAtk_Display;

    public PlayablePosition position;

    private void Start()
    {
        selRotation = gameObject.GetComponent<SelectRotation>();
    }

    private void Update()
    {
        BlueHP_Display.text = BlueHealth.ToString();
        BlueAtk_Display.text = BlueAttack.ToString();
        RedHP_Display.text = RedHealth.ToString();
        RedAtk_Display.text = RedAttack.ToString();

        if(OnBlueSide == true && CardIsActive == true)
        {
            BlueHP_Display.color = Color.red;
            BlueAtk_Display.color = Color.red;
            RedHP_Display.color = Color.white;
            RedAtk_Display.color = Color.white;
        }
        else if(CardIsActive == true)
        {

            BlueHP_Display.color = Color.white;
            BlueAtk_Display.color = Color.white;
            RedHP_Display.color = Color.red;
            RedAtk_Display.color = Color.red;
        }
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

    public void TakeDamage(int Damage, CardValues AttackingCard)
    {
        position.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        AttackingCard.CardAttackedForTurn = true;

        if (OnBlueSide == true)
        {
            BlueHealth -= Damage;
            if(BlueHealth <= 0)
            {
                CardDefeated();
            }
        }
        else
        {
            RedHealth -= Damage;
            if(RedHealth <= 0)
            {
                CardDefeated();
            }
        }
    }

    public void CardDefeated()
    {
        position.CardInPlay = false;
        position.Card = null;

        Destroy(gameObject);
    }
}
