using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaComponent : MonoBehaviour
{
    public static ManaComponent Instance;

    [SerializeField] private Slider manaSlider;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;

    public event Action OnManaChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mana = maxMana;
        UpdateManaSlider();
    }

    private void OnEnable()
    {
        OnManaChanged += UpdateManaSlider;
    }

    private void OnDisable()
    {
        OnManaChanged -= UpdateManaSlider;
    }

    public float Mana
    {
        get => mana;
        set
        {
            mana = Mathf.Clamp(value, 0, maxMana);
            OnManaChanged?.Invoke();
        }
    }

    public void DecreaseMana(float value)
    {
        Mana -= value;
        AnimateManaSlider(Mana/maxMana);
    }

    public void IncreaseMana(float value)
    {
        Mana += value;
        AnimateManaSlider(Mana/maxMana);
    }

    private void AnimateManaSlider(float targetValue)
    {
        manaSlider.DOValue(targetValue, 1f)
            .SetEase(Ease.Linear);
        
        manaSlider.transform.DOScale(Vector3.one * 1.2f, 0.5f)
            .SetEase(Ease.OutBack)
            .OnKill(() => manaSlider.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
    }

    private void UpdateManaSlider()
    {
        manaSlider.value = Mana / maxMana;
        manaText.text = $"{mana}/{maxMana}";
    }
}