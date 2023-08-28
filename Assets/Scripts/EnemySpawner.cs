using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Round
{
    public int normalEnemiesCount;
    public int aggressiveEnemiesCount;
    public int rangeEnemiesCount;
}


public class EnemySpawner : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject aggressiveEnemyPrefab;
    public GameObject rangeEnemyPrefab;

    public GameObject[] spawnPoints;

    public Round[] rounds;
    private int currentRound = 0;
    public int enemiesInRound;

    public TextMeshProUGUI roundText;
    public GameObject enemyPrefab;

    private float startTime;
    private float winTime;
    private bool hasFinishedGame = false;

    public GameObject cuboAzul;
    public GameObject cuboRojo;
    public GameObject CanvasB;

    private void Start()
    {
        roundText = GameObject.Find("Ronda Text").GetComponent<TextMeshProUGUI>();
        enemiesInRound = 0;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        startTime = Time.time;
        SpawnEnemies();
        cuboAzul.SetActive(false);
        cuboRojo.SetActive(false);

    }

    public void ReduceEnemiesInRound()
    {
        enemiesInRound--;
    }

    private void Update()
    {
        if (!hasFinishedGame && currentRound >= 0 && currentRound < rounds.Length)
        {
            // Lógica para verificar si los enemigos de la ronda actual han sido derrotados
            // Si es así, avanzar a la siguiente ronda
            if (enemiesInRound <= 0)
            {
                currentRound++;
                if (currentRound < rounds.Length)
                {
                    SpawnEnemies();
                }
                else
                {
                    Debug.Log("VICTORIA");
                    winTime = Time.time;
                    PlayerManager.sharedInstance.hasWon = true;
                    PlayerManager.sharedInstance.victoryPanel.SetActive(true);
                    PlayerManager.sharedInstance.totalTimeText.text = "Tiempo total: " + (winTime - startTime).ToString("F2") + " segundos";
                    PlayerManager.sharedInstance.scoreTextVictory.text = "Puntaje obtenido: " + PlayerManager.sharedInstance.score;
                    hasFinishedGame = true;
                    cuboAzul.SetActive(true);
                    cuboRojo.SetActive(true);
                    CanvasB.SetActive(false);
                    return;
                }
            }
        }
        else if (!hasFinishedGame)
        {
            Debug.Log("VICTORIA");
            winTime = Time.time;
            PlayerManager.sharedInstance.hasWon = true;
            PlayerManager.sharedInstance.victoryPanel.SetActive(true);
            PlayerManager.sharedInstance.totalTimeText.text = "Tiempo total: " + (winTime - startTime).ToString("F2") + " segundos";
            hasFinishedGame = true;
            cuboAzul.SetActive(true);
            cuboRojo.SetActive(true);
            CanvasB.SetActive(false);
            return;
        }


    }

    

    private void SpawnEnemies()
    {
        Round round = rounds[currentRound];
        enemiesInRound = round.normalEnemiesCount + round.aggressiveEnemiesCount + round.rangeEnemiesCount;

        for (int i = 0; i < round.normalEnemiesCount; i++)
        {
            Vector3 spawnPosition = spawnPoints[i % spawnPoints.Length].transform.position;
            GameObject newEnemy = Instantiate(normalEnemyPrefab, spawnPosition, Quaternion.identity);
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.enemySpawner = this;
        }

        for (int i = 0; i < round.aggressiveEnemiesCount; i++)
        {
            Vector3 spawnPosition = spawnPoints[i % spawnPoints.Length].transform.position;
            GameObject newEnemy = Instantiate(aggressiveEnemyPrefab, spawnPosition, Quaternion.identity);
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.enemySpawner = this;
        }

        for (int i = 0; i < round.rangeEnemiesCount; i++)
        {
            Vector3 spawnPosition = spawnPoints[i % spawnPoints.Length].transform.position;
            GameObject newEnemy = Instantiate(rangeEnemyPrefab, spawnPosition, Quaternion.identity);
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.enemySpawner = this;
        }

        // Actualizar el texto 
        roundText.text = "Ronda: " + (currentRound + 1);
    }

    private void InstantiateEnemy(Vector3 position)
    {
        GameObject selectedPrefab = GetRandomEnemyPrefab();
        Instantiate(selectedPrefab, position, Quaternion.identity);
    } 

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, 3);

        if (randomIndex == 0)
        {
            return normalEnemyPrefab;
        }
        else if (randomIndex == 1)
        {
           return aggressiveEnemyPrefab;
        }
        else 
        {
            return rangeEnemyPrefab;
        }
    }


}
