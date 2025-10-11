using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] public Health bossHealth;   // référence au script Health du boss
    [SerializeField] private Slider healthSlider; // Slider UI
    [SerializeField] private Image fillImage;     // Image de remplissage du Slider (assigner le Fill du Slider)
    private float smoothSpeed = 5f;
    private float displayedHealth = 1f; // Valeur affichée (pour le lissage)

    void Start()
    {
        if (healthSlider == null || bossHealth == null) return;
        healthSlider.minValue = 0f;
        healthSlider.maxValue = 1f;

        // Attendre que le boss soit prêt avant de forcer la synchro
        float initialHealth = bossHealth.getNormalizedHealth();
        displayedHealth = Mathf.Clamp01(initialHealth);
        // healthSlider.value = displayedHealth;
        UpdateFillColor(initialHealth);
        bossHealth.healthChange += HandleHealthChanged;
    }

    void Update()
    {
       healthSlider.value = Mathf.Lerp(healthSlider.value,displayedHealth,Time.deltaTime * smoothSpeed);
    }

    private void HandleHealthChanged(float normalizedHealth){
        displayedHealth = Mathf.Clamp01(normalizedHealth);
        UpdateFillColor(normalizedHealth);
    }

    private void UpdateFillColor(float normalizedHealth)
    {
        if (fillImage != null)
        {
            fillImage.color = Color.Lerp(Color.red, Color.green, normalizedHealth);
        }
    }

    public void Initialize(Health health)
    {
        if (health == null) return;

        bossHealth = health;

        // Met à jour la barre immédiatement
        float normalized = Mathf.Clamp01(bossHealth.getNormalizedHealth());
        displayedHealth = normalized;
        if (healthSlider != null) healthSlider.value = normalized;
        UpdateFillColor(normalized);

        // Affiche la barre
        gameObject.SetActive(true);
    }
    
    public void UpdateBarInstant()
    {
        if (bossHealth != null && healthSlider != null)
        {
            float normalized = Mathf.Clamp01(bossHealth.getNormalizedHealth());
            healthSlider.value = normalized;
            UpdateFillColor(normalized);
        }
    }
}
