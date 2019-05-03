using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTicker : MonoBehaviour
{
    private float currentTime = 0.0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        Root.GameService.TickAgents(currentTime);
    }
}
