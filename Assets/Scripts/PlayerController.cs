using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Movement")]
    public float moveSpeed;
    private Vector2 moveVec;

    private Rigidbody2D rigidbody;

    public PlayerInput playerInput;

    public static InputAction moveAction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        moveVec.x = moveAction.ReadValue<Vector2>().x;
        moveVec.y = moveAction.ReadValue<Vector2>().y;

        rigidbody.velocity = moveVec * moveSpeed;
    }
}
