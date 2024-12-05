using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private float _baseHealth;

    public delegate void HealthCallback(float prevHealth, float diff);
    private event HealthCallback healthChanged;
    public delegate void GameEndCallback();
    private event GameEndCallback healthEnded;
    private float currentHealth;
    void Start()
    {
        currentHealth = _baseHealth;
    }

    public void hit(float amount)
    {
        healthChanged?.Invoke(currentHealth, amount);
        currentHealth -= amount;

        if (currentHealth <= 0)
            healthEnded?.Invoke();
    }

    public void addHealthChangedEvent(HealthCallback function)
    {
        healthChanged += function;
    }

    public void addHealthEndedEcent(GameEndCallback function)
    {
        healthEnded += function;
    }
}
