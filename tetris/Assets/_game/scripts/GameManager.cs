using UnityEngine;

namespace Tetris
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] GridHandler gridHandler;
        
        void Start()
        {
            gridHandler.OnStart(cameraController);
        }
    }
}