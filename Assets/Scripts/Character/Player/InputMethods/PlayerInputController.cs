using UnityEngine;

public class PlayerInputController : BaseInputReader
{
    [Header("Params")]
    [SerializeField] private KeyCode[] keys = new KeyCode[5];

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

    public void Awake()
    {
        attacks[0] = false;
        attacks[1] = false;
        directions[0] = 0;
        directions[1] = 0;
    }

    public void Update()
    {
        attacks[0] = Input.GetKeyDown(keys[3]);
        attacks[1] = Input.GetKeyDown(keys[4]);
        directions[0] = (Input.GetKey(keys[0]) ? -1 : 0) + (Input.GetKey(keys[1]) ? 1 : 0);
        directions[1] = Input.GetKeyDown(keys[2]) ? 1 : 0;
    }

    public void ButtonConfig(params KeyCode[] newKeys)
    {
        int i = 0;
        foreach (var key in newKeys)
        {
            keys[i] = key;
            i++;
        }
    }

    public KeyCode[] GetButtonConfig()
    {
        return keys;
    }
}
