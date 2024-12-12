using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigitbody;
    private Vector2 _moveInput;

    private bool _isJumping;
    private bool _isCrouching;
    private bool _isFacingRight;

    private float _lastOnGroundTime;
    private int _remainingJumps;

    [Header("Links")]
    [SerializeField] private PlayerMovementData Data;
    [SerializeField] private PlayerAnimationsController _animationsController;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckCircleRaduis = 0.3f;

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpCutMultiplier = 0.5f; 
    [SerializeField] private int maxJumps = 1; 

    private void Awake()
    {
        _rigitbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _isFacingRight = true;
        _remainingJumps = maxJumps;
    }

    private void Update()
    {
        _lastOnGroundTime -= Time.deltaTime;

        _moveInput.x = Input.GetAxisRaw("Horizontal");

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        if (Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckCircleRaduis, _groundLayer))
        {
            _lastOnGroundTime = 0.01f;
            _remainingJumps = maxJumps;
            _isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_lastOnGroundTime > 0 || _remainingJumps > 0)
            {
                Jump();
            }
        }

        if(Input.GetKey(KeyCode.LeftControl))
            _isCrouching = true;

        if(Input.GetKeyUp(KeyCode.LeftControl))
            _isCrouching = false;


        if (Input.GetKeyUp(KeyCode.Space) && _rigitbody.velocity.y > 0)
        {
            _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, _rigitbody.velocity.y * jumpCutMultiplier);
        }
        _animationsController.UpdateAnimationState(_moveInput.x, _isJumping, _isCrouching);
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;

        float accelRate;
        if(_isCrouching && _lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * 0.2f : Data.runDeccelAmount * 0.2f;
        else if (_lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;

        if (Data.doConserveMomentum && Mathf.Abs(_rigitbody.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(_rigitbody.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && _lastOnGroundTime < 0)
        {
            accelRate = 0;
        }

        float speedDif = targetSpeed - _rigitbody.velocity.x;

        float movement = speedDif * accelRate;

        _rigitbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private void Jump()
    {
        _rigitbody.velocity = new Vector2(_rigitbody.velocity.x, 0);

        _rigitbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        _remainingJumps--;
        _isJumping = true;

    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        _isFacingRight = !_isFacingRight;
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
            Turn();
    }
}
