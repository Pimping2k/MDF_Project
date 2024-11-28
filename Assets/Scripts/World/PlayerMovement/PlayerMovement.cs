using Containers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float maxDistance = 1000f;

    private Vector2 inputVector;

    private IA_PlayerControl playerControl;

    private void OnEnable()
    {
        playerControl = new IA_PlayerControl();
        playerControl.PlayerMovement.Move.performed += On_Move;
        playerControl.PlayerMovement.Move.canceled += On_Move;
        playerControl.PlayerMovement.Interact.performed += Interact_performed;
        playerControl.Enable();
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            if (hit.collider.CompareTag(TagsContainer.INTERACTABLE))
                hit.collider.GetComponent<IInteractable>().Interact();
    }

    private void On_Move(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputVector = obj.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        playerControl.PlayerMovement.Move.performed -= On_Move;
        playerControl.PlayerMovement.Move.canceled -= On_Move;
        playerControl.PlayerMovement.Interact.performed -= Interact_performed;
        playerControl.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y);
        move = Quaternion.Euler(0, 45, 0) * move;

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}