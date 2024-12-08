using System;
using System.Collections;
using System.Collections.Generic;
using CardScripts;
using Containers;
using CoreMechanic;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TestTools;

public class CardModelGrabComponent : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Slot currentSlot;
    [SerializeField] private CardItemModel _cardItemModel;
    private Vector3 originPosition;
    private Vector3 offset;
    private float zCoord;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
        _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        offset = transform.position - GetMouseWorldPosition();
        _spriteRenderer = _cardItemModel.GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = 10;
        if (currentSlot != null)
        {
            currentSlot.IsOccupied = false;
            currentSlot = null;
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        _spriteRenderer.sortingOrder = 3;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 10f))
        {
            Debug.Log($"Hit object tag: {hit.collider.tag}");

            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDITEMMODEL))
            {
                SwapCards(hit.collider.gameObject);
            }
            else
            {
                transform.localPosition = Vector3.zero;
                _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
            }

            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
            {
                _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
                var slotComponent = hit.collider.GetComponent<Slot>();
                
                if (slotComponent.IsOccupied && slotComponent != currentSlot && currentSlot != null)
                {
                    SwapCards(hit.collider.gameObject);
                    Debug.Log("This slot is already occupied.");
                    return;
                }

                if (currentSlot != null && currentSlot != slotComponent)
                {
                    currentSlot.IsOccupied = false;
                    Debug.Log($"Previous slot {currentSlot.ID} freed.");
                }
                
                currentSlot = slotComponent;
                currentSlot.IsOccupied = true;
                Debug.Log($"Slot {currentSlot.ID} is now occupied.");

                _cardItemModel.currentSlotId = slotComponent.ID;
                _cardItemModel.transform.parent = hit.collider.transform;
                _cardItemModel.transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.localPosition = Vector3.zero;
                _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
        else
        {
            transform.localPosition = Vector3.zero;
            _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void SwapCards(GameObject targetCard)
    {
        var cardItemModel = _cardItemModel.GetComponent<CardItemModel>();
        var targetCardItemModel = targetCard.GetComponent<CardItemModel>();

        var cardItemModelSlotComponent = _cardItemModel.GetComponentInParent<Slot>();
        var targetCardSlotComponent = targetCard.GetComponentInParent<Slot>();
        
        if (cardItemModel == null || targetCardItemModel == null)
        {
            Debug.LogError("Одна из карт не имеет компонента CardItemModel!");
            return;
        }

        var tmpSlotId = cardItemModel.currentSlotId;
        cardItemModel.currentSlotId = targetCardItemModel.currentSlotId;
        targetCardItemModel.currentSlotId = tmpSlotId;
        
        var tmpParent = _cardItemModel.transform.parent;
        //_cardItemModel.transform.parent = targetCard.transform.parent;
        _cardItemModel.transform.parent.DOMove(targetCard.transform.parent.position, 1f);
        //targetCard.transform.parent = tmpParent;
        targetCard.transform.parent.DOMove(tmpParent.position, 1f);
        _cardItemModel.transform.localPosition = Vector3.zero;
        targetCard.transform.localPosition = Vector3.zero;
        Debug.Log($"Карты поменялись местами. Новые слоты: карта 1 - {cardItemModel.currentSlotId}, карта 2 - {targetCardItemModel.currentSlotId}");

        if (cardItemModelSlotComponent == null || targetCardSlotComponent == null)
        {
            Debug.Log("Dont have slot component");
            return;
        }

        cardItemModelSlotComponent.AssignCard(cardItemModel.gameObject);
        targetCardSlotComponent.AssignCard(targetCard);
        
        Debug.Log($"{cardItemModelSlotComponent.ID}",cardItemModelSlotComponent);
        Debug.Log($"{targetCardSlotComponent.ID}",targetCardSlotComponent);
    }
}