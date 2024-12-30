using System;
using System.Collections.Generic;
using Containers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public static BookManager Instance;
    [SerializeField] private List<GameObject> bookPages = new List<GameObject>();

    private Image bookPageImage;

    public List<GameObject> BookPages
    {
        get => bookPages;
        set => bookPages = value;
    }

    private Animator animator;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        bookPageImage = GetComponent<Image>();
    }

    public void MoveOut()
    {
        animator.SetBool(AnimationStatesContainer.ISMOVINGOUT, true);
    }

    public void MoveIn()
    {
        animator.SetBool(AnimationStatesContainer.ISMOVINGOUT, false);
    }

    private void ClosePage()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    public void OpenPage(int pageId)
    {
        MoveOut();
        // var pageImage = bookPages[pageId].GetComponent<Image>();
        //
        // bookPageImage.sprite = pageImage.sprite;
        var pageInstance = Instantiate(bookPages[pageId], transform);
    }
}