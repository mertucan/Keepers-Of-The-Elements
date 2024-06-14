using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro bileşenini kullanmak için gerekli

public class HealthBar : MonoBehaviour
{
    public Damagable damagable;
    public Slider slider;
    public TextMeshProUGUI hpValueText; // HP Value metni için TextMeshPro bileşeni

    public float smoothingSpeed = 5f; // Geçişin yumuşaklığını kontrol eden bir değişken

    private void Start()
    {
        // Eğer Damagable scripti atanmadıysa, bu scriptin bulunduğu GameObject'te Damagable bileşenini arar
        if (damagable == null)
        {
            damagable = GetComponent<Damagable>();
        }

        // Slider atanmadıysa, bu scriptin bulunduğu GameObject'te Slider bileşenini arar
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        // Eğer hem Damagable hem de Slider bileşenleri doğru bir şekilde atandıysa, sağlık çubuğunu günceller
        if (damagable != null && slider != null)
        {
            UpdateHealthBar();
        }
        else
        {
            Debug.LogError("HealthBar: Damagable or Slider component is missing!");
        }
    }

    private void Update()
    {
        // Her güncellemede sağlık çubuğunu kontrol eder ve gerektiğinde günceller
        UpdateHealthBar();
    }

    // Sağlık çubuğunu güncelleyen fonksiyon
    private void UpdateHealthBar()
    {
        if (damagable != null && slider != null)
        {
            // Sağlık oranını hesaplar ve slider'ın değerini bu orana göre smooth bir şekilde ayarlar
            float healthRatio = damagable.Health / damagable.MaxHealth;
            float smoothedValue = Mathf.Lerp(slider.value, healthRatio, Time.deltaTime * smoothingSpeed);
            slider.value = smoothedValue;

            // HP Value metnini günceller
            if (hpValueText != null)
            {
                hpValueText.text = $"{damagable.Health}/{damagable.MaxHealth}";
            }
        }
    }
}
