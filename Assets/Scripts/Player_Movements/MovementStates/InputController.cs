using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player_Movements.MovementStates
{
    public class InputController : MonoBehaviour
    {
        private InputSystem_Actions _inputSystem;
        private MovementStateManager _player;
        private AimStateManager _aim;

        void Start()
        {
            _player = GetComponent<MovementStateManager>();
            _aim = GetComponent<AimStateManager>();
        }

        void HandleMouseVector(InputAction.CallbackContext context)
        {
            _aim.HandleAimDirection(context.ReadValue<Vector2>());
        }

        void HandleMovement(InputAction.CallbackContext context)
        {
            _player.SetDirection(context.ReadValue<Vector2>());
        }
        void OnEnable()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Look.performed += HandleMouseVector;
            _inputSystem.Player.Move.performed += HandleMovement;
            _inputSystem.Player.Move.canceled += HandleMovement;
            _inputSystem.Enable();
            //
        }
        void OnDisable()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Look.performed -= HandleMouseVector;
            _inputSystem.Player.Move.performed -= HandleMovement;
            _inputSystem.Player.Move.canceled -= HandleMovement;
            _inputSystem.Disable();
            //
        }
    }
}