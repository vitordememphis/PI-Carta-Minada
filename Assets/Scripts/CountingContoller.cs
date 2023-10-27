using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountingContoller : MonoBehaviour
{
    //Array de scripts pra coletar o número sorteado de cada carta da linha ou coluna
    public CardController[] cardScripts = new CardController[5];
    public TMP_Text rowColumnText;
    public int bombCount = 0;
    public int pointsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CountCards", 0.1f);
    }

    void CountCards()
    {
        for(int i = 0 ; i < cardScripts.Length; i++)
        {
            pointsCount += cardScripts[i].cardNumber;
            if(cardScripts[i].cardNumber == 0)
            {
                bombCount++;
            }
        }

        rowColumnText.text = pointsCount.ToString("00") + "\n" + ":" + bombCount.ToString("00");
    }
}
