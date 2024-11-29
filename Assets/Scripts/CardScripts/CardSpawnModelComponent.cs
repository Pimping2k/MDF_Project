using System.Collections;
using System.Collections.Generic;
using CardScripts;
using Containers;
using CoreMechanic;
using UnityEngine;
using GameObject = CardScripts.GameObject;

public class CardSpawnModelComponent : MonoBehaviour
{
    [SerializeField] private GameObject cardModel;
    private float maxDistance = 10000f;

    public bool FindAvailableLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
            {
                var cardModelIstance = Instantiate(cardModel, hit.transform);
                DeckManager.Instance.PlayerCards.Remove(gameObject);

                TableCardManager.Instance.playerCardsInstance.Add(cardModelIstance);
                
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }
}