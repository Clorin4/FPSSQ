using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float shootingRange = 5f;

    private Transform player;
    private float nextFireTime;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextFireTime = Time.time;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation); 
    }

}
