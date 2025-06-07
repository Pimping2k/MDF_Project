using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardScripts;
using Containers;
using UI.Phase;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreMechanic
{
    public class TableCardManager : MonoBehaviour
    {
        public static TableCardManager Instance;

        [SerializeField] private Animator bellAnimator;
        [SerializeField] private PhaseTurnUI _phaseTurnUI;
        
        public List<GameObject> playerCardsInstance;
        public List<GameObject> enemyCardsInstance;

        private IA_PlayerControl _playerControl;

        public List<CardItemModel> playerCardsQueue = new List<CardItemModel>();
        public List<EnemyCardItemModel> enemyCardsQueue = new List<EnemyCardItemModel>();

        private CardItemModel[] playerCardsInstanceModels;
        private EnemyCardItemModel[] enemyCardsInstanceModels;

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
                    _phaseTurnUI.ShowParticipantTurn(true);
                    StartCoroutine(ReorganizeCards(playerCardsInstance));
                }
            }
        }

        private IEnumerator ReorganizeCards(List<GameObject> cardsInstances)
        {
            playerCardsInstanceModels = cardsInstances.Select(c => c.GetComponent<CardItemModel>()).ToArray();

            var sortedCardsModels = playerCardsInstanceModels.OrderByDescending(c => c.currentSlotId).ToArray();

            foreach (var card in sortedCardsModels)
            {
                playerCardsQueue.Add(card);
            }

            foreach (var card in sortedCardsModels)
            {
                if (!card.IsMoving)
                {
                    card.IsMoving = true;
                    bellAnimator.SetBool(AnimationStatesContainer.ISCLICKED, true);
                    yield return new WaitForSeconds(0.1f);
                    yield return StartCoroutine(card.Step());
                    card.IsMoving = false;
                    playerCardsQueue.Remove(card);
                }
            }

            if (bellAnimator.GetBool(AnimationStatesContainer.ISCLICKED))
            {
                StartCoroutine(ReorganizeEnemyCards(enemyCardsInstance));
            }
        }

        private IEnumerator ReorganizeEnemyCards(List<GameObject> cardsInstances)
        {
            yield return new WaitForSeconds(0.75f);

            enemyCardsInstanceModels = cardsInstances.Select(c => c.GetComponent<EnemyCardItemModel>()).ToArray();

            var sortedCardsModels = enemyCardsInstanceModels.OrderByDescending(c => c.currentSlotId).ToArray();

            foreach (var card in sortedCardsModels)
            {
                enemyCardsQueue.Add(card);
            }

            foreach (var card in sortedCardsModels)
            {
                card.IsMoving = true;
                yield return new WaitForSeconds(0.1f);
                yield return StartCoroutine(card.Step());
                card.IsMoving = false;
                enemyCardsQueue.Remove(card);
            }
            
            bellAnimator.SetBool(AnimationStatesContainer.ISCLICKED, false);
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