using System.Collections.Generic;
using UnityEngine;

namespace WordGame
{
    public class LetterWheel : MonoBehaviour
    {
        [SerializeField] LevelGenerator levelGenerator;
        [SerializeField] Letter letterPrefab;
        [SerializeField] float radius = 2.0f;
        [SerializeField] string word;
        [SerializeField] InputHandler inputHandler;
        List<Letter> letterButtons = new List<Letter>();
        [SerializeField] LineRenderer line;
        bool canLineFollowMouse = false;
        [SerializeField] LayerMask layer;
        

        void OnEnable()
        {
            Game.onLevelGenerated.AddListener(OnLevelGenerated);
            inputHandler.OnTouchStart += HandleTouchStart;
            inputHandler.OnTouchEnd += HandleTouchEnd;
        }

        void OnDisable()
        {
            Game.onLevelGenerated.RemoveListener(OnLevelGenerated);
            inputHandler.OnTouchStart -= LineFollowMouse;
            inputHandler.OnTouchEnd -= HandleTouchEnd;
        }

        void Update()
        {
            if (canLineFollowMouse)
                LineFollowMouse();
        }
        
        void LineFollowMouse()
        {
            line.SetPosition(0, transform.position);
            var ray = Camera.main.ScreenPointToRay(new Vector3(inputHandler.mousePos.x, inputHandler.mousePos.y, 0));
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 1000, layer);
            if (hit.transform != null)
            {
                var lastIndex = line.positionCount - 1;
                // Debug.Log(hit.point);
                line.SetPosition(lastIndex, hit.point);
            }
        }

        void HandleTouchEnd()
        {
            canLineFollowMouse = false;
            
            levelGenerator.TryMatchWord(word);

            line.positionCount = 0;

            word = string.Empty;

            foreach (var letterButton in letterButtons)
            {
                letterButton.UnRegister();
            }
        }

        void HandleTouchStart()
        {
            line.positionCount = 2;
            canLineFollowMouse = true;
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
            
            float angleIncrement = 360f / letters.Count;
            
            for (int i = 0; i < letters.Count; i++)
            {
                // Calculate the angle for each letter
                float angle = i * angleIncrement;

                // Convert polar coordinates to Cartesian coordinates
                float x = center.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                float y = center.y + radius * Mathf.Sin(Mathf.Deg2Rad * angle);

                // Instantiate the letter prefab at the calculated position
                var letter = Instantiate(letterPrefab, new Vector3(x, y, transform.position.z - 1), Quaternion.identity, transform);
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
