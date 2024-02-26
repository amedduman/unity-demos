using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace WordGame
{
    [CreateAssetMenu(menuName = "_game/InputHandler")]
    public class InputHandler : ScriptableObject, GameInputActions.IGameplayActions
    {
        public Vector2 mousePos { get; private set; }
        public event UnityAction TouchStart = delegate { };
        public event UnityAction TouchEnd = delegate { };
        public event Action OnTap;
        GameInputActions gameInputActions;

        public void OnEnable()
        {
            if (gameInputActions == null)
            {
                gameInputActions = new GameInputActions();
                gameInputActions.gameplay.SetCallbacks(this);
            }
            gameInputActions.Enable();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            mousePos = context.ReadValue<Vector2>();
        }

        public void OnTouch(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    TouchStart.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    TouchEnd.Invoke();
                    break;
            }
        }

        public void OnTapClick(InputAction.CallbackContext context)
        {
            OnTap?.Invoke();
        }
    }
}
