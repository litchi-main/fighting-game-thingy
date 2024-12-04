using UnityEngine;

public class CollisionWithCameraBounds : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Camera _camera;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private PixelToUnitConverter _pixelToUnitConverter;

    private float leftSideBound;
    private float rightSideBound;

    private void Start()
    {
        _camera = GetComponentInParent<Player>().mainCamera;
    }

    void Update()
    {
        leftSideBound = _camera.transform.position.x - Screen.width / _pixelToUnitConverter.WorldToPixelAmount.x / 2f;
        rightSideBound = _camera.transform.position.x + Screen.width / _pixelToUnitConverter.WorldToPixelAmount.x / 2f;
        Vector2 leftBound = new(leftSideBound, _collider.transform.position.y);
        Vector2 rightBound = new(rightSideBound, _collider.transform.position.y);
        if (_collider.OverlapPoint(leftBound))
            moveDistanceNeeded(leftBound, "left");
        if (_collider.OverlapPoint(rightBound))
            moveDistanceNeeded(rightBound, "right");
    }

    private void moveDistanceNeeded(Vector2 bound, string side)
    {
        Vector2 distanceBetweenBoundAndCenter;
        if (side == "left")
            distanceBetweenBoundAndCenter = (Vector2)_collider.transform.position - bound;
        else
            distanceBetweenBoundAndCenter = bound - (Vector2)_collider.transform.position;
        Vector2 moveDist = _collider.size / 2f - distanceBetweenBoundAndCenter;
        moveDist *= new Vector2(1f, 0f);
        if (side == "left")
            gameObject.GetComponentInParent<CharacterController>().Move(moveDist);
        else
            gameObject.GetComponentInParent<CharacterController>().Move(-moveDist);
    }
}
