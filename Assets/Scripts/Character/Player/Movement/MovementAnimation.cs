using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class MovementAnimation : MonoBehaviour
{ 
    [Header("Params")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private Func<float, bool> running = (a) => a > 0f;
    private Func<float, bool> walkingBack = (a) => a < 0f;

    private BaseInputReader _inputSource;
    private GenericInputReader _inputReader;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = _player.animator;
        _inputSource = _player.inputSource;
        _inputReader = _player.inputReader;
    }

    private void Update()
    {
        _animator.SetBool("Running", running(_inputReader.getDirectionalInput(_inputSource)[0]));
        _animator.SetBool("Walking back", walkingBack(_inputReader.getDirectionalInput(_inputSource)[0]));
        _animator.SetBool("Grounded", _player.gravityController.IsGrounded);
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