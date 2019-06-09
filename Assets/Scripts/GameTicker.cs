using UnityEngine;

/// <summary>
/// This component triggers the Game Ticker.
/// In the future the Game Ticker could be triggered by a background thread independently from Unity.
/// </summary>
public class GameTicker : MonoBehaviour
{
    private float currentTime = 0.0f;

    private void Update()
    {
        this.currentTime += Time.deltaTime;
        Root.GameService.Tick(this.currentTime);
    }
}
