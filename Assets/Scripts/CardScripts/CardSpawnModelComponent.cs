using System;
using System.Collections;
using System.Collections.Generic;
using CardScripts;
using Components;
using Containers;
using CoreMechanic;
using DG.Tweening;
using UnityEngine;

public class CardSpawnModelComponent : MonoBehaviour
{
    [SerializeField] private GameObject cardModel;
    [SerializeField] private float manaPrice;
    private float maxDistance = 10000f;

    private CardItemModel _cardItemModelComponent;
    private CardModelGrabComponent _cardItemModelGrabComponent;

    private void Start()
    {
        _cardItemModelComponent = cardModel.GetComponent<CardItemModel>();
        _cardItemModelGrabComponent = cardModel.GetComponent<CardModelGrabComponent>();
    }

    public bool FindAvailableLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
            {
                var slotComponent = hit.collider.GetComponent<Slot>();

                if (slotComponent.IsOccupied)
                {
                    Debug.Log("Occupied");
                    return false;
                }
                
                int slotID = slotComponent.ID;
                
                _cardItemModelComponent.currentSlotId = slotID;
                _cardItemModelGrabComponent.CurrentSlot = slotComponent;
                
                var cardModelIstance = Instantiate(cardModel, hit.transform);
                slotComponent.AssignCard(cardModelIstance);
                
                ManaComponent.Instance.DecreaseMana(manaPrice);
                
                DeckManager.Instance.PlayerCards.Remove(gameObject);
                TableCardManager.Instance.playerCardsInstance.Add(cardModelIstance);
                
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }
}