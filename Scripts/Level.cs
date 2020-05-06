using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    float delayTime = 2.0f;
    Coroutine GameOverCoroutine;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        GameOverCoroutine = StartCoroutine(GameOverDelay());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Game Over");
    }
}
