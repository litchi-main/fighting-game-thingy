using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private Color _healthBarColor;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _healthBar;


    private void Start()
    {
        _health = GetComponent<Health>();
        _health.addHealthChangedEvent(OnHit);
        _healthBar = Instantiate(_healthBar);
        _healthBar.transform.position = Vector3.zero;
        _healthBar.transform.localPosition = _healthBar.transform.localScale / 2f;
        _healthBar.GetComponentInChildren<SpriteRenderer>().color = _healthBarColor;
    }

    private void OnHit(float prevHealth, float amount)
    {
        Vector3 scale = _healthBar.transform.localScale;
        scale.x *= (prevHealth - amount) / prevHealth;
        _healthBar.transform.localScale = scale;
        _healthBar.transform.localPosition = _healthBar.transform.localScale / 2f;
    }
}
