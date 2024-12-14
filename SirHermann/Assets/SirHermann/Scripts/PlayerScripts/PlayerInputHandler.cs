using System;
using UnityEngine;

public class PlayerInputHandler
{
    public event Action OnJump;
    public event Action OnCrouch;
    public event Action OnCrouchCanceled;

    public Vector2 MoveInput { get; private set; }

    private GameInput _gameInput;

    public PlayerInputHandler()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        _gameInput.Gameplay.Jump.performed += ctx => OnJump?.Invoke();
        _gameInput.Gameplay.Crouch.performed += ctx => OnCrouch?.Invoke();
        _gameInput.Gameplay.Crouch.canceled += ctx => OnCrouchCanceled?.Invoke();
    }

    public void ProcessInput()
    {
        MoveInput = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
    }
}
