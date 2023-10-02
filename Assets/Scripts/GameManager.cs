using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variáveis carregadas pelo singleton
    public bool gameOver = false;
    public bool levelComplete = false;
    // Variáveis para contar números 2 e 3 no jogo
    public int twoCount = 0;
    public int threeCount = 0;
    // Variáveis para contar números 2 e 3 já descobertos
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
