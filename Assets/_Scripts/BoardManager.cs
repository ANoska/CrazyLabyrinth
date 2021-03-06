﻿using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode 
{
    idle,
    playing,
    levelEnd
}

public class BoardManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[] boards;         // An array of the boards
    public Text uiTimerText;            // The text element of the UI for the timer
    public Text uiHighScoreText;        // The text element of the UI for the high score.
    public Button uiRestartButton;         // The button element of the UI for restarting the game.

    [Header("Set Dynamically")]
    public int level;                   // The current level
    public int levelMax;                // The number of levels
    public GameObject board;            // The current board
    public Vector3 spawn;               // The current player spawnpoint
    public GameMode mode;
    public GameObject player;

    // High scores pop up members
    private GameObject uiPopUp;
    private Button uiPopUpButton;
    private Text uiPopUpText;

    private float levelStartTime;
    private float timeTaken;

    // Player pref keys
    private const string PLAYER_PREF_HIGH_SCORE = "CrazyLabyrinth_HighScore";
    private const string PLAYER_PREF_EASY_BOARD_TIME = "CrazyLabyrinth_EasyBoardTime";
    private const string PLAYER_PREF_MEDIUM_BOARD_TIME = "CrazyLabyrinth_MediumBoardTime";
    private const string PLAYER_PREF_HARD_BOARD_TIME = "CrazyLabyrinth_HardBoardTime";
    private const string PLAYER_PREF_CRAZY_BOARD_TIME = "CrazyLabyrinth_CrazyBoardTime";

    void Start() 
    {
        InitializeHighScoresPopUp();
        uiRestartButton.onClick.AddListener(OnRestartButtonClicked);

        mode = GameMode.idle;
        level = 0;
        levelMax = boards.Length;
        StartLevel();
    }

    void Update()
    {
        // Update timer
        timeTaken = Time.time - levelStartTime;

        // Check for level end
        if ((mode == GameMode.playing) && GoodHoles.goodHoleMet)
        {
            // Update Player Prefs
            UpdatePlayerPrefs();

            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 1f);
        }
        if ((mode == GameMode.playing) && BadHoles.badHoleMet)
        {
            // Maybe display some cool effect here when the player hits the wrong hole?
        }

        UpdateGUI();
    }

    public void SpawnPlayer()
    {
        // Destroy any old player objects
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }

        // Spawn the new player object
        Instantiate<GameObject>(player, spawn, Quaternion.identity);
    }

    private void StartLevel()
    {
        //reset hole conditions
        GoodHoles.goodHoleMet = false;
        BadHoles.badHoleMet = false;

        // Destroy the old board if one exists
        if (board != null)
        {
            Destroy(board);
        }

        // Destroy the old player if one exists
        var livePlayer = GameObject.FindGameObjectWithTag("Player");
        if (livePlayer != null)
        {
            Destroy(livePlayer);
        }

        // Instantiate the new board
        board = Instantiate<GameObject>(boards[level]);

        // Calculate the new spawn point
        spawn = board.transform.GetChild(0).transform.position;

        // Change the game mode.
        mode = GameMode.playing;

        SpawnPlayer();

        levelStartTime = Time.time;
    }

    private void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    /// <summary>
    /// Gross switch statements that update the high scores
    /// (Probably a better way to do this?)
    /// </summary>
    private void UpdatePlayerPrefs()
    {
        if (PlayerPrefs.GetInt(PLAYER_PREF_HIGH_SCORE) < level + 1)
        {
            PlayerPrefs.SetInt(PLAYER_PREF_HIGH_SCORE, level + 1);

            switch (level)
            {
                case 0:
                    PlayerPrefs.SetFloat(PLAYER_PREF_EASY_BOARD_TIME, timeTaken);
                    break;
                case 1:
                    PlayerPrefs.SetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME, timeTaken);
                    break;
                case 2:
                    PlayerPrefs.SetFloat(PLAYER_PREF_HARD_BOARD_TIME, timeTaken);
                    break;
                case 3:
                    PlayerPrefs.SetFloat(PLAYER_PREF_CRAZY_BOARD_TIME, timeTaken);
                    break;
            }

            NewHighScorePopUp();
        }
        else if(PlayerPrefs.GetInt(PLAYER_PREF_HIGH_SCORE) == level + 1)
        {
            switch (level)
            {
                case 0:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_EASY_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_EASY_BOARD_TIME, timeTaken);
                    }
                    break;

                case 1:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME, timeTaken);
                    }
                    break;

                case 2:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_HARD_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_HARD_BOARD_TIME, timeTaken);
                    }
                    break;

                case 3:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_CRAZY_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_CRAZY_BOARD_TIME, timeTaken);
                    }
                    break;
            }

            NewHighScorePopUp();
        }
        else
        {
            switch (level)
            {
                case 0:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_EASY_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_EASY_BOARD_TIME, timeTaken);
                    }
                    break;

                case 1:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME, timeTaken);
                    }
                    break;

                case 2:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_HARD_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_HARD_BOARD_TIME, timeTaken);
                    }
                    break;

                case 3:
                    if (PlayerPrefs.GetFloat(PLAYER_PREF_CRAZY_BOARD_TIME) > timeTaken)
                    {
                        PlayerPrefs.SetFloat(PLAYER_PREF_CRAZY_BOARD_TIME, timeTaken);
                    }
                    break;
            }
        }
    }

    private void InitializeHighScoresPopUp()
    {
        // Find the pop up
        uiPopUp = GameObject.Find("HighScorePopUp");

        if (uiPopUp == null)
            throw new InvalidOperationException("High Scores Pop Up GameObject not found");

        // Add listeners for the pop up button
        uiPopUpButton = uiPopUp.GetComponentInChildren<Button>();
        uiPopUpButton.onClick.AddListener(CloseHighScoresPopUp);

        // Find the text element we want
        uiPopUpText = uiPopUp.GetComponentsInChildren<Text>().Where(t => t.name == "HighScorePopUpText").FirstOrDefault();

        // Hide the pop up
        uiPopUp.SetActive(false);
    }

    private void NewHighScorePopUp()
    {
        string levelTime = "";
        switch (level)
        {
            case 0:
                levelTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(PLAYER_PREF_EASY_BOARD_TIME)).ToString(@"mm\:ss");
                break;
            case 1:
                levelTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(PLAYER_PREF_MEDIUM_BOARD_TIME)).ToString(@"mm\:ss");
                break;
            case 2:
                levelTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(PLAYER_PREF_HARD_BOARD_TIME)).ToString(@"mm\:ss");
                break;
            case 3:
                levelTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(PLAYER_PREF_CRAZY_BOARD_TIME)).ToString(@"mm\:ss");
                break;
        }

        string popUpMessage = string.Format("New High Score!\nLevels Completed: {0}\nLevel Time: {1}", PlayerPrefs.GetString(PLAYER_PREF_HIGH_SCORE), levelTime);
        uiPopUpText.text = popUpMessage;

        // Pause time and show the pop up
        Time.timeScale = 0;
        uiPopUp.SetActive(true);
    }

    private void CloseHighScoresPopUp()
    {
        // Hide the pop up and resume time
        uiPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    private void UpdateGUI()
    {
        uiTimerText.text = TimeSpan.FromSeconds(timeTaken).ToString(@"mm\:ss");

        // Only show the high score if there is one
        if (PlayerPrefs.HasKey(PLAYER_PREF_HIGH_SCORE))
            uiHighScoreText.text = string.Format("High Score = {0}", PlayerPrefs.GetInt(PLAYER_PREF_HIGH_SCORE).ToString());
    }

    void OnRestartButtonClicked()
    {
        // Restart the game
        level = 0;
        StartLevel();
    }
}