using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform movingTurret;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform barrel = null;
    [SerializeField] AudioSource shootSFX;

    [Header("Turret Config")]
    [SerializeField] float attackRange = .5f;
    [SerializeField] float fireRate = 2;

    private Enemy enemy;
    private float fireDelay;

    void Start()
    {
        //Мы хотим, чтобы первый выстрел был сразу
        fireDelay = 0;
    }

    // Запуск вызывается перед первым обновлением кадра
    void Update()
    {
        FindClosestEnemy();
        LookAtClosestEnemy();
    }

    private void FindClosestEnemy()
    {
        //Находим каждого врага на месте преступления
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        //Если нет ни одного врага 
        if(enemies.Length == 0) { return; }
        else
        {
            //Создаём первого врага
            Enemy closestEnemy = enemies[0];

            //Рассчитываем расстояние до башни
            var closestEnemyDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);

            //Проходимся по каждому врагу на месте преступления
            for (int i = 0; i < enemies.Length; i++)
            {
                //Если дистанция врага [i] менльше чем позиция ближайшего врага
                if(Vector2.Distance(transform.position, enemies[i].transform.position) < closestEnemyDistance)
                {
                    //Этот враг теперь ближайший враг
                    closestEnemy = enemies[i];

                    //Самое короткое расстояние - это расстояние нового ближайшего врага
                    closestEnemyDistance = Vector2.Distance(transform.position, enemies[i].transform.position);
                }
            }

            //Пусть ближайший враг будет врагом, на которого смотрит башня
            enemy = closestEnemy;
        }
    }

    private void LookAtClosestEnemy()
    {
        //Если враг суще ствует
        if(enemy != null)
        {
            //Если расстояние между башней и противником меньше дальности башни
            if (Vector2.Distance(transform.position, enemy.transform.position) < attackRange)
            {
                //Смотрим на врага
                movingTurret.LookAt(enemy.transform.position);
                transform.right = enemy.transform.position - transform.position;

                Shoot();
            }
        }
        
    }

    private void Shoot()
    {
        fireDelay -= Time.deltaTime;

        if (fireDelay <= 0)
        {
            Instantiate(projectile, barrel.transform.position, barrel.rotation);
            shootSFX.Play();

            //Переутсанавливаем задержку
            fireDelay = fireRate;
        }
    }
}

