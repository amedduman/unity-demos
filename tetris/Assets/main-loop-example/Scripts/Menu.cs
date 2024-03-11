using UnityEngine;

public class Menu : MonoBehaviour
{
    GameLoopExampleGameManager _gameManager;

    public bool IsOpen { get; set; }
    
    public void OnCreated(GameLoopExampleGameManager gameManager)
    {
        _gameManager = gameManager;
        IsOpen = false;
        gameObject.SetActive(false);
    }

    public void OnUpdate(GameLoopExampleGameManager.GameInput input)
    {
        // Do some menu stuff
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        gameObject.SetActive(IsOpen);
    }
}