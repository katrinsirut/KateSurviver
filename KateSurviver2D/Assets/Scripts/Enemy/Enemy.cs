﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int attackDamage = 20;
    [SerializeField] int moveSpeed = 500;
    [SerializeField] int health = 100;

    [Header("References")]
    [SerializeField] Projectile projectile;
    private Score score;

    Vector2 moveDown;
    Rigidbody2D rb;
    bool changeColour = false;

    //Используем
    public int GetAttackDamage() { return attackDamage; }

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
        score = FindObjectOfType<Score>();
        Move();
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        
    }

    private void Move()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * (moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если враг столкнется с базой
        if (collision.gameObject.tag == "Base" || collision.gameObject.tag == "MenuCollider")
        {
            Destroy(gameObject, 1f);
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            health -= projectile.GetDamage();
            StartCoroutine(FlashRedWhenDamaged());

            if (health <= 0)
            {
                score.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator FlashRedWhenDamaged()
    {
        changeColour = true;
        float timeLeft = 0.08f;
        if (changeColour)
        {
            transform.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(timeLeft);
            changeColour = false;
        }

        if (changeColour == false)
        {
            transform.GetComponent<Renderer>().material.color = Color.white;
        }
    }

}
