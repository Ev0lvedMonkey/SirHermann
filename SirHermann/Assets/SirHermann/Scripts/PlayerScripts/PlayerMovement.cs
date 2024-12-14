using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IService
{
    private PlayerInputHandler _inputHandler;
    private MovementController _movementController;
    private PlayerAnimationsController _animationsController;
    private PlayerSpawn _playerSpawn;
    private PlayerMovementRestrictions _movementRestrictions;

    private bool _canRun = true;
    private bool _canJump = true;
    private bool _canCrouch = true;

    [Header("Links")]
    [SerializeField] private PlayerMovementData _movementData;
    [SerializeField] private SceneTransitionData _sceneTransitionData;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckRadius = 0.3f;

    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();        
    }

    private void Awake()
    {
        _inputHandler = new PlayerInputHandler();
        _movementController = new MovementController(_rigidbody, _movementData, _groundCheckPoint, _groundCheckRadius, _groundLayer);
        _animationsController = new PlayerAnimationsController(_animator);
        _playerSpawn = new PlayerSpawn(transform, _sceneTransitionData);
        _movementRestrictions = new PlayerMovementRestrictions(this);
    }

    private void Start()
    {
        _playerSpawn.Spawn();
        _movementRestrictions.DecidePlayerRestrictions();
    }

    private void OnEnable()
    {
        _inputHandler.OnJump += HandleJump;
        _inputHandler.OnCrouch += HandleCrouch;
        _inputHandler.OnCrouchCanceled += HandleCrouchCancel;
    }

    private void OnDisable()
    {
        _inputHandler.OnJump -= HandleJump;
        _inputHandler.OnCrouch -= HandleCrouch;
        _inputHandler.OnCrouchCanceled -= HandleCrouchCancel;
    }

    private void Update()
    {
        _inputHandler.ProcessInput();
        _movementController.UpdateGroundCheck();
        Vector2 moveInput = _inputHandler.MoveInput;

        if (_canRun)
            _movementController.Run(moveInput.x);


        if (moveInput.x != 0)
        {
            _movementController.CheckDirectionToFace(moveInput.x > 0);
        }

        _animationsController.UpdateAnimationState(
            moveInput.x,
            _movementController.IsJumping,
            _movementController.IsCrouching
        );
    }

    public void SetCanRun(bool value) => _canRun = value;
    public void SetCanJump(bool value) => _canJump = value;
    public void SetCanCrouch(bool value) => _canCrouch = value;


    private void HandleJump()
    {
        if (_canJump)
            _movementController.Jump();
    }

    private void HandleCrouch()
    {
        if (_canCrouch)
            _movementController.StartCrouching();
    }

    private void HandleCrouchCancel()
    {
        _movementController.StopCrouching();
    }
}
