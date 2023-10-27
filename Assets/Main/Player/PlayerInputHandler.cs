using System;
using Main.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private CustomInput _input = null;
        private Vector2 _movementAxis = Vector2.zero;
        private Vector2 _viewAxis = Vector2.zero;
        public event Action<InputActionEventType> OnActionCast;
        public Vector2 MovementAxis => _movementAxis;
        public Vector2 ViewAxis => _viewAxis;

        public void Init() => _input = new CustomInput();

        private void Awake() => Init();

        private void OnMovementPerformed(InputAction.CallbackContext value)
            => _movementAxis = value.ReadValue<Vector2>();

        private void OnMovementCancelled(InputAction.CallbackContext value)
            => _movementAxis = Vector2.zero;

        private void OnViewPerformed(InputAction.CallbackContext value)
            => _viewAxis = value.ReadValue<Vector2>();

        private void OnViewCancelled(InputAction.CallbackContext value)
            => _viewAxis = Vector2.zero;

        private void CastInteractEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.INTERACT);

        private void CastEscapeEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.ESCAPE);

        private void CastLClickEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.LEFT_CLICK);

        private void CastRClickEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.RIGHT_CLICK);

        private void CastCrouchEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.CROUCH);

        private void CastJumpEvent(InputAction.CallbackContext value)
            => OnActionCast?.Invoke(InputActionEventType.JUMP);

        private void SubscribeOnInput()
        {
            _input.Player.Movement.performed += OnMovementPerformed;
            _input.Player.Movement.canceled += OnMovementCancelled;
            _input.Player.View.performed += OnViewPerformed;
            _input.Player.View.canceled += OnViewCancelled;

            _input.Player.Interact.started += CastInteractEvent;
            _input.Player.Escape.started += CastEscapeEvent;
            _input.Player.MouseLeftButtonClick.started += CastLClickEvent;
            _input.Player.MouseRightButtonClick.started += CastRClickEvent;
            _input.Player.Jump.started += CastJumpEvent;
            _input.Player.Crouch.started += CastCrouchEvent;
        }

        private void UnsubscribeOnInput()
        {
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCancelled;
            _input.Player.View.performed -= OnViewPerformed;
            _input.Player.View.canceled -= OnViewCancelled;

            _input.Player.Interact.started -= CastInteractEvent;
            _input.Player.Escape.started -= CastEscapeEvent;
            _input.Player.MouseLeftButtonClick.started -= CastLClickEvent;
            _input.Player.MouseRightButtonClick.started -= CastRClickEvent;
            _input.Player.Jump.started -= CastJumpEvent;
            _input.Player.Crouch.started -= CastCrouchEvent;
        }

        private void OnEnable()
        {
            _input.Enable();
            SubscribeOnInput();
            OnActionCast += PrintActionType;
        }
        
        private void OnDisable()
        {
            _input.Disable();
            UnsubscribeOnInput();
            OnActionCast -= PrintActionType;
        }


        //DEBUG
        private void PrintActionType(InputActionEventType type) => Debug.Log(type);
        private void FixedUpdate()
        {
            if (_movementAxis != Vector2.zero) Debug.Log("Mov: " + _movementAxis);
            if (_viewAxis != Vector2.zero) Debug.Log("View: " + _viewAxis);
        }
    }
}