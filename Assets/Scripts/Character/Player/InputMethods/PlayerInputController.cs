using UnityEngine;

public class PlayerInputController : BaseInputReader, IInputReader
{
    [Header("Params")]
    [SerializeField] private KeyCode _lightAttack;
    [SerializeField] private KeyCode _heavyAttack;
    [SerializeField] private KeyCode _leftDirection;
    [SerializeField] private KeyCode _rightDirection;
    [SerializeField] private KeyCode _upDirection;

    private bool[] attacks = new bool[2];
    private int[] directions = new int[2];

    public override bool[] attackButtonInput()
    {
        return attacks;
    }

    public override int[] directionalInput()
    {
        return directions;
    }

    public void Update()
    {
        attacks[0] = Input.GetKeyDown(_lightAttack);
        attacks[1] = Input.GetKeyDown(_heavyAttack);
        directions[0] = (Input.GetKey(_leftDirection) ? -1 : 0) + (Input.GetKey(_rightDirection) ? 1 : 0);
        directions[1] = Input.GetKeyDown(_upDirection) ? 1 : 0;
    }
}
