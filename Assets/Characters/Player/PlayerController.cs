using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Propiedad para verificar si el jugador se está moviendo
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                rb.drag = moveDrag;
            }
            else
            {
                rb.drag = stopDrag;
            }
        }
    }

    public float moveSpeed = 1250f;

    // Arrastre cuando el jugador se está moviendo por el nivel
    public float moveDrag = 15f;

    // Arrastre cuando el jugador no puede o está intentando moverse
    public float stopDrag = 25f;

    public bool canAttack = true;
    public string attackAnimName = "swordAttack";

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Collider2D swordCollider;
    Vector2 moveInput = Vector2.zero;

    bool isMoving = false;
    bool canMove = true;

    void Start()
    {
        // Inicialización de componentes
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("canAttack", canAttack);
    }

    void FixedUpdate()
    {
        // Verifica si el jugador puede moverse y hay entrada de movimiento
        if (canMove == true && moveInput != Vector2.zero)
        {
            // Animación de movimiento y añadir velocidad
            // Acelera al jugador mientras se presiona la dirección de movimiento (limitado por el arrastre lineal del rigidbody)
            rb.AddForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

            // Controla si está mirando a la izquierda o a la derecha
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }

    // Obtiene los valores de entrada para el movimiento del jugador
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Reproduce la animación de ataque e intenta infligir daño
    void OnFire()
    {
        if (canAttack)
        {
            animator.SetTrigger(attackAnimName);
        }
    }

    // Bloquea el movimiento del jugador
    void LockMovement()
    {
        canMove = false;
    }

    // Desbloquea el movimiento del jugador
    void UnlockMovement()
    {
        canMove = true;
    }
}
