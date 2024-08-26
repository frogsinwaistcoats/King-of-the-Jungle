using UnityEngine;

public class AimUI : MonoBehaviour
{
    public RectTransform aimUIIndicator; // Assign this via the Inspector

    private void Start()
    {
        if (aimUIIndicator == null)
        {
            Debug.LogError("Aim UI Indicator is not assigned!");
        }
    }
}