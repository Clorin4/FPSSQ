using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    private bool isDead = false;
    private PlayerShooting playerShooting;

    public float damageCooldownTime = .1f; 
    private bool isOnDamageCooldown = false;
    public int score = 0;
    public float maxTime = 60f; 
    private float currentTime;
    public TextMeshProUGUI timerText;

    public bool hasWon = false;
    public GameObject victoryPanel;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI scoreTextVictory;

    public static PlayerManager sharedInstance;
    AudioManager audioManager;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        audioManager = AudioManager.instance;
        currentHealth = maxHealth;
        currentTime = maxTime;
    }

    private void Update()
    {
        if (!hasWon)
        {
            
            UpdateTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vacio"))
        {
            Die();
        }
    }

    private IEnumerator ActivateCooldown()
    {
        isOnDamageCooldown = true;
        yield return new WaitForSeconds(damageCooldownTime);
        isOnDamageCooldown = false;
    }

    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                Die();
            }
        }
        timerText.text = "Tiempo: " + Mathf.Ceil(currentTime).ToString();
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;
        if (currentTime > maxTime)
        {
            currentTime = maxTime;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead || isOnDamageCooldown) return;

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }

        
        StartCoroutine(ActivateCooldown());
    }


    private void Die()
    {
        audioManager.PlaySound("Die");
        SceneManager.LoadScene(1);
        Debug.Log("MORISTE");
    }
    
}
