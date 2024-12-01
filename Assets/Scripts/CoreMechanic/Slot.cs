using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int ID;
    private GameObject currentCard;

    public bool isOccupied => currentCard != null;

    public void AssignCard(GameObject card)
    {
        currentCard = card;
    }

    public void ClearSlot()
    {
        currentCard = null;
    }

    public GameObject GetCurrrentCard()
    {
        return currentCard;
    }
}
