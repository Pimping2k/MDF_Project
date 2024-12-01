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

        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
            Debug.Log(hit.collider.tag);
            Debug.DrawLine(ray.origin, ray.direction*10f,Color.red,10f);
            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
            {
                var slotComponent = hit.collider.GetComponent<Slot>();
                if (slotComponent.isOccupied)
                {
                    Debug.Log("Occupied");
                }

                int slotID = slotComponent.ID;
                _cardItemModel.currentSlotId = slotID;

                _cardItemModel.transform.parent = hit.transform;
                _cardItemModel.transform.position = Vector3.zero;
                _cardItemModel.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
}