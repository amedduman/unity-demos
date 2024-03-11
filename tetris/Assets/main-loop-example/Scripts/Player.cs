using UnityEngine;

public class Player : MonoBehaviour
{
    GameLoopExampleGameManager _gameManager;
    
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _moveSpeed = 10f;

    public void OnCreated(GameLoopExampleGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void OnUpdate(GameLoopExampleGameManager.GameInput input)
    {
        _characterController.Move(new Vector3(input.Horizontal * _moveSpeed * Time.deltaTime, 
            0f,
            input.Vertical * _moveSpeed * Time.deltaTime));
    }
}