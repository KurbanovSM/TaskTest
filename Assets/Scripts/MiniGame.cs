using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGame : MonoBehaviour, IMinigameClientHandler
{
    [SerializeField] private SetSettingsMiniGame setSettingsMiniGame;
    [SerializeField] private DeterminingKnobPosition determiningKnobPosition;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private RectTransform otherZone;
    [SerializeField] private RectTransform knob;

    public MinigameResult result { get; private set; }

    private const int PERCENTAGE_WIDTH_PERFECT_ZONE = 10;
    private const int PERCENTAGE_WIDTH_SUCCESSFUL_ZONE= 30;
    private const float KNOB_SPEED = 8;

    private bool isStopMove = false;

    private TaskCompletionSource<MinigameResult> tcs;

    public async Task<MinigameResult> PlayAsync(TimeSpan maxDuration)
    {
        tcs = new TaskCompletionSource<MinigameResult>();

        StartMiniGame();

        var result = MinigameResult.Ignore;

        try
        {
            Task timeOutTask = Task.Delay(maxDuration);

            Task completedTask = await Task.WhenAny(tcs.Task, timeOutTask);

            if(completedTask == tcs.Task)
            {
                result = await tcs.Task;
            }
        }
        catch (TaskCanceledException tce)
        { }
        finally
        {
            button.onClick.RemoveAllListeners();
            StopCoroutine(MoveKnob(KNOB_SPEED));
            resultText.text = result.ToString();
            Invoke(nameof(DeactiveGameObgect), 1);
        }

        return result;
    }

    private void StartMiniGame()
    {
        resultText.text = "";
        setSettingsMiniGame.SetParameters(PERCENTAGE_WIDTH_SUCCESSFUL_ZONE, PERCENTAGE_WIDTH_PERFECT_ZONE);
        gameObject.SetActive(true);
        isStopMove = false;
        button.onClick.AddListener(delegate { isStopMove = true; StopCoroutine(MoveKnob(KNOB_SPEED)); tcs.TrySetResult(SetResultButtonClick()); });
        StartCoroutine(MoveKnob(KNOB_SPEED));
    }

    private MinigameResult SetResultButtonClick()
    {
        return determiningKnobPosition.CheckPosition();
    }

    private IEnumerator MoveKnob(float knobSpeed)
    {
        Vector2 target = new Vector2();
        Vector2 point1 = new Vector2(otherZone.anchoredPosition.x - otherZone.sizeDelta.x / 2 + knob.sizeDelta.x / 2, otherZone.anchoredPosition.y);
        Vector2 point2 = new Vector2(otherZone.anchoredPosition.x + otherZone.sizeDelta.x / 2 - knob.sizeDelta.x / 2, otherZone.anchoredPosition.y);

        knob.anchoredPosition = point1;
        target = point2;

        while (!isStopMove)
        {
            knob.anchoredPosition = Vector2.MoveTowards(knob.anchoredPosition, target, knobSpeed);

            if (Vector2.Distance(knob.anchoredPosition, target) < 1)
            {
                if(target == point1) target = point2;
                else target = point1;
            }

            yield return null;
        }
    }

    public void StopIfPlaying()
    {
        if(tcs != null) tcs.TrySetCanceled();

        button.onClick.RemoveAllListeners();
        StopCoroutine(MoveKnob(KNOB_SPEED));
        gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        StopIfPlaying();
    }

    private void DeactiveGameObgect()
    {
        gameObject.SetActive(false);
    }
}
