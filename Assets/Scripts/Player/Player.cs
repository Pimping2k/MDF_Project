using System;
using Components;
using Containers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private HealthComponent healthComponent;

    [Header("States")] public CameraState state;

    public enum CameraState
    {
        standart,
        table,
        book
    }

    public bool canInput = true;

    [Header("References")] public BookManager bookManager;
    [SerializeField] private HealthComponent playerHealth;
    [SerializeField] private CameraManager cameraManager;
    private IA_PlayerControl playerControl;

    public HealthComponent HealthComponent => healthComponent;

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

    private void OnEnable()
    {
        state = CameraState.standart;
        playerControl = new IA_PlayerControl();
        playerControl.PlayerControl.GetDamage.performed += GetDamage_performed;
        playerControl.PlayerControl.ZoomIn.performed += ZoomIn_performed;
        playerControl.PlayerControl.ZoomOut.performed += ZoomOut_performed;
        playerControl.Enable();
    }

    private void ZoomOut_performed(InputAction.CallbackContext obj)
    {
        if (canInput)
            switch (state)
            {
                case CameraState.standart:
                    bookManager.MoveOut();
                    state = CameraState.book;
                    break;
                case CameraState.table:
                    cameraManager.ZoomOut();
                    state = CameraState.standart;
                    break;
                case CameraState.book: break;
            }
    }

    private void ZoomIn_performed(InputAction.CallbackContext obj)
    {
        if (canInput)
            switch (state)
            {
                case CameraState.standart:
                    cameraManager.ZoomIn();
                    state = CameraState.table;
                    break;
                case CameraState.book:
                    bookManager.MoveIn();
                    state = CameraState.standart;
                    break;
                case CameraState.table: break;
            }
    }

    private void GetDamage_performed(InputAction.CallbackContext obj)
    {
        playerHealth.DecreaseHealth(10);
    }

    private void OnDisable()
    {
        playerControl.PlayerControl.GetDamage.performed -= GetDamage_performed;
        playerControl.PlayerControl.ZoomIn.performed -= ZoomIn_performed;
        playerControl.PlayerControl.ZoomOut.performed -= ZoomOut_performed;
        playerControl.Disable();
    }
}