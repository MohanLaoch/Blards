using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_HP : MonoBehaviour
{
    public int MaxHP;
    [HideInInspector] public int CurrentHP;
    public HealthBar healthBar;

    public TMP_Text HP_Display;

    public SpriteRenderer OrbHighlightedBorder;
    private void Start()
    {
        CurrentHP = MaxHP;
        healthBar.SetMaxHealth(MaxHP);
    }

    private void Update()
    {
        healthBar.SetHealth(CurrentHP);
        HP_Display.text = CurrentHP.ToString();
    }

    public void TakeDamage(int Damage, CardValues CV)
    {
        CV.CardAttackedForTurn = true;

        OrbHighlightedBorder.enabled = false;
        CurrentHP -= Damage;
        if(CurrentHP <= 0)
        {
            //GameEndHere
        }
    }
}
