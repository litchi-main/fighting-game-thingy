using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class GravityController : MonoBehaviour
{
    public float YVelocity {  get; private set; }
    public bool IsGrounded {  get; private set; }

    [Header("Params")]
    [SerializeField] private float _gravityScale = 1.5f;
    [SerializeField] private float _maxVelocity = 40f;
    [SerializeField] private float _minVelocity = -40f;

    [Header("Components")]
    [SerializeField] private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        YVelocity = 0;
        IsGrounded = false;
    }

    private void FixedUpdate()
    {

        CollisionFlags collisionFlags = _characterController.Move(Vector2.up * YVelocity * Time.fixedDeltaTime);

        bool isGrounded = collisionFlags.HasFlag(CollisionFlags.Below);

        if (collisionFlags.HasFlag(CollisionFlags.Above))
        {
            YVelocity = Mathf.Min(YVelocity, 0f);
        }
            

        if (!isGrounded
            && IsGrounded)
        {
            IsGrounded = false;
        }
        else if (isGrounded)
        {
            IsGrounded = true;
            YVelocity = 0f;
        }

        YVelocity += Physics.gravity.y * _gravityScale * Time.fixedDeltaTime;
        YVelocity = Mathf.Clamp(YVelocity, _minVelocity, _maxVelocity);

    }

    public void SetVelocity(float velocity)
    {
        YVelocity = velocity;
    }

    public void AddVelocity(float velocity)
    {
        YVelocity += velocity;
    }

#if UNITY_EDITOR

    [ContextMenu(nameof(TryGetComponents))]
    private void TryGetComponents()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnValidate()
    {
        if ( _maxVelocity < _minVelocity ) 
            _maxVelocity = _minVelocity;
    }

#endif
}