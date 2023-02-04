using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oozeling.Helper;

public class GameInstanceController : Singleton<GameInstanceController>
{
    [SerializeField]
    public GameMode Mode;

    private bool gameEnded = false;
    public void EndGame()
    {
        gameEnded = true;
    }

    public bool GetGameEnded()
    {
        return gameEnded;
    }
}
