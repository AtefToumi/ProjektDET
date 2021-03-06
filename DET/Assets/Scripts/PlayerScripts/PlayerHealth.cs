using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    internal void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, startingHealth);

        if (healthSlider)
        {
            healthSlider.value += amount;
        }
    }

    Animator anim;
    PlayerMovement playerMovement;
    public bool playerDead;
    bool damaged;

    public GameObject gameOvercanvus;

    void Awake() 
    {
       anim = GetComponent<Animator>();
       playerMovement = GetComponent<PlayerMovement>();
       currentHealth = startingHealth;
    }

    void Update() 
    {
        if(damaged)
        {
            damageImage.color = flashColor;
            FindObjectOfType<AudioManager>().play("playerDamage");
       
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !playerDead)
        {
            Death();
        }
    }

    void Death()
    {
        playerDead = true;
        anim.SetTrigger("PlayerDead");

        //playerMovement.enabled = false;

       
        //GameOver();
    }

    void GameOver()
    {
        gameOvercanvus.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
