using UnityEngine;

[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(ActionController))]

public class Jump : MonoBehaviour, IMovementInput
{
    [Header("Params")]
    [SerializeField] public float jumpForce = 9f;
    [SerializeField] public int maxAirJumps = 2;
    [SerializeField] private float _jumpHorizontalVelocity = 3f;

    [SerializeField] public GravityController _gravityController;
    [SerializeField] public HorizontalMovement _horizontalMovementController;
    [SerializeField] public ActionController _actionController;
    [SerializeField] public Player _player;

    private Vector2 _YVelocity;
    private int _jumpsLeft;

    private void Awake()
    {
        _gravityController = GetComponent<GravityController>();
        _horizontalMovementController = GetComponent<HorizontalMovement>();
        _actionController = GetComponent<ActionController>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        RefreshJumps();
    }
    public float UDMovement()
    {
        float temp = 0f;
        if (Input.GetKeyDown(_player.JumpKey))
            temp++;

        return temp;
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

    private void Update()
    {
        float input = UDMovement();

        if ((_gravityController.IsGrounded
            || _jumpsLeft > 0)
            && _actionController.IsInNeutral())
        {
            if (input == 1f)
            {
                _horizontalMovementController.AddVelocity(LRMovement() * (_jumpHorizontalVelocity - maxAirJumps + _jumpsLeft) * Time.deltaTime, 
                    limit: _jumpHorizontalVelocity * Time.deltaTime);
                _gravityController.SetVelocity(jumpForce);
                if (_gravityController.IsGrounded)
                    RefreshJumps();
                else
                    _jumpsLeft--;
            }
        }

        if (_gravityController.IsGrounded)
            RefreshJumps();
    }
    
    private void RefreshJumps ()
    {
        _jumpsLeft = maxAirJumps;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _horizontalMovementController = GetComponent<HorizontalMovement>();
        _gravityController = GetComponent<GravityController>();
        _actionController = GetComponent<ActionController>();
        _player = GetComponent<Player>();
    }
#endif
}