using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardScripts
{
    public interface ICustomDrag
    {
        void OnCurrentDrag();
        void OnEndCurrentDrag();
    }

    public class CardDrag : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject card;
        private ICustomDrag onDrag;

        private void Start()
        {
            onDrag = card.GetComponent<ICustomDrag>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.OnCurrentDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onDrag.OnEndCurrentDrag();
        }
    }
}