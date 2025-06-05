using System;
using CardScripts;
using Components;
using Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Active
{
    public class PowerUpActive : MonoBehaviour, ICustomDrag
    {
        private LayoutElement layoutElementComponent;
        private RectTransform rectTransform;
        private Vector3 originLocalPosition;
    
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            layoutElementComponent = GetComponent<LayoutElement>();
        
            originLocalPosition = rectTransform.localPosition;
        }

        public void OnCurrentDrag()
        {
            layoutElementComponent.ignoreLayout = true;
            CameraManager.Instance.ZoomIn();
            rectTransform.position = Input.mousePosition;
        
            Player.Instance.canInput = false;
            if (Player.Instance.state == Player.CameraState.book)
                Player.Instance.bookManager.MoveIn();
        }

        public void OnEndCurrentDrag()
        {
            if (FindAvailableCard()) { }
            else
            {
                rectTransform.localPosition = originLocalPosition;
                layoutElementComponent.ignoreLayout = false;
            }
        
            CameraManager.Instance.ZoomOut();
            Player.Instance.canInput = true;
            Player.Instance.state = Player.CameraState.standart;
        }

        private bool FindAvailableCard()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 10000f))
            {
                if (hit.collider.CompareTag(TagsContainer.PLAYERCARDITEMMODEL))
                {
                    hit.collider.GetComponent<DamageComponent>().IncreaseDamage(1);
                    hit.collider.GetComponent<HealthComponent>().IncreaseHealth(1);
                
                    DeckManager.Instance.PlayerCards.Remove(gameObject);
                    Destroy(gameObject);
                    return true;
                }
            }

            return false;
        }
    }
}