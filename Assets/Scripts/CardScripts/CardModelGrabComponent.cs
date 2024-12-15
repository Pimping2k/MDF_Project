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

    public Slot CurrentSlot
    {
        get => currentSlot;
        set => currentSlot = value;
    }

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
        currentSlot.ClearCard();
        zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
        _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        offset = transform.position - GetMouseWorldPosition();
        _spriteRenderer = _cardItemModel.GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = 10;
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

                if (currentSlot != null && currentSlot != slotComponent)
                {
                    currentSlot.IsOccupied = false;
                }

                currentSlot = slotComponent;
                currentSlot.AssignCard(gameObject);
                currentSlot.IsOccupied = true;
                
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
            return;


        var tmpSlotId = cardItemModel.currentSlotId;
        cardItemModel.currentSlotId = targetCardItemModel.currentSlotId;
        targetCardItemModel.currentSlotId = tmpSlotId;

        var tmpParent = _cardItemModel.transform.parent;
        var targetParent = targetCard.transform.parent;

        _cardItemModel.transform.DOMove(targetParent.position, 0.5f).OnComplete(() =>
        {
            _cardItemModel.transform.parent = targetParent;
            _cardItemModel.transform.localPosition = Vector3.zero;
        });

        targetCard.transform.DOMove(tmpParent.position, 0.5f).OnComplete(() =>
        {
            targetCard.transform.parent = tmpParent;
            targetCard.transform.localPosition = Vector3.zero;
        });

        if (cardItemModelSlotComponent == null || targetCardSlotComponent == null)
            return;


        cardItemModelSlotComponent.ClearCard();
        targetCardSlotComponent.ClearCard();
        cardItemModelSlotComponent.AssignCard(targetCard);
        targetCardSlotComponent.AssignCard(cardItemModel.gameObject);
    }
}