using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;

    private List<GameObject> cardInstanceList = new List<GameObject>();

    public List<GameObject> CardInstanceList
    {
        get => cardInstanceList;
        set => cardInstanceList = value;
    }

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
    }
}
