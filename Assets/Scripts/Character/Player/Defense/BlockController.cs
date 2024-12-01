using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private HorizontalMovement _horizontalMovement;
    [SerializeField] private OrientationController _orientation;

    void Start()
    {
        _horizontalMovement = GetComponent<HorizontalMovement>();
        _orientation = GetComponent<OrientationController>();
    }

    public bool CheckIfBlocking()
    {
        if (_orientation.getOrientation())
            return _horizontalMovement.GetVelocityInput().x > 0f;
        else
            return _horizontalMovement.GetVelocityInput().x < 0f;
    }
}
