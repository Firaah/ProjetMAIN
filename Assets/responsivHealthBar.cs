using UnityEngine;

[ExecuteAlways]
public class ResponsiveBossBar : MonoBehaviour
{
    [Range(0f, 1f)] public float verticalOffsetRatio = 0.05f; // 5% du haut de l’écran
    [Range(0.1f, 1f)] public float widthRatio = 0.6f;         // % de la largeur de l’écran
    public float height = 20f;                                // hauteur fixe en pixels

    void Update()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Rect canvasRect = rt.root.GetComponent<Canvas>().GetComponent<RectTransform>().rect;

        float screenHeight = canvasRect.height;
        float screenWidth = canvasRect.width;

        rt.anchorMin = new Vector2(0.5f, 1f);
        rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 1f);

        float yOffset = -screenHeight * verticalOffsetRatio;
        float width = screenWidth * widthRatio;

        rt.anchoredPosition = new Vector2(0, yOffset);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
