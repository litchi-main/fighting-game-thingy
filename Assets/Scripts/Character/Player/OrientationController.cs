using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(MovementAnimation))]
[RequireComponent(typeof(HorizontalMovement))]

public class OrientationController : MonoBehaviour
{
    //Orientation is false when facing right (left-side player) and is true when facing left (right-side player)

    [Header("Params")]
    [SerializeField] private Player _player;
    [SerializeField] private MovementAnimation _animationController;
    [SerializeField] private HorizontalMovement _movementController;
    
    private bool Orientation;
    private bool _prevOrientation;

    public OrientationController ()
    {
        _prevOrientation = false;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animationController = GetComponent<MovementAnimation>();
        _movementController = GetComponent<HorizontalMovement>();
        Orientation = _player.baseOrientation;
    }

    public void Update()
    {
        if (_prevOrientation != Orientation)
        {
            _player.transform.localScale = new Vector3(_player.transform.localScale.x * -1,1,1);
            _animationController.SwapWalkAnimations();
            _movementController.SwapWalkSpeeds();
        }
        _prevOrientation = Orientation;
    }

    public void SwapOrientation ()
    {
        Orientation = Orientation ? false : true;
    }

    public bool getOrientation()
    {
        return Orientation;
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _player = GetComponent<Player>();
        _animationController = GetComponent<MovementAnimation>();
        _movementController = GetComponent<HorizontalMovement>();
    }
#endif
}