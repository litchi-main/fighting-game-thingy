using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class MovementAnimation : MonoBehaviour, IMovementInput
{ 
    [Header("Params")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private Func<float, bool> running = (a) => a > 0f;
    private Func<float, bool> walkingBack = (a) => a < 0f;

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
        float temp = 0f;
        if (Input.GetKeyDown(_player.JumpKey))
            temp++;

        return temp;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("Running", running(LRMovement()));
        _animator.SetBool("Walking back", walkingBack(LRMovement()));
        _animator.SetBool("Grounded", _player._gravityController.IsGrounded);
    }

    public void SwapWalkAnimations()
    {
        (running, walkingBack) = (walkingBack, running);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }
#endif
}