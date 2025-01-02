using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardItemView CardItemView;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Right)
            return;
        
        Player.Instance.state = Player.CameraState.book;
        CameraManager.Instance.ZoomOut();
        BookManager.Instance.OpenPage(CardItemView.ID);
    }
}