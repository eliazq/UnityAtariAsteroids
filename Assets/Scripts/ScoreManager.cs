using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int IncreaseScore = 10;
    private int score = 0;

    private void Start()
    {
        Asteroid.OnAsteroidExploding += Asteroid_OnAsteroidExploding;
    }

    private void Asteroid_OnAsteroidExploding(object sender, System.EventArgs e)
    {
        score += IncreaseScore;
        scoreText.text = score.ToString();
    }
}
