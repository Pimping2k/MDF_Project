using System;
using System.Collections;
using Containers;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

namespace CardScripts
{
    public class CardItemModel : MonoBehaviour, IHittable, IStepable
    {
        [SerializeField] private CardItemView cardView;
        public int currentSlotId = -1;
        private float damage;
        private float health;

        private Coroutine step;
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            damage = cardView.damageComponent.Damage;
            health = cardView.healthComponent.Health;
        }

        public void Hit()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                step = StartCoroutine(Step());
            }
        }

        public IEnumerator Step()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.5f, this.transform.up,
                    out var hitInfo, Quaternion.identity, maxDistance: 10f))
            {
                if (hitInfo.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
                {
                    var interactionSlot = hitInfo.collider.GetComponent<Slot>();
                    var interactionTransform = hitInfo.collider.transform;

                    if (interactionSlot.IsOccupied)
                        yield return null;

                    var currentSlot = this.GetComponentInParent<Slot>();
                    if (currentSlot != null)
                    {
                        currentSlot.ClearCard();
                    }
                    transform.DOMove(interactionTransform.parent.position, 0.3f).OnComplete(() =>
                    {
                        currentSlotId = interactionSlot.ID;
                        transform.parent = interactionTransform;
                        transform.localPosition = Vector3.zero;
                        interactionSlot.AssignCard(this.gameObject);
                    });
                }
            }
        }
    }
}