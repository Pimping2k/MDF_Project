using CardScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItemView : MonoBehaviour, ICustomDrag
{
    [Header("Text")]
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text healthText;
    [Header("Images")]
    [SerializeField] private Image passiveImage;
    [SerializeField] private Image activeImage;
    [Header("Components")]
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private DamageComponent damageComponent;
    [SerializeField] private LayoutElement layoutElementComponent;
    [SerializeField] private CardSpawnModelComponent spawnModelComponent;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private int id;
    
    public int ID
    {
        get => id;
        set => id = value;
    }

    public HealthComponent HealthComponent
    {
        get => healthComponent;
        set => healthComponent = value;
    }

    public DamageComponent DamageComponent
    {
        get => damageComponent;
        set => damageComponent = value;
    }
    
    private Vector3 originLocalPosition;

    private void Start()
    {
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
        if (spawnModelComponent.FindAvailableLocation())
        {
            Debug.Log(DeckManager.Instance.PlayerCards.Count);
            Debug.Log("Nashel");
        }
        else
        {
            rectTransform.localPosition = originLocalPosition;
            layoutElementComponent.ignoreLayout = false;
        }

        CameraManager.Instance.ZoomOut();
        Player.Instance.canInput = true;
        Player.Instance.state = Player.CameraState.standart;
    }

    private void Awake()
    {
        UpdateCardText();
    }

    private void UpdateCardText()
    {
        healthText.text = healthComponent.Health.ToString();
        damageText.text = damageComponent.Damage.ToString();
    }
}