using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    static GameObject winScreen;
    static GameObject menu;
    static GameObject credits;

    private static float startTime;
    private static int winScreenDelay = 5;

    public static int WinScreenDelay
    {
        get
        {
            return winScreenDelay;
        }
    }

    public static void ShowWinScreen()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        GameObject.Find("Time").GetComponent<Text>().text += (int)(Time.time - startTime) - WinScreenDelay + " sec.";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        StartGame();
    }

    void StartGame()
    {
        Time.timeScale = 1;
    }

    public void LoadGameplaySettings()
    {
        SceneManager.LoadScene("GameplaySettings");
        StartGame();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void QuitCredits()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Use this for initialization
    void Start()
    {
        winScreen = GameObject.Find("WinScreen");
        menu = GameObject.Find("Menu");
        credits = GameObject.Find("Credits");

        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            winScreen.SetActive(false);
            startTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            LoadMenu();
    }
}
