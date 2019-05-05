using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component triggers the Game Ticker.
/// In the future the Game Ticker could be triggered by a background thread independently from Unity.
/// </summary>
public class GameTicker : MonoBehaviour
{
    private float currentTime = 0.0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        Root.GameService.TickAgents(currentTime);
    }
}
