using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthBarSlider;


    private int health;

    private bool isInvunerable;

    public event Action OnTakeDamage;

    public event Action OnDie;

    // check anywhere if dead
    public bool isDead => health == 0;

    private void Start()
    {
        health = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;
    }

    public void SetInvulnerable(bool isInvunerable) 
    {
        this.isInvunerable = isInvunerable;
    }

    public void DealDamage(int damage) 
    {
        if(health == 0) 
        {
            return;  
        }

        if (isInvunerable)
        {
            return;
        }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }

        //Debug.Log(health);
        healthBarSlider.value = health;
    }
}
