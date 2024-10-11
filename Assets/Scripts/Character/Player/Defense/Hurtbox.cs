using System.Collections;
using System.Collections.Generic;
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

    public void GetHit(int hitStun, int blockStun)
    {
        gameObject.GetComponentInParent<ActionController>().GetHit(hitStun, blockStun);
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