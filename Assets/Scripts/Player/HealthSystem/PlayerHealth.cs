using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthBarForeground;

    private void Awake()
    {
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, maxHealth);
        Debug.Log($"Player took {amount} damage, Remaining: {_currentHealth}");

        UpdateHealthBar();

        if( _currentHealth <= 0f)
        {
            Die();
        }
    }


    private void UpdateHealthBar()
    {
        if (healthBarForeground != null)
        {
            healthBarForeground.fillAmount = _currentHealth / maxHealth;
        }
    }
    private void Die()
    {
        Debug.Log("Player died.");
    }

}
