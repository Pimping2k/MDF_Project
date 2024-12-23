﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardScripts;
using Containers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreMechanic
{
    public class TableCardManager : MonoBehaviour
    {
        public static TableCardManager Instance;

        [SerializeField] private Animator bellAnimator;

        public List<GameObject> playerCardsInstance;
        public List<GameObject> enemyCardsInstance;

        private IA_PlayerControl _playerControl;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            ClearTable();
        }

        private void OnEnable()
        {
            _playerControl = new IA_PlayerControl();
            _playerControl.Enable();
            _playerControl.PlayerMouseInteraction.LMBAction.performed += LMBActionRaycastOnperformed;
        }

        private void OnDisable()
        {
            _playerControl.Disable();
            _playerControl.PlayerMouseInteraction.LMBAction.performed -= LMBActionRaycastOnperformed;
        }

        private void LMBActionRaycastOnperformed(InputAction.CallbackContext obj)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, 10000f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.red, 5f);
                if (hitInfo.collider.CompareTag(TagsContainer.INTERACTABLEBELL))
                {
                    StartCoroutine(ReorganizeCards(playerCardsInstance));
                    StartCoroutine(ReorganizeCards(enemyCardsInstance));
                }
            }
        }

        private IEnumerator ReorganizeCards(List<GameObject> cardsInstances)
        {
            var cardsInstanceModels = cardsInstances.Select(c => c.GetComponent<CardItemModel>()).ToArray();

            var sortedCardsModels = cardsInstanceModels.OrderByDescending(c => c.currentSlotId).ToArray();

            foreach (var card in sortedCardsModels)
            {
                if (!card.IsMoving)
                {
                    card.IsMoving = true;
                    bellAnimator.SetBool(AnimationStatesContainer.ISCLICKED, true);
                    yield return new WaitForSeconds(0.1f);
                    yield return StartCoroutine(card.Step());

                    bellAnimator.SetBool(AnimationStatesContainer.ISCLICKED, false);
                    card.IsMoving = false;
                }
            }
        }

        private void ClearTable()
        {
            playerCardsInstance.ForEach(Destroy);
            enemyCardsInstance.ForEach(Destroy);

            playerCardsInstance.Clear();
            enemyCardsInstance.Clear();
        }
    }
}