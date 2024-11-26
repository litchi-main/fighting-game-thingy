using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("params")]
    [SerializeField] private float _baseHealth;

    private event EventHandler healthChanged;
    private event EventHandler healthEnded;
    private float currentHealth;
    void Start()
    {
        currentHealth = _baseHealth;
    }

    public void hit(float amount)
    {
        currentHealth -= amount;
        healthChanged?.Invoke(this, EventArgs.Empty);

        if (currentHealth <= 0)
            healthEnded?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {

    }
}
