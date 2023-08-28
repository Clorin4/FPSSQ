using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectil : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.sharedInstance.TakeDamage(damage);
        }
        else if (other.CompareTag("Enemy") && !other.CompareTag("Proyectile"))
        {
            Destroy(gameObject);
        }


    }


}
