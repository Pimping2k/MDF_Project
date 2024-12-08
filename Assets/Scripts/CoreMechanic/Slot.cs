using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int ID;
    [SerializeField] private GameObject currentCard;

    public GameObject CurrentCard
    {
        get => currentCard;
        set => currentCard = value;
    }

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
}