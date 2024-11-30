using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OrientationController))]

public class Player : MonoBehaviour
{
    [field: Header("Components")]
    [field: SerializeField] public CharacterController _characterController { get; protected set; }
    [field: SerializeField] public GravityController _gravityController { get; protected set; }
    [field: SerializeField] public Animator _animator { get; protected set; }
    [field: SerializeField] public OrientationController _orientationController { get; protected set; }
    [field: SerializeField] public bool baseOrientation { get; protected set; }
    [field: SerializeField] public Player opponent { get; protected set; }

    [field: SerializeField] public KeyCode LeftKey { get; protected set; }
    [field: SerializeField] public KeyCode RightKey { get; protected set; }
    [field: SerializeField] public KeyCode JumpKey { get; protected set; }
    [field: SerializeField] public KeyCode LightAttackKey { get; protected set; }
    [field: SerializeField] public KeyCode HeavyAttackKey { get; protected set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
        _animator = GetComponent<Animator>();
        _orientationController = GetComponent<OrientationController>();
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _characterController = GetComponent<CharacterController>();
        _gravityController = GetComponent<GravityController>();
        _animator = GetComponent<Animator>();
        _orientationController = GetComponent<OrientationController>();
    }
#endif
}