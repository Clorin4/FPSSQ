using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyType
{
    Normal,
    Agresive,
    Ranged
}


public class EnemyController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    AudioManager audioManager;

    public float normalSearchRange = 10f;
    public float aggressiveSearchRange = 20f;
    public float rangeSearchRange = 5f;

    public int health = 100;
    public int damageAmount = 20;

    public int pointValue = 50;

    private Transform player;
    private NavMeshAgent navMeshagent;
    public EnemyType enemyType;

    public float stopChaseDistance = 20f;
    public Vector3 initialPos;

    public bool foundPlayer = false;

    private void Awake()
    {
      
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshagent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        audioManager = AudioManager.instance;
    }

    private void Update()
    {
        SearchPlayer();
        StopSearchPlayer();
    }

    private void SearchPlayer()
    {
        float searchRange = GetSearchRangeForEnemyType(enemyType);

        if (Vector3.Distance(player.position, transform.position) <= searchRange)
        {
            navMeshagent.SetDestination(player.position);
        }
    }

    private void StopSearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (!foundPlayer || distanceToPlayer == stopChaseDistance)
        {
            navMeshagent.SetDestination(player.position);
        }
        else
            navMeshagent.SetDestination(initialPos);
    }


    private float GetSearchRangeForEnemyType(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                return normalSearchRange;
            case EnemyType.Agresive:
                return aggressiveSearchRange;
            case EnemyType.Ranged:
                return rangeSearchRange;
            default:
                return 0f;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        audioManager.PlaySound("Laser");
        enemySpawner.ReduceEnemiesInRound();
        PlayerManager.sharedInstance.score += pointValue;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundPlayer = true;
            PlayerManager.sharedInstance.TakeDamage(damageAmount);
            navMeshagent.ResetPath();
        }

        else if (other.CompareTag("Bullet"))
        {
            Die();
        }

    }

    

}
