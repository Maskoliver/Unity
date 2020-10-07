using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public Text levelText;
    public Text totalEggs1;
    public Text totalEggs2;
    public Text activeLevel;
    void Start()
    {
        levelText.text = SceneManager.GetActiveScene().name;

    }

    // Update is called once per frame
    void Update()
    {
        totalEggs1.text = "Total Eggs : " + PlayerPrefs.GetInt("TotalEggs");
        totalEggs2.text = "Total Eggs : " + PlayerPrefs.GetInt("TotalEggs");
        activeLevel.text = "Max Level : " + PlayerPrefs.GetString("LatestLevel");
        scoreText.text = "Eggs : " + score.ToString();
    }

    public void updateScore()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }
}
