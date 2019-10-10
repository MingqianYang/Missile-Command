using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // UI panel appears after every end of one round to show information
    [SerializeField] private GameObject endOfRoundPanel;

    EnemyMissileSpawner enemyMissileSpawner;

    // The speed will become 1.25 times quicker than previous round
    [SerializeField] private float enemyMissileSpeedMultiplier = 1.25f;

    // the initial score, level, and enemy missile speed
    public int score = 0;
    public int level = 1;
    public float enemyMissileSpeed = 1f;

    // The number of defenders on the ground
    public int cityCounter = 0;

    // The total number of missiles left, the orignal is 30
    public int playerMissilesLeft = 30;

    // The current number of missiles in the launcher 
    public int currentMissilesLoadedLauncher = 0;

    // Initially set the enemy's missiles to 10, and left is 0
    private int enemyMissilesThisRound = 10;
    private int enemyMissilesLeftInRound = 0;
  

    // Score values, the player will add 25 marks every time he desotyed an enemy's missile
    private int missileDestroyedPoints = 25;

    private bool isRoundOver = false;

    // Player will get additional 5 scores for every player's missile left
    [SerializeField] private int missileEndofRoundPoints = 5;
    // Player will get additional 100 scores for every left city 
    [SerializeField] private int cityEndofRoundPoints = 100;

    // GUI text that need to be updated
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missileLeftText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI missilesLeftInLauncherText;

    [SerializeField] private TextMeshProUGUI leftOverMissileBonusText;
    [SerializeField] private TextMeshProUGUI leftOverCityBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;


    // Start is called before the first frame update
    void Start()
    {
        // Load 10 player's missiles into launcher, 30 - 10 = 20
        currentMissilesLoadedLauncher = 10;
        playerMissilesLeft -= 10;

        enemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();

        // Get the number of cities
        cityCounter = GameObject.FindObjectsOfType<City>().Length;

        // Update the information on the right top corner
        UpdateLevelText();
        UpdateScoreText();
        UpdateMissilesLeftText();
        UpdateMissilesInLauncherText();

        StartRound();

    }

    // Update is called once per frame
    void Update()
    {
        // Monitor if the end of rouond
        if (enemyMissilesLeftInRound <=0 && !isRoundOver)
        {
            isRoundOver = true;
            StartCoroutine(EndofRound());
        }

        // If all the cities are destoryed, then the "End Scene" will be loaded
        if (cityCounter <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }

    public void UpdateMissilesLeftText()
    {
        missileLeftText.text = "Missile Left: " + playerMissilesLeft;
        UpdateMissilesInLauncherText();
    }

    public void UpdateMissilesInLauncherText()
    {
        missilesLeftInLauncherText.text = "Missiles In Launcher: " + currentMissilesLoadedLauncher;
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

    // Update the player's missiles when every missile is luached
    public void PlayerFiredMissile()
    {
        if (currentMissilesLoadedLauncher > 0)
        {
            currentMissilesLoadedLauncher--;
        }
        if (currentMissilesLoadedLauncher == 0)
        {
            if (playerMissilesLeft >= 10)
            {
                currentMissilesLoadedLauncher = 10;
                playerMissilesLeft -= 10;
            }else
            {
                currentMissilesLoadedLauncher = playerMissilesLeft;
                playerMissilesLeft = 0;
            }
        }
        
        UpdateMissilesLeftText();
    }

    // The enemy's missile hited/destoryed the city/defender
    public void MissileLauncherHit()
    {
        if (playerMissilesLeft >= 10)
        {
            currentMissilesLoadedLauncher = 10;
            playerMissilesLeft -= 10;
        }
        else
        {
            currentMissilesLoadedLauncher = playerMissilesLeft;
            playerMissilesLeft = 0;
        }

        // Update the information on the screen
        UpdateMissilesLeftText();
        UpdateMissilesInLauncherText();
    }
    private void StartRound()
    {
        // Initially assign the number of enemy missile to enemy missile spawner
        enemyMissileSpawner.missilesToSpawnThisRound = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;

        enemyMissileSpawner.StartRound();
    }

    public IEnumerator EndofRound()
    {
        yield return new WaitForSeconds(0.5f);
        endOfRoundPanel.SetActive(true);

        // Caculate the scores
        int leftOverMissileBonus = (playerMissilesLeft + currentMissilesLoadedLauncher ) * missileEndofRoundPoints;

        // How many cities left
        City[] cities = GameObject.FindObjectsOfType<City>();
        int leftOverCityBonus = cities.Length * cityEndofRoundPoints;

        int totalBonus = leftOverCityBonus + leftOverMissileBonus;

        // There is no infinity level in this game
        if (level >=3 && level <5)
        {
            totalBonus *= 2;
        }
        else if (level >= 5 && level < 7)
        {
            totalBonus *= 3;
        }
        else if (level >= 7 && level < 9)
        {
            totalBonus *= 4;
        }
        else if (level >= 9 && level < 11)
        {
            totalBonus *= 5;
        }
        else if (level >= 11  )
        {
            totalBonus *= 6;
        }    

        // Display
        leftOverMissileBonusText.text = "Left over missile bonus: " + leftOverMissileBonus;
        leftOverCityBonusText.text = "Left over city bonus: " + leftOverCityBonus;
        totalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        UpdateScoreText();

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        // Hide Panel
        endOfRoundPanel.SetActive(false);

        // Commence new round
        isRoundOver = false;


        //  Updating new round settings;
        enemyMissileSpeed *= enemyMissileSpeedMultiplier;
        playerMissilesLeft = 30;

        currentMissilesLoadedLauncher = 10;
        playerMissilesLeft -= 10;

        level++;
        StartRound();
        UpdateLevelText();
        UpdateMissilesLeftText();

    }
}
