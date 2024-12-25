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
    public class EnemyCardItemModel : MonoBehaviour, IHittable, IStepable
    {
        [SerializeField] private HealthComponent HealthComponent;
        [SerializeField] private DamageComponent DamageComponent;

        public int currentSlotId = -1;

        private Coroutine stepCoroutine;
        private bool isMoving = false;
        private bool isHitting = false;
        private Vector3 originalPosition;

        private float damage;
        private float health;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }

        public bool IsHitting
        {
            get => isHitting;
            set => isHitting = value;
        }

        private void Awake()
        {
            HealthComponent = this.GetComponent<HealthComponent>();
            DamageComponent = this.GetComponent<DamageComponent>();
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
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, transform.up * -1, out var hitInfo,
                    Quaternion.identity, maxDistance: 10f))
            {
                if (hitInfo.collider.CompareTag(TagsContainer.PLAYERCARDITEMMODEL))
                {
                    var enemyHealth = hitInfo.collider.GetComponent<HealthComponent>();

                    PerformAttack(hitInfo.collider.gameObject, hitInfo.collider.transform.position, enemyHealth);
                }
            }
        }

        public IEnumerator Step()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, this.transform.up * -1,
                    out var hitInfo, Quaternion.identity, maxDistance: 50f))
            {
                Debug.DrawRay(transform.position, transform.up, Color.red, 10f);
                Debug.Log($"{this.gameObject.name} found card with tag : " + hitInfo.collider.tag, this);
                if (hitInfo.collider.CompareTag(TagsContainer.ENEMYCARDSLOT))
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
            isHitting = true;
            originalPosition = this.transform.position;
            Sequence attackSequence = DOTween.Sequence();

            Vector3 halfwayPosition = Vector3.Lerp(originalPosition, targetPosition, 0.5f);

            attackSequence.Append(this.transform.DOMove(halfwayPosition, 0.3f).SetEase(Ease.OutCubic).OnComplete((() =>
            {
                target.transform.DOShakePosition(0.5f, new Vector3(0.15f, 0.15f, 0.15f)).SetEase(Ease.InBounce);
            })));

            attackSequence.AppendCallback(() => enemyHealth.DecreaseHealth(DamageComponent.Damage));

            attackSequence.Append(this.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InCubic).SetDelay(0.1f)).OnComplete(() => isHitting = false);
        }
    }
}