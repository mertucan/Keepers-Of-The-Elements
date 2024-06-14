using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarReverse : MonoBehaviour
{
    private Slider healthSlider;
    private Damagable damagable;
    private Transform enemyTransform;

    private void Awake()
    {
        // Düşmanın üzerindeki Slider bileşenini al
        healthSlider = GetComponentInChildren<Slider>();

        // Düşmanın Damagable bileşenini al
        damagable = GetComponent<Damagable>();

        // Enemy gameObject'ini al
        enemyTransform = GetComponent<Transform>();

        // Eğer Slider veya Damagable bileşeni yoksa hata ver
        if (healthSlider == null)
        {
            Debug.LogError("Slider component not found!");
            return;
        }

        if (damagable == null)
        {
            Debug.LogError("Damagable component not found!");
            return;
        }

        if (enemyTransform == null)
        {
            Debug.LogError("Enemy Transform not found!");
            return;
        }
    }

    private void Update()
    {
        // Sağlık çubuğunun değerini güncelle
        UpdateHealthBarValue();

        // Sağlık çubuğunun yönünü güncelle
        UpdateHealthBarDirection();
    }

    // Sağlık çubuğunun değerini güncelleyen fonksiyon
    public void UpdateHealthBarValue()
    {
        // Güncel sağlık değerini al
        float currentHealth = damagable.Health;
        // Maksimum sağlık değerini al
        float maxHealth = damagable.MaxHealth;
        
        // Sağlık çubuğunun değerini, güncel sağlık ve maksimum sağlık arasındaki orana göre ayarla
        healthSlider.value = currentHealth / maxHealth;
    }

    // Sağlık çubuğunun yönünü güncelleyen fonksiyon
    public void UpdateHealthBarDirection()
    {
        // Enemy gameObject'inin localScale değerini kontrol et
        if (enemyTransform.localScale.x < 0)
        {
            // Sağlık çubuğunun yönünü sola doğru ayarla
            healthSlider.direction = Slider.Direction.RightToLeft;
        }
        else if (enemyTransform.localScale.x > 0)
        {
            // Sağlık çubuğunun yönünü sağa doğru ayarla
            healthSlider.direction = Slider.Direction.LeftToRight;
        }
    }
}
