using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    // cardBack é o objeto que contém a parte de trás da carta
    public GameObject cardBack;

    // cardFront é o objeto que contém a frente da carta
    public GameObject cardFront;

    // cardSlot é a posição na tela que a carta será colocada
    public Transform cardSlot;

    // cardText é o texto da carta
    public TMP_Text cardText;

    //bombImage é a imagem de bomba
    public GameObject bombImage;

    // cardNumber é o número da carta
    public int cardNumber;

    // Variáveis pra fazer girar a carta
    private bool coroutineAllowed, facedUp;

    //Variáveis para os quatro espaços de anotação
    public GameObject writeBombSlot, writeSlot1, writeSlot2, writeSlot3;

    //Botões para anotação de cada número
    public Button bombButton, button1, button2, button3;


    // Start is called before the first frame update
    void Start()
    {
        //Variáveis do singleton
        //Quando a tela do jogo carregar, o fim de jogo o nível completo deve ser falso
        GameManager.instance.gameOver = false;
        GameManager.instance.levelComplete = false;

        //Deixando os textos do modo anotação desabilitados
        writeBombSlot.SetActive(false);
        writeSlot1.SetActive(false);
        writeSlot2.SetActive(false);
        writeSlot3.SetActive(false);

        //Sorteando o número da carta
        GenerateNumber();
        cardFront.SetActive(false);

        //Posicionando a carta no lugar certo da tela
        cardBack.transform.position = cardSlot.position;
        cardFront.transform.position = cardSlot.position;

        //Variáveis da corrotina pra fazer a carta girar
        coroutineAllowed = true;
        facedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Se o modo de anotação não estiver ativo e não for fim de jogo
        if(!WriteButtonManager.writeMode && !GameManager.instance.gameOver)
        {
            if(Input.GetMouseButtonDown(0))
            {
                // Se clicar na carta, o número será revelado
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                
                // Se o raycast encontrou algum colisor
                if(hit.collider != null)
                {
                    //Início da corrotina
                    if(coroutineAllowed && hit.collider.gameObject.tag == cardBack.tag)
                    {
                        StartCoroutine(FlipCard());

                        //Se a carta virada for de valor 0 (bomba), fim de jogo
                        if (cardNumber == 0)
                        {
                            GameManager.instance.gameOver = true;
                        }

                        //Contagem dos dois encontrados
                        if (cardNumber == 2)
                        {
                            GameManager.instance.twoFlipped++;
                        }

                        //Contagem dos três encontrados
                        if (cardNumber == 3)
                        {
                            GameManager.instance.threeFlipped++;
                        }

                        //Se todos os dois e três foram encontrados, fim de jogo
                        if (GameManager.instance.twoFlipped == GameManager.instance.twoCount && GameManager.instance.threeFlipped == GameManager.instance.threeCount)
                        {
                            GameManager.instance.gameOver = true;
                            GameManager.instance.levelComplete = true;
                        }
                    }
                }
            }
        }

        //Se for fim de jogo, vira todas as cartas
        if(GameManager.instance.gameOver)
        {
            Invoke("GameOver", 2f);
            StopCoroutine(FlipCard());
        }
    }

// Gerador de números aleatórios pra carta Bomba - 30%; 1 - 50%; 2 - 10%; 3 - 10%
    void GenerateNumber()
    {
        int randomNumber;
        randomNumber = UnityEngine.Random.Range(1,11);
        if(randomNumber <= 5)
        {
            cardNumber = 1;

            //Comando pra fazer o número sorteado aparecer em texo na carta virada
            cardText.SetText(cardNumber.ToString());
        }
        else if(randomNumber > 5 && randomNumber <= 8)
        {
            cardNumber = 0;
        }
        else if(randomNumber > 8 && randomNumber <= 9)
        {
            cardNumber = 2;
            //Fazendo a contagem de dois no jogo
            GameManager.instance.twoCount++;

            //Comando pra fazer o número sorteado aparecer em texo na carta virada
            cardText.SetText(cardNumber.ToString());
        }
        else if(randomNumber == 10)
        {
            cardNumber = 3;
            //Fazendo a contagem de três no jogo
            GameManager.instance.threeCount++;

            //Comando pra fazer o número sorteado aparecer em texo na carta virada
            cardText.SetText(cardNumber.ToString());
        }
    }

// Função pra virar a carta e revelar seu número
    private IEnumerator FlipCard()
    {
        coroutineAllowed = false;
        if(!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                cardBack.transform.rotation = Quaternion.Euler(0f, i, 0f);
                cardFront.transform.rotation = Quaternion.Euler(0f, i, 0f);
                if(i==90f)
                {
                    cardFront.SetActive(true);
                    cardBack.SetActive(false);

                    //Se o número for dierente de zero, inativa a imagem da bomba
                    if (cardNumber != 0)
                    {
                        bombImage.SetActive(false);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        coroutineAllowed = false;
        facedUp = !facedUp;
    }

    //Funções pra escrever 0, 1, 2 e 3 nas cartas
    public void WriteBomb()
    {
        if(!writeBombSlot.activeInHierarchy)
        {
            writeBombSlot.SetActive(true);
        }

        if(writeBombSlot.activeInHierarchy)
        {
            writeBombSlot.SetActive(false);
        }
    }

    public void WriteNumber1()
    {
        
        if(!writeSlot1)
        {
            writeSlot1.SetActive(true);
            }
        if(writeSlot1)
        {
            writeSlot1.SetActive(false);            
        }
        
    }

    public void WriteNumber2()
    {
        if(!writeSlot2)
        {
            writeSlot2.SetActive(true);
        }
        if(writeSlot2)
        {
            writeSlot2.SetActive(false);
        }
        
    }

    public void WriteNumber3()
    {
        if(!writeSlot3)
        {
            writeSlot3.SetActive(true);
        }
        if(writeSlot3)
        {
            writeSlot3.SetActive(false);
        }        
    }

    void GameOver()
    {
        //Se o verso da carta estiver ativo, vira a carta.
        // ------------  NO FIM DO JOGO, TODAS AS CARTAS SÃO ABERTAS ------------
        if(cardBack.activeInHierarchy)
        {
            StartCoroutine(FlipCard());
        }
    }
}
