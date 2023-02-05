using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class StartController : MonoBehaviour
{
    
    [SerializeField]
    List<GameMode> gameModes;

    [SerializeField]
    TMP_Dropdown difficultyDropdown;
    private void Start()
    {
        DontDestroyOnLoad(GameInstanceController.Instance.gameObject);


    }


    public void OnDifficulty()
    {

        GameInstanceController.Instance.Mode = gameModes[difficultyDropdown.value];
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
