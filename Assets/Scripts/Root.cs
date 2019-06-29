using TWF;
using UnityEngine;

public static class Root
{
    public static GameService GameService { get; set; } = new GameService(GameObject.FindObjectOfType<UnityConfigProvider>());

    public static IWorldView WorldView => GameService.GetWorldView();
}
