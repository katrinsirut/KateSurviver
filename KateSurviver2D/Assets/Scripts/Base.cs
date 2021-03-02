using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Text baseHealthUI;

    [SerializeField] SceneLoader sceneLoader;

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
        baseHealthUI.text = health.ToString();
        
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        StartCoroutine(LoseGame());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        RemoveHealth(10);
    }

    private void RemoveHealth(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            baseHealthUI.text = health.ToString();
            Debug.Log(health);
        }
    }

    private IEnumerator LoseGame()
    {
        if(health <= 0)
        {
           

            //Ждём 2 секунды
            yield return new WaitForSeconds(2);
            //Игрок проигрывает, загружаем Game Over
            sceneLoader.LoadSceneByName("Game Over Screen");
        }
    }
}
