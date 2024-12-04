using UnityEngine;
using static System.Math;

public class CPUInputController : BaseInputReader
{
    [Header("params")]
    [SerializeField] private Player _player;

    private bool[] attacks = new bool[2];
    private int[] directions = new int[2];
    private float effectiveRange = 3.3f;

    public override bool[] attackButtonInput()
    {
        return attacks;
    }

    public override int[] directionalInput()
    {
        return directions;
    }

    public void Start()
    {
        attacks[0] = false;
        attacks[1] = false;
        directions[0] = 0;
        directions[1] = 0;
        _player = GetComponent<Player>();
    }

    public void Update()
    {
        directions[0] = _player.orientationController.getOrientation() ? -1 : 1;
        if (Abs(_player.opponent.transform.position.x - _player.transform.position.x) <= effectiveRange)
            attacks[1] = true;
        else
            attacks[1] = false;
    }
}
