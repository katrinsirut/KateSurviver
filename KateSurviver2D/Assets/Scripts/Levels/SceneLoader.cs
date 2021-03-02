using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Score score;

    // Запуск вызывается перед первым обновлением кадра
    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    public void LoadNextScene()
    {
        //Получаем нашу активную сцену, возвращаем ее указатель.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToMenu()
    {
        int startMenu = 0;
        SceneManager.LoadScene(startMenu);
        score.DestroyScoreObject();
    }

    //Эта функция будет работать только на отдельных сборках игры.
    public void ExitGame()
    {
        Application.Quit();
    }
}
