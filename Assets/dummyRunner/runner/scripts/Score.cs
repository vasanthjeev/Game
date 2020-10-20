using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private float score = 2.85f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;
    public Text scoreText;
    public DeathMenu deathMenu;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Heelo";
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (score >= scoreToNextLevel)
            LevelUp();
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;
        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
        // Debug.Log("LevelUp: ", difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("Highscore") < score)
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }

        deathMenu.ToggleEndMenu(score);
        scoreText.text = ((int)score).ToString();
    }
}