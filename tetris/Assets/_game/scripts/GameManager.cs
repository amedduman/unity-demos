using UnityEngine;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] GridHandler gridHandler;
        [SerializeField] BlockSpawner blockSpawner;
        
        void Start()
        {
            // gridHandler.Init(cameraController);
            // blockSpawner.Init();
        }
    }
}