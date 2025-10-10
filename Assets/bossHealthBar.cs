using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Health bossHealth;   // référence au script Health du boss
    public Slider healthSlider; // Slider UI
    public Image fillImage;     // Image de remplissage du Slider (assigner le Fill du Slider)

    public Color fullHealthColor = Color.green; // couleur quand HP plein
    public Color zeroHealthColor = Color.red;   // couleur quand HP à 0

    void Update()
    {
        if (bossHealth != null && healthSlider != null && fillImage != null)
        {
            float healthPercent = bossHealth.GetCurrentHealth() / bossHealth.maxHealth;
            
            // Met à jour la valeur du Slider
            healthSlider.value = healthPercent;

            // Interpole la couleur du vert (plein) au rouge (vide)
            fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercent);
        }
    }
}
