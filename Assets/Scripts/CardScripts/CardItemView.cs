using CardScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardItemView : MonoBehaviour, ICustomDrag
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private Image passiveImage;
    [SerializeField] private Image activeImage;

    [SerializeField] private HealthComponent HealthComponent;
    [SerializeField] private DamageComponent DamageComponent;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CardSpawnModelComponent spawnModelComponent;
    private Player player;
    private Vector3 originPosition;
    private int id;
    
    public int ID
    {
        get => id;
        set => id = value;
    }

    public void OnCurrentDrag()
    {
        CameraManager.Instance.ZoomIn();
        rectTransform.position = Input.mousePosition;
        player.canInput = false;
        if (player.state == Player.CameraState.book)
            player.bookManager.MoveIn();
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
            rectTransform.position = originPosition;
        }

        CameraManager.Instance.ZoomOut();
        player.canInput = true;
        player.state = Player.CameraState.standart;
    }

    private void Awake()
    {
        UpdateCardText();
        player = FindAnyObjectByType<Player>();
    }

    private void Start()
    {
        originPosition = rectTransform.position;
    }

    private void UpdateCardText()
    {
        healthText.text = HealthComponent.Health.ToString();
        damageText.text = DamageComponent.Damage.ToString();
    }
}