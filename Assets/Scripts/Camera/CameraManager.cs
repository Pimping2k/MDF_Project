using System;
using System.Collections;
using System.Collections.Generic;
using Containers;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private Animator Animator;
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

    public void ZoomIn()
    {
        Animator.SetBool(AnimationStatesContainer.ISZOOMING, true);
    }

    public void ZoomOut()
    {
        Animator.SetBool(AnimationStatesContainer.ISZOOMING, false);
    }
}