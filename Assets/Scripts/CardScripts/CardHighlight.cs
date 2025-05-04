using System.Collections;
using CardScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float YOffset = 50f;
    [SerializeField] private GameObject highlight;
    private CardItemView cardItemView;
    private LayoutElement cardLayoutElementComponent;
    private RectTransform rectTransform;
    private Vector3 originPos;
    private Vector3 targetPos;

    private void Start()
    {
        cardItemView = GetComponent<CardItemView>();
        cardLayoutElementComponent = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
        originPos = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform != null && !cardItemView.IsDragging)
        {
            highlight.SetActive(true);
            rectTransform.DOLocalMoveY(YOffset, 0.025f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform != null && !cardItemView.IsDragging)
        {
            highlight.SetActive(false);
            rectTransform.position = originPos;
            cardLayoutElementComponent.ignoreLayout = true;
            cardLayoutElementComponent.ignoreLayout = false;
        }
    }
}