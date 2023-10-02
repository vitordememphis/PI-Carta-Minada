using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WriteButtonManager : MonoBehaviour
{
    //Variável para o botão
    public Button noteButton;

    //Variáveis para a cor do botão antes e depois de clicar
    ColorBlock clickedColor;
    ColorBlock nonClickedColor;

    //Variável pra saber se o jogo está no modo anotação
    public static bool writeMode = false;

    //Indicador de qual carta está selecionada
    public GameObject cardIndicator;

    //Lista de objetos cardBack
    public List<Transform> cards = new();

    //Inteiro pra fazer o inicador começar na primeira carta
    int defaultCard = 0;


    void Start()
    {
        //Deixando o indicador inativo e na posição da carta 1
        cardIndicator.transform.position = cards[defaultCard].transform.position;
        cardIndicator.SetActive(false);

        //Guardando as cores do botão pressionado e não pressionado
        noteButton = GetComponent<Button>();
        clickedColor = noteButton.colors;
        clickedColor.selectedColor = noteButton.colors.pressedColor;
        clickedColor.normalColor = noteButton.colors.pressedColor;
        nonClickedColor = noteButton.colors;
    }

    void Update()
    {
        if(writeMode)
        {
            MoveIndicator();
        }
    }

    //Função pro botão ativar e desativar o modo de escrita
     public void ActivateWriteMode()
    {
        if(!writeMode)
        {
            writeMode = true;
            cardIndicator.SetActive(true);

            //Mudando a cor do botão enquanto o modo de anotação estiver ativo
            noteButton.colors = clickedColor;
        }
        else
        {
            writeMode = false;
            cardIndicator.SetActive(false);

            //Mudando a cor do botão de volta quando o modo de anotação estiver iativo
            noteButton.colors = nonClickedColor;
        }
    }


    //Função pra mover o indicador pra a carta clicada
    void MoveIndicator()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null)
            {
                for(int i=0; i<cards.Count; i++)
                {
                    if(hit.collider.gameObject.tag == cards[i].tag)
                    {
                        cardIndicator.transform.position = cards[i].transform.position;
                        break;
                    }
                }
            }
        }
    }


}
