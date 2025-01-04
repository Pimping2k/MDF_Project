using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float YOffset = 150f;
    private LayoutElement cardLayoutElementComponent;
    private RectTransform rectTransform;
    private Vector3 originPos;
    private Vector3 targetPos;

    private void Start()
    {
        cardLayoutElementComponent = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
        originPos = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            cardLayoutElementComponent.ignoreLayout = true;
            //targetPos = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y + YOffset, rectTransform.localPosition.z);
            rectTransform.DOLocalMoveY(YOffset,0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform != null)
        {
            cardLayoutElementComponent.ignoreLayout = false;
            rectTransform.position = originPos;
        }
    }
}