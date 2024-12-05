using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private Color _healthBarColor;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _healthBar;

    [SerializeField] private bool _leftOrRight;
    private Vector2 _relativePosToCamera;
    private Vector2 _relativePosToDamage;

    [SerializeField] private PixelToUnitConverter _pixelToUnitConverter;

    private void Start()
    {
        _player = GetComponent<Player>();
        _camera = _player.mainCamera;
        _health = _player.healthPoints;
        _pixelToUnitConverter.SetCamera(_camera.gameObject);
        _health.addHealthChangedEvent(OnHit);

        _relativePosToCamera = _leftOrRight
            ? new(Screen.width / _pixelToUnitConverter.WorldToPixelAmount.x / 4f, Screen.height / _pixelToUnitConverter.WorldToPixelAmount.y / 2f - 1f)
            : new(-Screen.width / _pixelToUnitConverter.WorldToPixelAmount.x / 4f, Screen.height / _pixelToUnitConverter.WorldToPixelAmount.y / 2f - 1f);
        _relativePosToDamage = new(0, 0);

        _healthBar = Instantiate(_healthBar);
        _healthBar.transform.position = new(_camera.transform.position.x, _camera.transform.position.y);
        _healthBar.transform.localPosition = _relativePosToCamera;
        _healthBar.GetComponentInChildren<SpriteRenderer>().color = _healthBarColor;
    }

    private void LateUpdate()
    {
        _healthBar.transform.position = new Vector2(_camera.transform.position.x, _camera.transform.position.y) + _relativePosToCamera + _relativePosToDamage;
    }

    private void OnHit(float prevHealth, float amount)
    {
        Vector3 scale = _healthBar.transform.localScale;
        scale.x *= (prevHealth - amount) / prevHealth;
        _relativePosToDamage += _leftOrRight
            ? new(-(_healthBar.transform.localScale.x - scale.x) / 2f, 0)
            : new((_healthBar.transform.localScale.x - scale.x) / 2f, 0);
        _healthBar.transform.localScale = scale;
    }

    public void setLeftOrRight(bool lor)
    {
        _leftOrRight = lor;
    }
}
