using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Hurtbox : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void GetHit(int hitStun, int blockStun, float damage)
    {
        gameObject.GetComponentInParent<ActionController>().GetHit(hitStun, blockStun, damage);
    }

    public void TurnHurtboxOff()
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