using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(ActionController))]
[RequireComponent(typeof(Player))]

public class HorizontalMovement : MonoBehaviour, IMovementInput
{
    [Header("Params")]
    [SerializeField, Min(0.5f)] private float _walkLeftSpeed = 5f;
    [SerializeField, Min(0.5f)] private float _walkRightSpeed = 10f;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GravityController _gravityController;
    [SerializeField] private ActionController _actionController;
    [SerializeField] private Player _player;

    private Vector2 _xVelocityFinal { get; set; }
    private Vector2 _xVelocityAdditive { get; set; }
    private Vector2 _velocityInput;
    private float _characterRadius;
    private bool _isplayeronme;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
        _actionController = GetComponent<ActionController>();
        _player = GetComponent<Player>();
        _characterRadius = _characterController.radius;
        _isplayeronme = false;
    }

    private void Start()
    {
        _velocityInput = Vector2.zero;
    }

    public float LRMovement()
    {
        float temp = 0f;
        if (Input.GetKey(_player.LeftKey))
            temp--;
        if (Input.GetKey(_player.RightKey))
            temp++;

        return temp;
    }
    public float UDMovement()
    {
        return 0f;
    }

    private void Update()
    {
        _xVelocityFinal *= new Vector2(1, 0);

        _characterController.Move(_xVelocityFinal * Time.deltaTime);

        _isplayeronme = _characterController.collisionFlags.HasFlag(CollisionFlags.Sides);

        _xVelocityFinal = Vector2.zero;

        _velocityInput.Set(LRMovement(), UDMovement());
        if (_gravityController.IsGrounded)
            if (_actionController.IsInNeutral())
                _xVelocityFinal = (_velocityInput.x > 0 ? _walkRightSpeed : _walkLeftSpeed) * _velocityInput;

        _xVelocityFinal += _xVelocityAdditive;

        _xVelocityAdditive = Vector2.zero;
    }

    public void SwapWalkSpeeds()
    {
        (_walkLeftSpeed, _walkRightSpeed) = (_walkRightSpeed, _walkLeftSpeed);
    }

    public void SetVelocity(float velocity)
    {
        _xVelocityFinal = new Vector2(velocity, _xVelocityFinal.y);
    }

    public void AddVelocity(float velocity, float limit = 0f)
    {
        switch (_xVelocityAdditive.x + velocity)
        {
            case float i when limit == 0f
            || Mathf.Abs(i) < limit:
                _xVelocityAdditive = new Vector2(i, _xVelocityAdditive.y);
                break;
            case float i when i > 0f:
                _xVelocityAdditive = new Vector2(limit, _xVelocityAdditive.y);
                break;
            case float i when i < 0f:
                _xVelocityAdditive = new Vector2(-limit, _xVelocityAdditive.y);
                break;
            default:
                break;
        }
    }

    public Vector2 GetBaseVelocity()
    {
        return _xVelocityFinal;
    }

    public Vector2 GetFullVelocity()
    {
        return _xVelocityFinal + _xVelocityAdditive;
    }

    public Vector2 GetVelocityInput()
    {
        return _velocityInput;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _characterController = GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
        _actionController = GetComponent<ActionController>();
        _player = GetComponent<Player>();
    }
#endif
}