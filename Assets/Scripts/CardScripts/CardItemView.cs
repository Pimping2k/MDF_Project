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
    
    private Vector3 originPosition;

    public void OnCurrentDrag()
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndCurrentDrag()
    {
        rectTransform.position = originPosition;
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

    private void OnMouseDrag()
    {
        rectTransform.position = Input.mousePosition;
    }

    private void OnMouseEnter()
    {
        rectTransform.position =
            new Vector3(rectTransform.position.x, rectTransform.position.y + 30, rectTransform.position.z);
    }

    private void OnMouseExit()
    {
        rectTransform.position = originPosition;
    }

}