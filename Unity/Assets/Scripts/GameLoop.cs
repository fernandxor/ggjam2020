using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    GameObject gameoverTitle, youWinTitle;

    static GameLoop instance;


    private void Awake()
    {
        instance = this;
    }

    public static void GameOver()
    {
        instance.gameoverTitle.SetActive(true);
        instance.Invoke("Reset", 3);
    }

    public static void YouWin()
    {
        instance.youWinTitle.SetActive(true);
        instance.Invoke("Reset", 3);
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }

}
