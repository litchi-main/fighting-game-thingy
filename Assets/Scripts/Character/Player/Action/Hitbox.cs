using System.Threading;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Hitbox : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private BoxCollider2D _collider;

    public int _activeFrames;
    public int _hitStun;
    public int _blockStun;
    public float _damage;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _activeFrames = _activeFrames == default ? 10 : _activeFrames;
    }

    private void Update()
    {
        _activeFrames--;
        if (_activeFrames < 0)
            TurnHitboxOff();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.CompareTag(other.gameObject.tag))
        {
            HitObject(other);
        }
    }

    private void HitObject(Collider2D other)
    {
        try
        {
            other.GetComponent<Hurtbox>().GetHit(_hitStun, _blockStun, _damage);
        }
        catch
        {

        }
        //TurnHitboxOff();
    }

    public void TurnHitboxOff()
    {
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
#endif
}