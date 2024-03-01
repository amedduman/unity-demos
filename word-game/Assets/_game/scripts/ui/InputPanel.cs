using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordGame
{
    public class InputPanel : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TMP_InputField wordInputField;
        [SerializeField] Button enterButton;
        
        void Awake()
        {
            panel.SetActive(false);
        }

        void OnEnable()
        {
            Game.onStartingTileAndDirectionSet.AddListener(ShowPanel);
            enterButton.onClick.AddListener(HandleEntering);
        }

        void OnDisable()
        {
            Game.onStartingTileAndDirectionSet.RemoveListener(ShowPanel);
            enterButton.onClick.RemoveListener(HandleEntering);
        }

        void ShowPanel()
        {
            panel.SetActive(true);
        }
        
        void HandleEntering()
        {
            if (string.IsNullOrEmpty(wordInputField.text)) return;
            panel.SetActive(false);
            Game.wordInputData.word = wordInputField.text;
            Game.words.Add(wordInputField.text);

            var input = new WordInputData();
            input.tile = Game.wordInputData.tile;
            input.word = Game.wordInputData.word;
            input.dir = Game.wordInputData.dir;
            Game.WordInputDataList.Add(input);
            
            Game.onWordCreationPanelEnterButtonPressed.Invoke();
        }
    }
}
