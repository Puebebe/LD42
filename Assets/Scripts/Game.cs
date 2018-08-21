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
    static GameObject helpScreen;

    private static float startTime;
    private static float winScreenDelay;

    public static float WinScreenDelay
    {
        get
        {
            return winScreenDelay;
        }

        set
        {
            winScreenDelay = value;
        }
    }

    public static void ShowWinScreen()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        var time = GameObject.Find("Time");
        if (time != null)
            time.GetComponent<Text>().text += (int)(Time.time - startTime - WinScreenDelay) + " sec.";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }

    public void StartTutorial()
    {
        Parameters.parameterSize = new Vector3(1, 1, 1);
        Parameters.parameterRotation = true;
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadGameplaySettings()
    {
        SceneManager.LoadScene("GameplaySettings");
        Time.timeScale = 1;
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

    public static void ShowHelpScreen(bool state)
    {
        helpScreen.SetActive(state);
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
        helpScreen = GameObject.Find("HelpScreen");

        if (SceneManager.GetActiveScene().name == "Gameplay" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            winScreen.SetActive(false);
            helpScreen.SetActive(false);
            startTime = Time.time;
            winScreenDelay = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            LoadMenu();
    }
}
