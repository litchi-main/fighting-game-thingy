using UnityEngine;

public class GenericInputReader : MonoBehaviour 
{
    public bool[] getAttackInput<T>(T t) where T : BaseInputReader
    {
        return t.attackButtonInput();
    }

    public int[] getDirectionalInput<T>(T t) where T : BaseInputReader
    {
        return t.directionalInput();
    }
}
