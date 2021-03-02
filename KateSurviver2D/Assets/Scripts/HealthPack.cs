using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] Player player;

    [Range(5, 25)]
    [SerializeField] int healAmount = 10;

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
    }

    // Обновление вызывается один раз за кадр
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealPlayer();
    }

    private void HealPlayer()
    {
        Destroy(gameObject);
        player.AddHealth(healAmount);
    }
}
