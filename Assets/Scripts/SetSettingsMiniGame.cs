using UnityEngine;

public class SetSettingsMiniGame : MonoBehaviour
{
    [SerializeField] private RectTransform otherZone;
    [SerializeField] private RectTransform successfulZone;
    [SerializeField] private RectTransform perfectZone;

    public void SetParameters(int percentageWidthSuccessfulZone, int percentageWidthPerfectZone)
    {
        SetZoneLength(successfulZone, percentageWidthSuccessfulZone);
        SetZoneLength(perfectZone, percentageWidthPerfectZone);

        SetZonePosition(successfulZone, otherZone);
        SetZonePosition(perfectZone, successfulZone);
    }

    private void SetZoneLength(RectTransform zone, int percent)
    {
        float otherZoneWidth = otherZone.sizeDelta.x;

        float result = ÑalculatingPercentage(percent, otherZoneWidth);
        zone.sizeDelta = new Vector2(result, zone.sizeDelta.y);
    }

    private void SetZonePosition(RectTransform zone1, RectTransform zone2)
    {
        float randomMin = (zone2.anchoredPosition.x - (zone2.sizeDelta.x / 2)) + zone1.sizeDelta.x / 2;
        float randomMax = (zone2.anchoredPosition.x + (zone2.sizeDelta.x / 2)) - zone1.sizeDelta.x / 2;

        float randomPosition = Random.Range(randomMin, randomMax);

        zone1.anchoredPosition = new Vector2(randomPosition, zone2.anchoredPosition.y);
    }

    private float ÑalculatingPercentage(int percent, float number)
    {
        return (number / 100) * percent;
    }
}
