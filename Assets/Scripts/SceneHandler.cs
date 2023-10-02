using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");

        //Resetando a quantidade de dois e três para o próximo jogo
        GameManager.instance.twoCount = 0;
        GameManager.instance.threeCount = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
