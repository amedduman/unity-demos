using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace WordGame
{
    public class LetterWheel : MonoBehaviour
    {
        [SerializeField] LevelGenerator levelGenerator;
        [SerializeField] Letter letterPrefab;
        [SerializeField] float radius = 2.0f;
        [SerializeField] float centerY = 200;
        [SerializeField] string word;
        [SerializeField] InputHandler inputHandler;
        List<Letter> letterButtons = new List<Letter>();
        

        void OnEnable()
        {
            Game.onLevelGenerated.AddListener(OnLevelGenerated);
            inputHandler.OnTouchEnd += TryMatch;
        }

        void OnDisable()
        {
            Game.onLevelGenerated.RemoveListener(OnLevelGenerated);
            inputHandler.OnTouchEnd -= TryMatch;
        }
        
        void TryMatch()
        {
            levelGenerator.TryMatchWord(word);

            word = string.Empty;

            foreach (var letterButton in letterButtons)
            {
                letterButton.UnRegister();
            }
        }

        [ContextMenu("search")]
        void OnCompleteWord()
        {
            levelGenerator.TryMatchWord(word);
        }

        void OnLevelGenerated(List<WordData> wordDataList)
        {
            List<char> letters = new List<char>(); 
            foreach (WordData wordData in wordDataList)
            {
                foreach (var c in wordData.word)
                {
                    if (letters.Contains(c)) continue;
                    letters.Add(c);
                }
            }

            // foreach (var l in letters)
            // {
            //     var letter = Instantiate(letterPrefab, transform.position, quaternion.identity, transform);
            //     letter.SetText(l);
            // }

            Vector3 center = transform.position;
            center += new Vector3(0, centerY, 0);
            
            
            float angleIncrement = 360f / letters.Count;
            
            for (int i = 0; i < letters.Count; i++)
            {
                // Calculate the angle for each letter
                float angle = i * angleIncrement;

                // Convert polar coordinates to Cartesian coordinates
                float x = center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                float y = center.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

                // Instantiate the letter prefab at the calculated position
                var letter = Instantiate(letterPrefab, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
                letter.SetText(letters[i]);
                
                letterButtons.Add(letter);
            }
        }

        public void AddLetterToWord(string l)
        {
            word += l;
        }
        
        
    }
}
