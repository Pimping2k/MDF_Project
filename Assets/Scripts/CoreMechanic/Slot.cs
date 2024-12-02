using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int ID;
    private GameObject currentCard;

    private bool isOccupied;

    public bool IsOccupied
    {
        get => currentCard != null;
        set => isOccupied = value;
    }

    public void AssignCard(GameObject card)
    {
        currentCard = card;
    }

    public void ClearSlot()
    {
        currentCard = null;
    }

    public void ClearCard()
    {
        currentCard = null;
        IsOccupied = false;
    }
    
    public GameObject GetCurrrentCard()
    {
        return currentCard;
    }
}
