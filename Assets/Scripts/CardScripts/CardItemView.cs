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

    private Vector3 originPosition;

    public void OnCurrentDrag()
    {
        CameraManager.Instance.ZoomIn();
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndCurrentDrag()
    {
        if (spawnModelComponent.FindAvailableLocation())
        {
            Debug.Log("Nashel");
        }
        else
        {
            rectTransform.position = originPosition;
        }

        CameraManager.Instance.ZoomOut();
    }

    private void Awake()
    {
        UpdateCardText();
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