using UnityEngine;

public class MovementController
{
    private Rigidbody2D _rigidbody;
    private PlayerMovementData _data;
    private Transform _groundCheckPoint;
    private float _groundCheckRadius;
    private LayerMask _groundLayer;

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isCrouching;
    private bool _isFacingRight;
    private int _remainingJumps;

    public bool IsJumping => _isJumping;
    public bool IsCrouching => _isCrouching;

    public MovementController(Rigidbody2D rigidbody, PlayerMovementData data, Transform groundCheckPoint, float groundCheckRadius, LayerMask groundLayer)
    {
        _rigidbody = rigidbody;
        _data = data;
        _groundCheckPoint = groundCheckPoint;
        _groundCheckRadius = groundCheckRadius;
        _groundLayer = groundLayer;

        _remainingJumps = data.MaxJump;
    }

    public void UpdateGroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded)
        {
            _remainingJumps = _data.MaxJump;
            _isJumping = false;
        }
    }

    public void Run(float horizontalInput)
    {
        float targetSpeed = horizontalInput * _data.RunMaxSpeed;
        float speedDiff = targetSpeed - _rigidbody.velocity.x;
        float accelerationRate = _isGrounded ? _data.RunAccelAmount : _data.RunAccelAmount * _data.AccelInAir;

        float movement = speedDiff * accelerationRate;
        _rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public void Jump()
    {
        if (_remainingJumps > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _data.JumpForce, ForceMode2D.Impulse);
            _remainingJumps--;
            _isJumping = true;
        }
    }

    public void StartCrouching()
    {
        _isCrouching = true;
    }

    public void StopCrouching()
    {
        _isCrouching = false;
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != _isFacingRight)
        {
            _isFacingRight = isMovingRight;
            Vector3 scale = _rigidbody.transform.localScale;
            scale.x *= -1;
            _rigidbody.transform.localScale = scale;
        }
    }
}

