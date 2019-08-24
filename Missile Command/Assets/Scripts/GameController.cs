using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endOfRoundPanel;
    EnemyMissileSpawner enemyMissileSpawner;


    [SerializeField] private float enemyMissileSpeedMultiplier = 1.25f;

    public int score = 0;
    public int level = 1;
    public float enemyMissileSpeed = 1f;

    public int currentMissilesLoadedLauncher = 0;
    public int playerMissilesLeft = 30;
    private int enemyMissilesThisRound = 10;
    private int enemyMissilesLeftInRound = 0;
  

    // Score values
    private int missileDestroyedPoints = 25;

    // GUI text
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missileLeftText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI missilesLeftInLauncherText;

    [SerializeField] private int missileEndofRoundPoints = 5;
    [SerializeField] private int cityEndofRoundPoints = 100;

    [SerializeField] private TextMeshProUGUI leftOverMissileBonusText;
    [SerializeField] private TextMeshProUGUI leftOverCityBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    private bool isRoundOver = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMissilesLoadedLauncher = 10;
        playerMissilesLeft -= 10;

        enemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();

        UpdateLevelText();
        UpdateScoreText();
        UpdateMissilesLeftText();
        UpdateMissilesInLauncherText();


        StartRound();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMissilesLeftInRound <=0 && !isRoundOver)
        {
            isRoundOver = true;
            StartCoroutine(EndofRound());
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

    public void PlayerFiredMissile()
    {
        if (currentMissilesLoadedLauncher > 0)
        {
            currentMissilesLoadedLauncher--;
        }
        if (currentMissilesLoadedLauncher == 0)
        {
            if (playerMissilesLeft>=10)
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
    public void MissileLauncherHit()
    {
       // playerMissilesLeft -= currentMissilesLoadedLauncher;
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

        UpdateMissilesLeftText();
        UpdateMissilesInLauncherText();
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
        StartRound();
        UpdateLevelText();
        UpdateMissilesLeftText();

    }
}
