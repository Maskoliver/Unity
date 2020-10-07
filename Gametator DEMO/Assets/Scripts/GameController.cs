using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;
    public float restartDelay = 1f;
    public void CompleteLevel()
    {
        Score myScore = FindObjectOfType<Score>();
        myScore.scoreText.enabled = false;
        levelMenu.SetActive(true);


    }



    public void EndGame()
    {
        Debug.Log("Ended");
        FindObjectOfType<EnemyRotation>().setActivated(false);
        FindObjectOfType<PlayerController>().freezePlayer();
        Invoke("Restart", restartDelay);
    }

    void Restart()
    {
        FindObjectOfType<LineDrawer>().setIsDrawed(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
