using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ActionController))]


public class AttackAnimation : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private ActionController _actionController;

    private BaseInputReader _inputSource;
    private GenericInputReader _inputReader;

    private void Start()
    {
        _player = GetComponent<Player>();
        _animator = _player.animator;
        _actionController = _player.actionController;
        _inputSource = _player.inputSource;
        _inputReader = _player.inputReader;
}

    private void Update()
    {
        if (_inputReader.getAttackInput(_inputSource)[0])
            _animator.SetTrigger("n.L");
        else if (_inputReader.getAttackInput(_inputSource)[1])
            _animator.SetTrigger("n.H");
        _animator.SetBool("In Neutral", _actionController.IsInNeutral());
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger("Got hit");
    }

    public void PlayBlockAnimation()
    {
        _animator.SetTrigger("Blocking");
    }

    public void ForceIntoNeutral()
    {
        _animator.SetBool("In Neutral", true);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _player = GetComponent<Player>();
        _actionController = GetComponent<ActionController>();
        _animator = GetComponent<Animator>();
    }
#endif
}