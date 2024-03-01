using UnityEngine;

namespace WordGame
{
    public class LetterWheel : MonoBehaviour
    {
        [SerializeField] LevelGenerator levelGenerator;
        [SerializeField] string word;

        [ContextMenu("search")]
        void OnCompleteWord()
        {
            levelGenerator.TryMatchWord(word);
        }
    }
}
