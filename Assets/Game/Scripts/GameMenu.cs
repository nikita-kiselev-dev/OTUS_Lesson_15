using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelNameText;

        [SerializeField] private GameObject backToMenuCanvas;
        [SerializeField] private TextMeshProUGUI gameResult;

        private string levelName;

        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            levelName = currentScene.name;
            levelNameText.text = levelName;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void ShowGameResult(string result)
        {
            backToMenuCanvas.SetActive(true);
            
            if (result == "win")
            {
                gameResult.text = "YOU WIN!";
            }
            else
            {
                gameResult.text = "YOU LOSE!";
            }
        }
        
        public void RestartLevel()
        {
            SceneManager.LoadScene(levelName);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}