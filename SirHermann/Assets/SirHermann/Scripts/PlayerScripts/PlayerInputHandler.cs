using System;
using UnityEngine;

public class PlayerInputHandler
{
    public Action OnJump;
    public Action OnCrouch;
    public Action OnCrouchCanceled;

    public Vector2 MoveInput { get; private set; }

    public GameInput gameInput;

    public PlayerInputHandler()
    {
        gameInput = new GameInput();
        gameInput.Enable();
        gameInput.Gameplay.Jump.performed += ctx => OnJump?.Invoke();
        gameInput.Gameplay.Crouch.performed += ctx => OnCrouch?.Invoke();
        gameInput.Gameplay.Crouch.canceled += ctx => OnCrouchCanceled?.Invoke();
    }

    public void ProcessInput()
    {
        MoveInput = gameInput.Gameplay.Movement.ReadValue<Vector2>();
    }
}
