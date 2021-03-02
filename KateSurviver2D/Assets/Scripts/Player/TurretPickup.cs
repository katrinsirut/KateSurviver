using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPickup : MonoBehaviour
{
    [SerializeField] Turret turret;
    [SerializeField] GameObject turretUIImage;
    private bool isPickedUp = false;
    private float waitTime = .1f;

    // Обновление вызывается один раз за кадр
    void Update()
    {
        StartCoroutine(DropTurret());
        ShowUI();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(PickupTurret(collision));
    }

    private IEnumerator PickupTurret(Collider2D collision)
    {
        if(!isPickedUp)
        {
            //Сравнить метку башни с столкновением
            if (collision.gameObject.tag == "Turret")
            {
                //Если RMB нажата
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                   
                    turret.transform.position = new Vector2(-1000, -1000);

                    yield return new WaitForSeconds(waitTime);
                    isPickedUp = true;
                }
            }
        }
    }

    private IEnumerator DropTurret()
    {
        if (isPickedUp)
        {
            //Если RMB нажата
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Бросить башню на позиции игроков
                turret.transform.position = transform.position;

                yield return new WaitForSeconds(waitTime);
                isPickedUp = false;
            }
        }
    }

    private void ShowUI()
    {
        //Показываем или скрываем пользовательский интерфейс в зависимости от того, выбрана башня или нет
        if (isPickedUp)
            turretUIImage.gameObject.SetActive(true);
        else
            turretUIImage.gameObject.SetActive(false);
    }
}