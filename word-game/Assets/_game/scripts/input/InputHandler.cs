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
        public event Action OnTap;
        public event Action OnTouchStart;
        public event Action OnTouchEnd;
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

        public void Disable()
        {
            gameInputActions.Disable();
        }

        public void Enable()
        {
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
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    OnTouchStart?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnTouchEnd?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnTapClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    OnTap?.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}
