using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] float minSpawnTime = 2f;
    [SerializeField] float maxSpawnTime = 4f;
    [Header("Offest Settings")]
    [SerializeField] float spawnOffsetX = 0.5f;
    [SerializeField] float spawnOffsetY = 0.5f;

    float randomSpawnTime;
    Vector3 randomPathWidth;

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
        randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        randomPathWidth = new Vector3(Random.Range(transform.position.x - spawnOffsetX, transform.position.x + spawnOffsetX),
                                      Random.Range(transform.position.y - spawnOffsetY, transform.position.y + spawnOffsetY),
                                                   transform.position.z);
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        //Отсчитываем случайный таймер
        randomSpawnTime -= Time.deltaTime;

        if(randomSpawnTime <= 0)
        {
            //Порождаем рандомного зомби
            Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], randomPathWidth, Quaternion.identity);

            //Переустанавливаем spawnTime и pathWidth для врага
            randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            randomPathWidth = new Vector3(Random.Range(transform.position.x - spawnOffsetX, transform.position.x + spawnOffsetX),
                                          Random.Range(transform.position.y - spawnOffsetY, transform.position.y + spawnOffsetY), 
                                                       transform.position.z);           
        }
    }
}
