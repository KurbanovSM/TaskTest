using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System;

public class StarMiniGame : MonoBehaviour
{
    [SerializeField] private float timeOutInSeconds;
    [SerializeField] private MiniGame miniGame;

    public async void StartMiniGame()
    {
        TimeSpan time = new TimeSpan();
        time = TimeSpan.FromSeconds(timeOutInSeconds);

        MinigameResult result = await miniGame.PlayAsync(time);

        Debug.Log(result);
    }
}
