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

    public void Reset()
    {
        gameEnded = false;
    }
    public bool GetGameEnded()
    {
        return gameEnded;
    }

    #if UNITY_EDITOR
    // for debugging
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            HUDController.Instance.EndGame(true, 0);
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            HUDController.Instance.EndGame(true, 1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            HUDController.Instance.EndGame(true, 2);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            HUDController.Instance.EndGame(true, 3);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            HUDController.Instance.EndGame(true, 4);
        }
    }
    #endif
}
