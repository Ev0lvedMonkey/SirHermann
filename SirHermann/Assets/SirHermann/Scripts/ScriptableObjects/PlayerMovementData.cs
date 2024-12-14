using UnityEngine;

[CreateAssetMenu(menuName = "Player Run Data")] 
public class PlayerMovementData : ScriptableObject
{
    [Header("Run Properties")]
    [Tooltip("Maximum running speed of the player.")]
    [SerializeField] private float _runMaxSpeed = 10f;

    [Tooltip("Acceleration while running.")]
    [SerializeField] private float _runAcceleration = 5f;

    [Tooltip("Max jump count")]
    [SerializeField] private int _maxJump = 1;

    [SerializeField] private float _jumpForce = 10f;

    [Tooltip("Deceleration while stopping.")]
    [SerializeField] private float _runDeceleration = 5f;

    [Tooltip("Airborne acceleration multiplier.")]
    
    [SerializeField, Range(0.01f, 1)] private float _accelInAir = 0.5f;

    [Tooltip("Airborne deceleration multiplier.")]
    [SerializeField, Range(0.01f, 1)] private float _decelInAir = 0.5f;

    [Tooltip("Preserve momentum during movement transitions.")]
    [SerializeField] private bool _doConserveMomentum = true;

    public float RunMaxSpeed => _runMaxSpeed;
    public int MaxJump => _maxJump;
    public float JumpForce => _jumpForce;
    public float RunAcceleration => _runAcceleration;
    public float RunDeceleration => _runDeceleration;
    public float AccelInAir => _accelInAir;
    public float DecelInAir => _decelInAir;
    public bool DoConserveMomentum => _doConserveMomentum;

    public float RunAccelAmount => CalculateAccelerationAmount();
    public float RunDeccelAmount => CalculateDecelerationAmount();

    private void OnValidate()
    {
        _runAcceleration = Mathf.Clamp(_runAcceleration, 0.01f, _runMaxSpeed);
        _runDeceleration = Mathf.Clamp(_runDeceleration, 0.01f, _runMaxSpeed);
    }

    private float CalculateAccelerationAmount()
    {
        return (_runMaxSpeed > 0) ? (50f * _runAcceleration) / _runMaxSpeed : 0f;
    }

    private float CalculateDecelerationAmount()
    {
        return (_runMaxSpeed > 0) ? (50f * _runDeceleration) / _runMaxSpeed : 0f;
    }

}
