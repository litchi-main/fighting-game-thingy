using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(OrientationController))]

public class Player : MonoBehaviour, ICloneable
{
    [field: Header("Components")]
    [field: SerializeField] public CharacterController characterController { get; protected set; }
    [field: SerializeField] public GravityController gravityController { get; protected set; }
    [field: SerializeField] public HorizontalMovement horizontalMovementController { get; protected set; }
    [field: SerializeField] public AirMovement airMovementController { get; protected set; }
    [field: SerializeField] public ActionController actionController { get; protected set; }
    [field: SerializeField] public Animator animator { get; protected set; }
    [field: SerializeField] public AttackAnimation attackAnimator { get; protected set; }
    [field: SerializeField] public MovementAnimation movementAnimator { get; protected set; }
    [field: SerializeField] public OrientationController orientationController { get; protected set; }
    [field: SerializeField] public bool baseOrientation { get; set; }
    [field: SerializeField] public Player opponent { get;  set; }
    [field: SerializeField] public GenericInputReader inputReader { get; protected set; }
    [field: SerializeField] public BaseInputReader inputSource { get;  set; }
    [field: SerializeField] public Health healthPoints { get; protected set; }
    [field: SerializeField] public Camera mainCamera { get;  set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        gravityController = GetComponent<GravityController>();
        horizontalMovementController = GetComponent<HorizontalMovement>();
        airMovementController = GetComponent<AirMovement>();
        actionController = GetComponent<ActionController>();
        animator = GetComponent<Animator>();
        attackAnimator = GetComponent<AttackAnimation>();
        movementAnimator = GetComponent<MovementAnimation>();
        orientationController = GetComponent<OrientationController>();
        inputReader = GetComponent<GenericInputReader>();
        healthPoints = GetComponent<Health>();
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        characterController = GetComponent<CharacterController>();
        gravityController = GetComponent<GravityController>();
        animator = GetComponent<Animator>();
        orientationController = GetComponent<OrientationController>();
    }

    public object Clone()
    {
        return Instantiate(gameObject);
    }
#endif
}