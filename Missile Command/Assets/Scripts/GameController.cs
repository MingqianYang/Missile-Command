using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{

    public int score = 0;
    public int level = 1;
    public int missilesLeft = 30;

    // Score values
    private int missileDestroyedPoints = 25;

    // GUI text
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missileLeftText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText();
        UpdateScoreText();
        UpdateMissilesLeftText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMissilesLeftText()
    {
        missileLeftText.text = "Missile Left: " + missilesLeft;
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
        UpdateScoreText();
    }
}
