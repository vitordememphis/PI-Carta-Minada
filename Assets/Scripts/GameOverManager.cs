using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    //Vari�veis do canvas de fim de jogo
    [SerializeField] CanvasGroup gameOverCanvas;
    [SerializeField] GameObject gameOverObject;
    [SerializeField] TextMeshProUGUI gameOverText;


    private void Start()
    {
        gameOverCanvas.alpha = 0;
        gameOverObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Caso seja fim de jogo, ative o canvas
        if (GameManager.instance.gameOver && gameOverCanvas.alpha < 1)
        {
            if (GameManager.instance.levelComplete)
            {
                gameOverObject.SetActive(true);
                Invoke("ActivateCanvasLevelComplete", 4f);
            }
            else
            {
                gameOverObject.SetActive(true);
                Invoke("ActivateCanvasGameOver", 4f);
            }    
        }
    }

    //Corrotina do alpha do canvas (n�o funciona)
    private IEnumerator ShowGameOverCanvas()
    {
        for(float i = 0; i <= 1; i += 0.01f)
        {
            gameOverCanvas.alpha += i;
        }
        yield return new WaitForSeconds(0.1f);
    }

    //Fun��o pra come�ar a corrotina quando perde o jogo
    void ActivateCanvasGameOver()
    {
        StartCoroutine(ShowGameOverCanvas());
        gameOverText.text = "FIM DE JOGO";
    }


    //Fun��o pra come�ar a corrotina quando ganha o jogo
    void ActivateCanvasLevelComplete()
    {
        StartCoroutine(ShowGameOverCanvas());
        gameOverText.text = "PARAB�NS";
    }
}
