using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class CollisionWithPlayers : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private CharacterController _charController;
    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _charController = GetComponentInParent<CharacterController>();
        _collider.size = new(_charController.radius * 2 + 0.5f, _collider.size.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.CompareTag("P1")
            || collision.gameObject.CompareTag("P2"))
            && collision.gameObject.layer == LayerMask.NameToLayer("CollisionBoxes"))
        {
            Vector2 collisionPoint = collision.transform.position;
            Vector2 colliderTop = (Vector2)_collider.transform.position + _collider.size / 2f;
            if (collisionPoint.y <= colliderTop.y)
                PushPlayerOut(collision);
            if (_charController.collisionFlags.HasFlag(CollisionFlags.Sides))
                PushPlayerOut(collision);
        }
    }

    private void MoveObject(float pushForward, float pushBackward, CharacterController otherObject)
    {
        CollisionFlags flags = otherObject.Move(new(pushForward, 0));
        if (flags.HasFlag(CollisionFlags.Sides))
        {
            gameObject.GetComponentInParent<CharacterController>().Move(new(pushForward, 0));
            otherObject.Move(new(pushBackward, 0));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("P1")
            || collision.gameObject.CompareTag("P2"))
            && collision.gameObject.layer == LayerMask.NameToLayer("CollisionBoxes"))
        {
            float myPos = gameObject.transform.position.x;
            float otherPos = collision.transform.position.x;
            HorizontalMovement myMovement = _collider.GetComponentInParent<HorizontalMovement>();
            float myVelocity = myMovement.GetBaseVelocity().x;

            if ((myPos < otherPos && myVelocity > 0)
                || (myPos > otherPos && myVelocity < 0))
            {
                HorizontalMovement otherMovement = collision.GetComponentInParent<HorizontalMovement>();
                otherMovement.AddVelocity(myVelocity);
            }
            if (_charController.collisionFlags.HasFlag(CollisionFlags.Sides))
                PushPlayerOut(collision);
        }
    }

    private void PushPlayerOut(Collider2D collision)
    {

        Vector2 collisionPoint = collision.transform.position;
        Vector2 colliderTop = (Vector2)_collider.transform.position + _collider.size / 2f;
        Vector2 colliderBottom = (Vector2)_collider.transform.position - _collider.size / 2f;

        CharacterController otherObject = collision.gameObject.GetComponentInParent<CharacterController>();
        BoxCollider2D otherCollider = collision.gameObject.GetComponent<BoxCollider2D>();

        float pushLeft = -(collisionPoint.x - colliderBottom.x + otherCollider.size.x / 2f);
        float pushRight = colliderTop.x - collisionPoint.x + otherCollider.size.x / 2f;
        if (collisionPoint.x < _collider.transform.position.x)
            MoveObject(pushLeft, pushRight, otherObject);
        else
            MoveObject(pushRight, pushLeft, otherObject);
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(TryGetComponents))]
    protected void TryGetComponents()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
#endif
}
