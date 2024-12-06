using System;
using System.Collections;
using System.Collections.Generic;
using CardScripts;
using Containers;
using CoreMechanic;
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

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
        _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        offset = transform.position - GetMouseWorldPosition();

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 10f))
        {
            Debug.Log($"Hit object tag: {hit.collider.tag}");

            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
            {
                _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
                var slotComponent = hit.collider.GetComponent<Slot>();

                slotComponent.ClearSlot();
                
                if (slotComponent.IsOccupied && slotComponent != currentSlot)
                {
                    Debug.Log("This slot is already occupied.");
                    return;
                }

                if (currentSlot != null && currentSlot != slotComponent)
                {
                    currentSlot.IsOccupied = false;
                    Debug.Log($"Previous slot {currentSlot.ID} freed.");
                }

                slotComponent.ClearSlot();
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
}