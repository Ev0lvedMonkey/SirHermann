using UnityEngine;

public class PlayerAnimationsController
{
    private const string MovementParameter = "CharacterAnimHorizontalInput";
    private const string JumpParameter = "CharacterAnimIsJumping";
    private const string CrouchParameter = "CharacterAnimIsCrouching";

    private Animator _animator;

    public PlayerAnimationsController(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateAnimationState(float horizontalInput, bool isJumping, bool isCrouching)
    {
        _animator.SetFloat(MovementParameter, Mathf.Abs(horizontalInput));
        _animator.SetBool(JumpParameter, isJumping);
        _animator.SetBool(CrouchParameter, isCrouching);
    }

}
