using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] float speed = 5f;
    [SerializeField] float padding = 1f;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Text playerHealthUI;
    [SerializeField] int damagePerSec = 3;
    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] float playerHealth;
    private Vector2 mousePos;
    float xMin, xMax, yMin, yMax;

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
        playerHealth = 100.0f;
        Debug.Log(playerHealth);
        playerHealthUI.text = playerHealth.ToString();
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        if(pauseMenu.GetIsPaused() == false)
        {
             MouseClickMovement();
        }

        //Лучевая болезнь
        LoseHealthOverTime();
    }

    public void AddHealth(int healAmount)
    { 
        playerHealth += healAmount;

        //Убеждаемся, что здоровье игрока не превышает полного
        if (playerHealth > 100) { playerHealth = 100; }
    }

    private void KeyboardMovement()
    {
        float moveHor = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVer = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float newXPos = Mathf.Clamp(transform.position.x + moveHor, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + moveVer, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void MouseClickMovement()
    {
        //Проверяем была ли кликнута левая кнопка мыши
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Find the position of the mouse 
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //Переместить спрайт в положение мыши
        if ((Vector2)transform.position != mousePos)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
        }
    }

    private void MovementBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void LoseHealthOverTime()
    { 
        if(playerHealth > 0)
        {
            //игрок теряет Х здоровья каждую секунду
            playerHealth -= Time.deltaTime * damagePerSec;

            //Создать локальную переменную INT версию здоровья игрока, чтобы пользовательский интерфейс не уменьшал десятичные знаки
            int playerHealthToInt = Mathf.RoundToInt(playerHealth);

            //Обновить пользовательский интерфейс playerHealth как int
            playerHealthUI.text = playerHealthToInt.ToString();
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        //Ждем 2 секунды
        yield return new WaitForSeconds(4);

        //Игрок проигрывает, загружаем Game Over
        sceneLoader.LoadSceneByName("Game Over Screen");
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }
}
