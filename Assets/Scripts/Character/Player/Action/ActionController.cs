using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(AttackAnimation))]

public class ActionController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _baseHitbox;
    [SerializeField] private AttackAnimation _animationController;
    [SerializeField] private Health _health;
    [SerializeField] private BlockController _blockController;

    private BaseInputReader _inputSource;
    private GenericInputReader _inputReader;

    private Dictionary<string, Attack> _attackList = new Dictionary<string, Attack>();
    private int _stun;
    private State _playerState;
    private bool _attackStartedInTheAir;

    private int _attackActiveAfter;
    private delegate void TurnOnHitboxFunc(Action<List<float>, float[]> Generate);
    private TurnOnHitboxFunc _TurnOnHitbox;

    private void Awake()
    {
        using StreamReader reader = new("Assets/Resources/AttackData.json");
        var json = reader.ReadToEnd();
        List<Attack> attacks = new List<Attack>();
        attacks = JsonConvert.DeserializeObject<List<Attack>>(json);
        reader.Close();

        foreach (var attack in attacks)
        {
            _attackList[attack.Name] = attack;
        }

        _player = GetComponent<Player>();
        _animationController = _player.attackAnimator;
        _health = _player.healthPoints;
        _inputSource = _player.inputSource;
        _inputReader = _player.inputReader;
    }

    public void Start()
    {
        _playerState = State.Neutral;
        _stun = 0;
        _attackStartedInTheAir = false;
        _attackActiveAfter = -1;
        _TurnOnHitbox = null;
    }

    private void Update()
    {
        if (_stun > 0)
        {
            _stun--;

            if (_attackActiveAfter > 0)
                _attackActiveAfter--;

            if (_attackActiveAfter == 0)
            {
                _TurnOnHitbox(GenerateHitbox);
                _TurnOnHitbox = null;
                _attackActiveAfter--;
            }


            if (_attackStartedInTheAir
                && _player.gravityController.IsGrounded
                && _playerState == State.Attacking)
            {
                _stun = 0;
            }
        }
        else
        {
            _playerState = State.Neutral;
            _attackStartedInTheAir = !_player.gravityController.IsGrounded;
            switch (_inputReader.getAttackInput(_inputSource)[0])
            {
                case bool i when i == true:
                    _playerState = State.Attacking;
                    switch (!_attackStartedInTheAir)
                    {
                        case bool j when j == true:
                            DoAttack(_attackList["n.L"]);
                            break;
                        case bool j when j == false:
                            DoAttack(_attackList["j.L"]);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (_inputReader.getAttackInput(_inputSource)[1])
            {
                case bool i when i == true:
                    _playerState = State.Attacking;
                    switch (!_attackStartedInTheAir)
                    {
                        case bool j when j == true:
                            DoAttack(_attackList["n.H"]);
                            break;
                        case bool j when j == false:
                            DoAttack(_attackList["j.H"]);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

    }

    private void DoAttack(Attack attack)
    {
        _stun = GetStunTotalDuration(attack.Name);
        _attackActiveAfter = attack.startup;
        _TurnOnHitbox = attack.TurnOnHitbox;
    }

    private void GenerateHitbox(List<float> dimensions, params float[] attackFrames)
    {
        GameObject hitbox = Instantiate(_baseHitbox, _player.transform.position, _player.transform.rotation);
        hitbox.GetComponent<BoxCollider2D>().offset = new Vector2(dimensions[0], dimensions[1]);
        hitbox.GetComponent<BoxCollider2D>().size = new Vector2(dimensions[2], dimensions[3]);
        SetHitboxData(out hitbox.GetComponent<Hitbox>()._activeFrames,
            out hitbox.GetComponent<Hitbox>()._hitStun,
            out hitbox.GetComponent<Hitbox>()._blockStun,
            out hitbox.GetComponent<Hitbox>()._damage,
            attackFrames);
        hitbox.tag = gameObject.tag;
        hitbox.transform.localScale = new Vector2(_player.orientationController.getOrientation() ? -1 : 1, 1);
    }

    private void SetHitboxData(out int activeFrames, out int hitStun, out int blockStun, out float damage, params float[] attackData)
    {
        activeFrames = (int)attackData[0];
        hitStun = (int)attackData[1];
        blockStun = (int)attackData[2];
        damage = attackData[3];
    }

    private int GetStunTotalDuration(string attackName)
    {
        return _attackList[attackName].startup + _attackList[attackName].active + _attackList[attackName].recovery;
    }

    public bool IsInNeutral()
    {
        return _playerState == State.Neutral;
    }

    public void GetHit(int hitStun, int blockStun, float damage)
    {
        if ((_playerState == State.Neutral
            || _playerState == State.Blocking)
            && _blockController.CheckIfBlocking())
        {
            _stun = blockStun;
            _playerState = State.Blocking;
            _animationController.PlayBlockAnimation();
            _attackActiveAfter = -1;
            _health.hit(damage / 8f);
        }
        else
        {
            _stun = hitStun;
            _playerState = State.Hit;
            _animationController.PlayHitAnimation();
            _attackActiveAfter = -1;
            _health.hit(damage);
        }
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _player = GetComponent<Player>();
        _animationController = GetComponent<AttackAnimation>();
    }
#endif
}

enum State
{
    Neutral,
    Attacking,
    Hit,
    Blocking
}