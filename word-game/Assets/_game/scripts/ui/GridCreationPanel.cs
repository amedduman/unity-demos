using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WordGame
{
    public class GridCreationPanel : MonoBehaviour
    {
        [SerializeField] TMP_InputField rowsInput;
        [SerializeField] TMP_InputField columnsInput;
        [SerializeField] Button generateButton;

        int rows;
        int columns;

        void OnEnable()
        {
            rowsInput.onValueChanged.AddListener(OnRowsInputChanged);
            columnsInput.onValueChanged.AddListener(OnColumnsInputChanged);
            generateButton.onClick.AddListener(HandleButtonClick);
        }

        void OnDisable()
        {
            rowsInput.onValueChanged.RemoveListener(OnRowsInputChanged);
            columnsInput.onValueChanged.RemoveListener(OnColumnsInputChanged);
            generateButton.onClick.RemoveListener(HandleButtonClick);
        }

        void OnRowsInputChanged(string txt)
        {
            int.TryParse(txt, out rows);
        }
        
        void OnColumnsInputChanged(string txt)
        {
            int.TryParse(txt, out columns);
        }
        
        void HandleButtonClick()
        {
            Game.onGenerateBtnClicked.CallTheEvent(rows, columns);
        }
    }
}
