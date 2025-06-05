using System;
using CardScripts;
using Components;
using ConfigsScripts;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardScripts
{
    public class CardItemView : MonoBehaviour, ICustomDrag
    {
        [Header("Main config")] [SerializeField]
        private CardConfig config;

        [Header("Text")] [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text damageHighlightText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text healthHighlightText;

        [Header("Images")] [SerializeField] private Image passiveImage;
        [SerializeField] private Image activeImage;

        [Header("Components")] 
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private PassiveCardComponent passiveCardComponent;
        [SerializeField] private DamageComponent damageComponent;
        [SerializeField] private LayoutElement layoutElementComponent;
        [SerializeField] private CardSpawnModelComponent spawnModelComponent;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GameObject highlight;
        
        private bool isDragging;

        public CardConfig Config => config;
        public bool IsDragging => isDragging;

        private Vector3 originLocalPosition;

        private void Awake()
        {
            UpdateCardText();
            ApplyTokenEffect();
            UpdateHeathUI();
            UpdateDamageUI();
        }

        private void Start()
        {
            originLocalPosition = rectTransform.localPosition;
            layoutElementComponent = GetComponent<LayoutElement>();
        }

        public void OnCurrentDrag()
        {
            highlight.SetActive(false);
            layoutElementComponent.ignoreLayout = true;
            CameraManager.Instance.ZoomIn();
            rectTransform.position = Input.mousePosition;

            isDragging = true;
            Player.Instance.canInput = false;
            if (Player.Instance.state == Player.CameraState.book)
                Player.Instance.bookManager.MoveIn();
        }

        public void OnEndCurrentDrag()
        {
            if (spawnModelComponent.FindAvailableLocation())
            {
                Debug.Log(DeckManager.Instance.PlayerCards.Count);
                ApplyActiveAbility();
            }
            else
            {
                rectTransform.localPosition = originLocalPosition;
                layoutElementComponent.ignoreLayout = false;
            }

            isDragging = false;
            CameraManager.Instance.ZoomOut();
            Player.Instance.canInput = true;
            Player.Instance.state = Player.CameraState.standart;
        }

        private void UpdateCardText()
        {
            healthText.text = healthComponent.CurrentValue.ToString();
            damageText.text = damageComponent.Damage.ToString();
        }

        private void ApplyTokenEffect()
        {
            switch (config.Token)
            {
                case Token.Earth:
                    healthComponent.IncreaseHealth(1);
                    UpdateHeathUI();
                    break;
                case Token.Spear:
                    break;
                case Token.Provocation:
                    healthComponent.EnableTaunt(true);
                    break;
                case Token.PowerUp:
                    passiveCardComponent.PowerUpPassiveEffect(1);
                    break;
                case Token.Support:
                    foreach (var card in passiveCardComponent.summonedEntities)
                        card.GetComponent<HealthComponent>().IncreaseHealth(1);
                    break;
                case Token.Stealing:
                    GetComponent<CardItemModel>().HasTokenStealing = true;
                    break;
                default:
                    throw new Exception("Not appliable token type");
            }
        }
        
        private void ApplyActiveAbility()
        {
            DeckManager deckManager = DeckManager.Instance;
            switch (config.ActiveAbility)
            {
                case ActiveAbilities.PowerUp:
                    deckManager.AddCard(deckManager.activePrefabs[0]);
                    break;
                case ActiveAbilities.HitWithRock:
                    deckManager.AddCard(deckManager.activePrefabs[1]);
                    break;
                case ActiveAbilities.HealingUp:
                    deckManager.AddCard(deckManager.activePrefabs[2]);
                    break;
                default:
                    throw new Exception("No appliable active abilities type");
            }
        }
        
        public void UpdateHeathUI()
        {
            healthText.text = healthComponent.Health.ToString();
            healthHighlightText.text = healthComponent.Health.ToString();
        }

        public void UpdateDamageUI()
        {
            damageText.text = damageComponent.Damage.ToString();
            damageHighlightText.text = damageComponent.Damage.ToString();
        }
    }
}