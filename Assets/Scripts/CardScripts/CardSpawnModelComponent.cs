using System.Collections;
using System.Collections.Generic;
using Containers;
using UnityEngine;

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
                Instantiate(cardModel, hit.transform);
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }
}