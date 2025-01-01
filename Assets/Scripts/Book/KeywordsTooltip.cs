using System;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeywordsTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private GameObject DescriptionPrefab;
    [SerializeField] private TMP_Text DescriptionText;

    private GameObject currentDescription;
    private RectTransform currentDescriptionTransform;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentDescription == null)
        {
            currentDescription = Instantiate(DescriptionPrefab, transform);

            currentDescriptionTransform = currentDescription.GetComponent<RectTransform>();
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentDescription != null)
        {
            Destroy(currentDescription);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (currentDescription != null)
        {
            Vector2 mousePosition = eventData.position;
            Vector2 adjustedPosition = mousePosition + new Vector2(currentDescriptionTransform.rect.width / 2, -currentDescriptionTransform.rect.height / 2);
            currentDescriptionTransform.position = adjustedPosition;
        }
    }
}