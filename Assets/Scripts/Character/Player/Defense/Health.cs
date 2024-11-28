using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private float _baseHealth;

    public delegate void HealthCallback(float prevHealth, float diff);
    private event HealthCallback healthChanged;
    private event EventHandler healthEnded;
    private float currentHealth;
    void Start()
    {
        currentHealth = _baseHealth;
    }

    void showHp()
    {
        Debug.Log(gameObject.tag + " " + currentHealth);
    }

    public void hit(float amount)
    {
        healthChanged?.Invoke(currentHealth, amount);
        currentHealth -= amount;

        if (currentHealth <= 0)
            healthEnded?.Invoke(this, EventArgs.Empty);
    }

    public void addHealthChangedEvent(HealthCallback function)
    {
        healthChanged += function;
    }
}
