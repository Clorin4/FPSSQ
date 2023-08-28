using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI timerText;

    //private int score;
    private int bullets;
    //private int life;
    private string position;
    //private float timer;

    private void Start()
    {
        //score = 0;
        
        //life = 100;
        position = "Normal";
        //timer = 0f;
    }

    private void Update()
    {
        scoreText.text = "Puntaje: " + PlayerManager.sharedInstance.score.ToString();

        healthText.text = "Vida: " + PlayerManager.sharedInstance.currentHealth.ToString();

        positionText.text = "Position: " + position;
        
        //timerText.text = "Timer: " + timer.ToString("F2") + "s";
    }

    public void UpdatePosition(string newPosition)
    {
        position = newPosition;
    }
}
