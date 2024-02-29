using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordGame
{
    public class SavePanel : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] Button saveButton;
        [SerializeField] TMP_InputField levelInputField;

        void Awake()
        {
            HidePanel();
        }

        void OnEnable()
        {
            Game.onStartingTileAndDirectionSet.AddListener(HidePanel);
            Game.onWordCreationPanelEnterButtonPressed.AddListener(ShowPanel);
            
            saveButton.onClick.AddListener(Save);
        }

        void OnDisable()
        {
            Game.onStartingTileAndDirectionSet.RemoveListener(HidePanel);
            Game.onWordCreationPanelEnterButtonPressed.RemoveListener(ShowPanel);
            
            saveButton.onClick.RemoveListener(Save);
        }

        void HidePanel()
        {
            panel.SetActive(false);
        }

        void ShowPanel()
        {
            panel.SetActive(true);
        }

        void Save()
        {
            var levelName = levelInputField.text;
            if (string.IsNullOrEmpty(levelName)) return;
            LevelDataSaver.Save(levelName);
        }
    }
}
