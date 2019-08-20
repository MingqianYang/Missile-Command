﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endOfRoundPanel;
    EnemyMissileSpawner enemyMissileSpawner;

    public int score = 0;
    public int level = 1;
    public int playerMissilesLeft = 30;
    private int enemyMissilesThisRound = 20;
    private int enemyMissilesLeftInRound = 0;
  

    // Score values
    private int missileDestroyedPoints = 25;

    // GUI text
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missileLeftText;

    [SerializeField] private int missileEndofRoundPoints = 5;
    [SerializeField] private int cityEndofRoundPoints = 100;

    [SerializeField] private TextMeshProUGUI leftOverMissileBonusText;
    [SerializeField] private TextMeshProUGUI leftOverCityBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    // Start is called before the first frame update
    void Start()
    {
        enemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();

        UpdateLevelText();
        UpdateScoreText();
        UpdateMissilesLeftText();

        StartRound();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMissilesLeftInRound <=0)
        {
            StartCoroutine(EndofRound());
        }
    }

    public void UpdateMissilesLeftText()
    {
        missileLeftText.text = "Missile Left: " + playerMissilesLeft;
    }

    public void UpdateScoreText()
    {       
        scoreText.text = "Score: " + score;
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }

    public void AddMissileDestroyedPoints()
    {
        score += missileDestroyedPoints;
        EnemyMissileDestroyed();
        UpdateScoreText();
    }

    public void EnemyMissileDestroyed()
    {
        enemyMissilesLeftInRound--;
    }

    private void StartRound()
    {
        enemyMissileSpawner.missilesToSpawnThisRound = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;

        enemyMissileSpawner.StartRound();
    }

    public IEnumerator EndofRound()
    {
        yield return new WaitForSeconds(0.5f);
        endOfRoundPanel.SetActive(true);
        int leftOverMissileBonus = playerMissilesLeft * missileEndofRoundPoints;


        City[] cities = GameObject.FindObjectsOfType<City>();
        int leftOverCityBonus = cities.Length * cityEndofRoundPoints;

        int totalBonus = leftOverCityBonus + leftOverMissileBonus;


        // Display
        leftOverMissileBonusText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityBonusText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
    }
}
