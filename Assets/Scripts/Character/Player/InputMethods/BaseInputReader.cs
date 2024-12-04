using UnityEngine;

public abstract class BaseInputReader : MonoBehaviour, IInputReader
{
    public abstract bool[] attackButtonInput();
    public abstract int[] directionalInput();
}
