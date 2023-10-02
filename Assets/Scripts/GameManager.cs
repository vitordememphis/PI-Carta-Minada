using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Vari�veis carregadas pelo singleton
    public bool gameOver = false;
    public bool levelComplete = false;
    // Vari�veis para contar n�meros 2 e 3 no jogo
    public int twoCount = 0;
    public int threeCount = 0;
    // Vari�veis para contar n�meros 2 e 3 j� descobertos
    public int twoFlipped = 0; 
    public int threeFlipped = 0;


    //Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
