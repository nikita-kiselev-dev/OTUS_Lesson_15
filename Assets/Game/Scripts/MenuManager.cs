using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    enum Screen
    {
        None,
        Menu,
        Settings,
        Levels,
        Game
    }

    [SerializeField] private LevelMenuController levelMenuController;

    public CanvasGroup mainScreen;
    public CanvasGroup levelsScreen;
    public CanvasGroup settingsScreen;

    private void Awake()
    {
        if (!settingsScreen)
        {
            settingsScreen = SoundSettings.Instance.GetComponentInChildren<CanvasGroup>();
        }
    }
    private void Start()
    {
        SetCurrentScreen(Screen.Menu);
        levelMenuController.CreateLevelButtons();
    }

    private void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Menu);
        Utility.SetCanvasGroupEnabled(levelsScreen, screen == Screen.Levels);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
    }

    public void StartNewGame()
    {
        SetCurrentScreen(Screen.Game);
        if (levelMenuController.GameScenes[0])
        {
            string sceneName = levelMenuController.GameScenes[0].name;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OpenLevelsMenu()
    {
        SetCurrentScreen(Screen.Levels);
    }

    public void OpenSettings()
    {
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseLevels()
    {
        SetCurrentScreen(Screen.Menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
