using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelMenu : MonoBehaviour
{
    public GameObject menu;
    LineDrawer lineDrawer;
    public void goNextLevel()
    {
        lineDrawer.setisMenuOpen(true);
        menu.SetActive(false);
        OpenMenu();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void OpenMenu()
    {
        lineDrawer.setisMenuOpen(true);
        if (gameObject.tag == "MainMenu")
        {
            menu.SetActive(true);
        }
        else
        {

        }
    }

    public void StartLevel()
    {
        if (PlayerPrefs.GetInt("LatestLevel") <= (SceneManager.GetActiveScene().buildIndex + 1))
        {
            PlayerPrefs.SetInt("LatestLevel", (SceneManager.GetActiveScene().buildIndex + 1));
        }

        lineDrawer.setisMenuOpen(false);
        lineDrawer.setIsDrawed(false);
        menu.SetActive(false);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "EndScene")
        {
            lineDrawer = FindObjectOfType<LineDrawer>();
            lineDrawer.setisMenuOpen(true);
        }

    }

    public void reStart()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
