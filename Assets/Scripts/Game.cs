using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    static GameObject loseScreen;
    static GameObject winScreen;
    static GameObject menu;
    static GameObject credits;
    static GameObject potatoPrefab;

    private static int highscore = 0;
    private static int score = 0;
    public static int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            GameObject.Find("Score").GetComponent<Text>().text = "Potatoes in the air: " + score;
            if (score > highscore)
                highscore = score;
            if (score == 0)
                ShowLoseScreen();
        }
    }

    private static int losingMashedPotatoes = 3;
    private static int mashedPotatoes = 0;
    public static int MashedPotatoes
    {
        get
        {
            return mashedPotatoes;
        }

        set
        {
            mashedPotatoes = value;
            GameObject.Find("PotatoesAlive").GetComponent<Image>().fillAmount = (float)(losingMashedPotatoes - mashedPotatoes) / losingMashedPotatoes;
            if (mashedPotatoes == losingMashedPotatoes)
                ShowLoseScreen();
        }
    }

    public static void CreatePotato()
    {
        Instantiate(potatoPrefab);
    }

    public static void ShowLoseScreen()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
        GameObject.Find("Highscore").GetComponent<Text>().text += highscore;
    }

    public static void ShowWinScreen()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
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
            winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            LoadMenu();
    }
}
