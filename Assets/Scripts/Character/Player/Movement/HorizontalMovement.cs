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

    private Vector2 _XVelocity { get; set; }
    private Vector2 _VelocityInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
        _actionController = GetComponent<ActionController>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _VelocityInput = Vector2.zero;
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
        if (_gravityController.IsGrounded)
            if (_actionController.IsInNeutral())
            {
                _VelocityInput.Set(LRMovement(), UDMovement());
                _XVelocity = _VelocityInput * (_VelocityInput.x > 0 ? _walkRightSpeed : _walkLeftSpeed) * Time.deltaTime;
            }
            else
                _XVelocity = Vector2.zero;

        CollisionFlags collisionFlags = _characterController.Move(_XVelocity);


        if (collisionFlags.HasFlag(CollisionFlags.Sides))
            _XVelocity = Vector2.zero;
    }

    public void SwapWalkSpeeds()
    {
        (_walkLeftSpeed, _walkRightSpeed) = (_walkRightSpeed, _walkLeftSpeed);
    }

    public void SetVelocity(float velocity)
    {
        _XVelocity = new Vector2(velocity, _XVelocity.y);
    }

    public void AddVelocity(float velocity, float limit = 0f)
    {
        switch (_XVelocity.x + velocity)
        {
            case float i when limit == 0f
            || Mathf.Abs(i) < limit:
                _XVelocity = new Vector2(i, _XVelocity.y);
                break;
            case float i when i > 0f:
                _XVelocity = new Vector2(limit, _XVelocity.y);
                break;
            case float i when i < 0f:
                _XVelocity = new Vector2(-limit, _XVelocity.y);
                break;
            default:
                break;
        }
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