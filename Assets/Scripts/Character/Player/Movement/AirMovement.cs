using UnityEngine;

[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(HorizontalMovement))]
[RequireComponent(typeof(ActionController))]

public class AirMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] public float jumpForce = 14f;
    [SerializeField] public int maxAirJumps = 2;
    [SerializeField] private float _jumpHorizontalVelocity = 4f;

    [SerializeField] public GravityController _gravityController;
    [SerializeField] public HorizontalMovement _horizontalMovementController;
    [SerializeField] public ActionController _actionController;
    [SerializeField] public Player _player;

    private BaseInputReader _inputSource;
    private GenericInputReader _inputReader;

    private Vector2 _Velocity;
    private int _jumpsLeft;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _gravityController = _player.gravityController;
        _horizontalMovementController = _player.horizontalMovementController;
        _actionController = _player.actionController;
        _inputSource = _player.inputSource;
        _inputReader = _player.inputReader;
    }

    private void Start()
    {
        RefreshJumps();
    }

    private void Update()
    {
        if ((_gravityController.IsGrounded
            || _jumpsLeft > 0)
            && _actionController.IsInNeutral())
        {
            if (_inputReader.getDirectionalInput(_inputSource)[1] > 0f)
            {
                int jumpStrength = maxAirJumps - _jumpsLeft;
                _Velocity.Set(_inputReader.getDirectionalInput(_inputSource)[0] * (_jumpHorizontalVelocity - jumpStrength), 0);
                _gravityController.SetVelocity(jumpForce);
                if (_gravityController.IsGrounded)
                    RefreshJumps();
                else
                    _jumpsLeft--;
            }
        }

        if (_gravityController.IsGrounded)
            RefreshJumps();
        else
            _horizontalMovementController.AddVelocity(_Velocity.x);
    }

    private void RefreshJumps()
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