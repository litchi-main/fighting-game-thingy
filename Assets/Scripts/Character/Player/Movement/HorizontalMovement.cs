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

    private Vector2 _XVelocityFinal { get; set; }
    private Vector2 _XVelocityAdditive { get; set; }
    private Vector2 _VelocityInput;
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
        _characterController.Move(_XVelocityFinal * Time.deltaTime);

        _XVelocityFinal = Vector2.zero;

        if (_gravityController.IsGrounded)
            if (_actionController.IsInNeutral())
            {
                _VelocityInput.Set(LRMovement(), UDMovement());
                _XVelocityFinal = (_VelocityInput.x > 0 ? _walkRightSpeed : _walkLeftSpeed) * _VelocityInput;
            }

        _XVelocityFinal += _XVelocityAdditive;

        _XVelocityAdditive = Vector2.zero;
    }

    public void SwapWalkSpeeds()
    {
        (_walkLeftSpeed, _walkRightSpeed) = (_walkRightSpeed, _walkLeftSpeed);
    }

    public void SetVelocity(float velocity)
    {
        _XVelocityFinal = new Vector2(velocity, _XVelocityFinal.y);
    }

    public void AddVelocity(float velocity, float limit = 0f)
    {
        switch (_XVelocityAdditive.x + velocity)
        {
            case float i when limit == 0f
            || Mathf.Abs(i) < limit:
                _XVelocityAdditive = new Vector2(i, _XVelocityAdditive.y);
                break;
            case float i when i > 0f:
                _XVelocityAdditive = new Vector2(limit, _XVelocityAdditive.y);
                break;
            case float i when i < 0f:
                _XVelocityAdditive = new Vector2(-limit, _XVelocityAdditive.y);
                break;
            default:
                break;
        }
    }

    public Vector2 GetBaseVelocity()
    {
        return _XVelocityFinal;
    }

    public Vector2 GetFullVelocity()
    {
        return _XVelocityFinal + _XVelocityAdditive;
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