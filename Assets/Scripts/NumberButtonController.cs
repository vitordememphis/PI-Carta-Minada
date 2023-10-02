using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberButtonController : MonoBehaviour
{
    //Script do botão de anotação
    public WriteButtonManager noteMode;

    //Lista de cartas
    public List<GameObject> cardSlots = new();

    //Lista de espaços de anotação de cada carta
    public List<GameObject> numberSlots = new();


    //Função pra anotar um número no verso da carta
    public void WriteNumber()
    {
        if(WriteButtonManager.writeMode)
        {
            for(int i = 0; i < numberSlots.Count; i++)
            {
                if(noteMode.cardIndicator.transform.position == cardSlots[i].transform.position)
                {
                    if(!numberSlots[i].activeInHierarchy)
                    {
                        numberSlots[i].SetActive(true);
                    }
                    else
                    {
                        numberSlots[i].SetActive(false);
                    }                    
                    break;
                }
            }
        }
    }


}
