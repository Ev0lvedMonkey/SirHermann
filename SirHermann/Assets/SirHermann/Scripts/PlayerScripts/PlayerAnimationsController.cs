using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;

    private const string MovementParameter = "CharacterAnimHorizontalInput";
    private const string JumpParameter = "CharacterAnimIsJumping";
    private const string CrouchParameter = "CharacterAnimIsCrouching";

    private void OnValidate()
    {
        if(_animator == null)
            _animator = GetComponent<Animator>();
        if(_playerMovement == null)
            _playerMovement = GetComponent<PlayerMovement>();
    }

    public void UpdateAnimationState(float horizontalInput, bool isJumping, bool isCrouching)
    {
        _animator.SetFloat(MovementParameter, Mathf.Abs(horizontalInput));
        _animator.SetBool(JumpParameter, isJumping);
        _animator.SetBool(CrouchParameter, isCrouching);
    }

}
