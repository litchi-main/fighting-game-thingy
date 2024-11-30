using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ActionController))]


public class AttackAnimation : MonoBehaviour, IAttackInput
{
    [Header("Params")]
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private ActionController _actionController;

    public bool LightAttackCheck()
    {
        return Input.GetKeyDown(_player.LightAttackKey);
    }
    
    public bool HeavyAttackCheck()
    {
        return Input.GetKeyDown(_player.HeavyAttackKey);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _actionController = GetComponent<ActionController>();
    }

    private void Update()
    {
        if (LightAttackCheck())
            _animator.SetTrigger("n.L");
        else if (HeavyAttackCheck())
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