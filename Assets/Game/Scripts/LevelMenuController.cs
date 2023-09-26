using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class LevelMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject levelButtonTemplate;
        [SerializeField] private RectTransform spawnParent;
        [SerializeField] private List<SceneAsset> gameScenes;

        public List<SceneAsset> GameScenes => gameScenes;

        public void CreateLevelButtons()
        {
            foreach (var scene in gameScenes)
            {
                var sceneGameObject = Instantiate(levelButtonTemplate, spawnParent);
                string sceneName = scene.name;
                ConfigureLevelButton(sceneGameObject, sceneName);
            }
        }

        private void ConfigureLevelButton(GameObject sceneGameObject, string sceneName)
        {
            var button = sceneGameObject.GetComponent<Button>();
            button.onClick.AddListener(() => LoadLevel(sceneName));

            var buttonText = sceneGameObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = sceneName;
        }

        private void LoadLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}