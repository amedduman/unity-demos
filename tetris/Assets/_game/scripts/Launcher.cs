using System.Collections;
using UnityEngine;

namespace Tetris
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] CameraController cameraController;
        [SerializeField] GridHandler gridHandler;
        [SerializeField] BlockSpawner blockSpawner;
        
        IEnumerator Start()
        {
            var gridInitProcess = gridHandler.Init(cameraController);
            yield return gridInitProcess.Item1;
            blockSpawner.Init(gridInitProcess.Item2);
        }
    }
}