using UnityEngine;

public class BlockController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private HorizontalMovement _horizontalMovement;
    [SerializeField] private OrientationController _orientation;
    [SerializeField] private Player _player;

    private BaseInputReader _inputSource;
    private GenericInputReader _inputReader;

    void Start()
    {
        _player = gameObject.GetComponent<Player>();
        _horizontalMovement = _player.horizontalMovementController;
        _orientation = _player.orientationController;
        _inputSource = _player.inputSource;
        _inputReader = _player.inputReader;
    }

    public bool CheckIfBlocking()
    {
        if (_orientation.getOrientation())
            return _inputReader.getDirectionalInput(_inputSource)[0] > 0f;
        else
            return _inputReader.getDirectionalInput(_inputSource)[0] < 0f;
    }
}
