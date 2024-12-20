using System;
using System.Collections;
using System.Collections.Generic;
using Containers;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace CardScripts
{
    public class CardItemModel : MonoBehaviour, IHittable, IStepable
    {
        [SerializeField] private CardItemView cardView;
        
        [SerializeField] private HealthComponent HealthComponent;
        [SerializeField] private DamageComponent DamageComponent;
        public int currentSlotId = -1;

        private Coroutine stepCoroutine;
        private bool isMoving = false;
        private Vector3 originalPosition;

        private float damage;
        private float health;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            health = HealthComponent.Health;
            damage = DamageComponent.Damage;
        }

        public void Hit()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, transform.up, out var hitInfo,
                    Quaternion.identity, maxDistance: 10f))
            {
                if (hitInfo.collider.CompareTag(TagsContainer.ENEMYCARD))
                {
                    var enemyHealth = hitInfo.collider.GetComponent<HealthComponent>();
                    
                    PerformAttack(hitInfo.collider.gameObject, hitInfo.collider.transform.position, enemyHealth);
                }
            }
        }

        public IEnumerator Step()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, this.transform.up,
                    out var hitInfo, Quaternion.identity, maxDistance: 10f))
            {
                Debug.DrawRay(transform.position, transform.up, Color.red, 10f);
                Debug.Log(hitInfo.collider.tag);
                if (hitInfo.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
                {
                    var interactionSlot = hitInfo.collider.GetComponent<Slot>();
                    var interactionTransform = hitInfo.collider.transform;

                    if (interactionSlot.IsOccupied)
                        yield break;

                    var currentSlot = this.GetComponentInParent<Slot>();
                    if (currentSlot != null)
                    {
                        currentSlot.ClearCard();
                    }

                    yield return transform.DOMove(interactionTransform.parent.position, 0.15f).OnComplete(() =>
                    {
                        currentSlotId = interactionSlot.ID;
                        transform.parent = interactionTransform;
                        transform.localPosition = Vector3.zero;
                        interactionSlot.AssignCard(this.gameObject);
                    });
                }
                else
                {
                    Hit();
                }
            }
        }

        private void PerformAttack(GameObject target, Vector3 targetPosition, HealthComponent enemyHealth)
        {
            originalPosition = Vector3.zero;
            Sequence attackSequence = DOTween.Sequence();

            attackSequence.Append(this.transform.DOMove(targetPosition, 0.3f).SetEase(Ease.OutCubic).OnComplete((() =>
            {
                target.transform.DOShakePosition(0.5f, new Vector3(0.15f, 0.15f, 0.15f)).SetEase(Ease.InBounce);
            })));

            attackSequence.AppendCallback(() => enemyHealth.DecreaseHealth(DamageComponent.Damage));

            attackSequence.Append(this.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InCubic).SetDelay(0.1f));
        }
    }
}