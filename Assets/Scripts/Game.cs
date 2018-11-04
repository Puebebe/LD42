﻿using System.Collections;
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

    private static int lesson;
    private const int NUMBER_OF_LESSONS = 4;
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

    private static bool isOver;

    public static bool IsOver
    {
        get
        {
            return isOver;
        }
    }

    public static Dictionary<string, bool> Objectives;

    public static bool ObjectivesAllDone()
    {
        foreach (bool done in Objectives.Values)
        {
            if (!done)
                return false;
        }

        return true;
    }

    public static void ShowWinScreen()
    {
        Time.timeScale = 0;
        Game.isOver = true;
        winScreen.SetActive(true);
        var time = GameObject.Find("Time");
        if (time != null)
            time.GetComponent<Text>().text += (int)(Time.time - startTime - WinScreenDelay) + " sec.";

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            winScreen.GetComponentInChildren<Text>().text = "Lesson Nº " + lesson + " completed!";

            if (lesson == NUMBER_OF_LESSONS)
            {
                GameObject.Find("Next").GetComponentInChildren<Text>().text = "Main menu";
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
    /*
    public void StartTutorial()
    {
        Parameters.parameterSize = new Vector3(1, 1, 1);
        Parameters.parameterRotation = true;
        SceneManager.LoadScene("Tutorial");
    }
    */
    public void StartTutorial()
    {
        string nextScene = "Tutorial";
        lesson++;
        Vector3 size = Vector3.one;

        switch (lesson)
        {
            case 1:
                size = new Vector3(1, 1, 1);
                break;
            case 2:
                size = new Vector3(2, 1, 1);
                break;
            case 3:
                size = new Vector3(2, 2, 2);
                break;
            case 4:
                size = new Vector3(3, 3, 3);
                break;
            default:
                nextScene = "Menu";
                lesson = 0;
                break;
        }

        Parameters.parameterSize = size;
        Parameters.parameterRotation = true;
        SceneManager.LoadScene(nextScene);
        Time.timeScale = 1;
    }

    public void LoadGameplaySettings()
    {
        SceneManager.LoadScene("GameplaySettings");
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        lesson = 0;
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
        Game.isOver = false;
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

        Objectives = new Dictionary<string, bool>(10);
        Objectives.Add("Grab", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (SceneManager.GetActiveScene().name == "Menu")
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                LoadMenu();
            else if (Input.GetKeyDown(KeyCode.R))
                RestartGame();
            else if (Input.GetKey(KeyCode.Semicolon) && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Alpha1) && !Game.IsOver)
                ShowWinScreen();
        }
    }
}
