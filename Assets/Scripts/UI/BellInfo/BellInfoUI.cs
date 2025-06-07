using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.BellInfo
{
    public class BellInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _bellInfoText;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _bellInfoText.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _bellInfoText.gameObject.SetActive(false);
        }
    }
}