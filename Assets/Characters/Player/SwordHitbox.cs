using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    // Parámetros de la espada
    public float swordDamage = 1f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(1, -0.9f, 0);
    public Vector3 faceLeft = new Vector3(-1, -0.9f, 0);

    void Start()
    {
        // Verifica si el Collider de la espada está configurado
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }

    // Manejo de colisiones cuando la espada entra en contacto con otros colliders
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Obtiene la interfaz IDamageable del objeto colisionado
        IDamageable damagableObject = collider.GetComponent<IDamageable>();

        // Verifica si el objeto colisionado es damageable
        if (damagableObject != null)
        {
            // Calcula la dirección entre el personaje y el objeto colisionado
            Vector3 parentPosition = transform.parent.position;

            // El desplazamiento para la detección de colisiones cambia la dirección de donde proviene la fuerza (cerca del jugador)
            Vector2 direction = (collider.transform.position - parentPosition).normalized;

            // La fuerza de knockback va en dirección de swordCollider hacia el collider
            Vector2 knockback = direction * knockbackForce;

            // Después de asegurarse de que el collider tiene un script que implementa IDamagable, se puede ejecutar la implementación de OnHit y pasar nuestro vector de fuerza
            damagableObject.OnHit(swordDamage, knockback);
        }
    }

    // Mantiene el desplazamiento del collider en 0 para que una rotación a la izquierda y una rotación a la derecha tengan la misma distancia desde el transform
    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
