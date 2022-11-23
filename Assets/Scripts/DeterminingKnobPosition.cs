using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeterminingKnobPosition : MonoBehaviour
{
    [SerializeField] private RectTransform knob;
    [SerializeField] private RectTransform successfulZone;
    [SerializeField] private RectTransform perfectZone;

    public MinigameResult CheckPosition()
    {
        List<Vector2> rangePerfectZones = CalculateRangeZone(perfectZone);
        List<Vector2> rangeSuccessfulZones = CalculateRangeZone(successfulZone);

        if(knob.anchoredPosition.x > rangePerfectZones[0].x && knob.anchoredPosition.x < rangePerfectZones[1].x)
        {
            return MinigameResult.Perfect;
        }
        else if (knob.anchoredPosition.x > rangeSuccessfulZones[0].x && knob.anchoredPosition.x < rangeSuccessfulZones[1].x)
        {
            return MinigameResult.Successful;
        }
        else
        {
            return MinigameResult.Missed;
        }
    }

    private List<Vector2> CalculateRangeZone(RectTransform zone)
    {
        List<Vector2> RangeZone = new List<Vector2>();

        Vector2 startZone = new Vector2(zone.anchoredPosition.x - zone.sizeDelta.x / 2, zone.anchoredPosition.y);
        Vector2 endZone = new Vector2(zone.anchoredPosition.x + zone.sizeDelta.x / 2, zone.anchoredPosition.y);
        RangeZone.Add(startZone);
        RangeZone.Add(endZone);

        return RangeZone;
    }
}
