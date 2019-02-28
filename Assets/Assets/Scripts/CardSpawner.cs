using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour {

    public GameObject[] cards;
    public GameObject[] cards1;
    public GameObject[] cards2;
    public Transform gridLayout;
       
    private void Awake()
    {
        ChooseSpawner();
        
    }

    public void RandomCardSpawner()
    {
        int cardsLenght = cards.Length;
        for (int i = 0; i < cardsLenght; i++)
        {
            int rand = Random.Range(0, 6);
            
            GameObject card;
            card = Instantiate(cards[i]);
            card.transform.SetParent(gridLayout);
            card.transform.SetSiblingIndex(rand);
        }
    }
    public void RandomCardSpawner1()
    {
        int cardsLenght = cards.Length;
        for (int i = 0; i < cardsLenght; i++)
        {
            int rand = Random.Range(0, 6);

            GameObject card;
            card = Instantiate(cards1[i]);
            card.transform.SetParent(gridLayout);
            card.transform.SetSiblingIndex(rand);
        }
    }
    public void RandomCardSpawner2()
    {
        int cardsLenght = cards.Length;
        for (int i = 0; i < cardsLenght; i++)
        {
            int rand = Random.Range(0, 6);

            GameObject card;
            card = Instantiate(cards2[i]);
            card.transform.SetParent(gridLayout);
            card.transform.SetSiblingIndex(rand);
        }
    }

    public void ChooseSpawner()
    {
        int ix = Random.Range(0, 3);
        if (ix == 0)
        {
            RandomCardSpawner1();
        }
        else if (ix == 1)
        {
            RandomCardSpawner();
        }
        else
        {
            RandomCardSpawner2();
        }
    }




}

   
