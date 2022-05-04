using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HP : MonoBehaviour
{
    public int MaxHP;
    [HideInInspector] public int CurrentHP;
    public HealthBar healthBar;

    private void Start()
    {
        CurrentHP = MaxHP;
        healthBar.SetMaxHealth(MaxHP);
    }
}
