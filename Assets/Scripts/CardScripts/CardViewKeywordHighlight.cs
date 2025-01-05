using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardViewKeywordHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject KeywordDescriptionPrefab;
    [SerializeField] private TMP_Text Keyword;

    private GameObject currentDescription;
    private RectTransform currentDescriptionTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentDescription == null)
        {
            currentDescription = Instantiate(KeywordDescriptionPrefab, transform);

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

    private void Update()
    {
        if (currentDescription != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 adjustedPosition = mousePos + new Vector2(currentDescriptionTransform.rect.width / 2, -currentDescriptionTransform.rect.height / 2);
            currentDescriptionTransform.position = adjustedPosition;
        }
    }
}