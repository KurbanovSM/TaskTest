using System.Threading.Tasks;
using System;

public interface IMinigameClientHandler
{
    Task<MinigameResult> PlayAsync(TimeSpan maxDuration);

    void StopIfPlaying();
}