using UnityEngine;

public class ReturnPlayerToZero : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Player _player;
    void Start()
    {
        _player = GetComponent<Player>();
    }

    private void LateUpdate()
    {
        if (_player.transform.position.y <= -10f)
        {
            if (_player.transform.position.x > 0f)
                _player.characterController.Move(new(-5f, 10f));
            else
                _player.characterController.Move(new(5f, 10f));
            _player.gravityController.SetVelocity(0f);
            _player.gravityController.IsGrounded = true;
        }
    }
}
