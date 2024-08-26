using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    // Parámetros del slime
    public float damage = 1;
    public float knockbackForce = 20f;
    public float moveSpeed = 500f;

    // Componentes necesarios
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    DamageableCharacter damagableCharacter;

    void Start()
    {
        // Inicialización de componentes
        rb = GetComponent<Rigidbody2D>();
        damagableCharacter = GetComponent<DamageableCharacter>();
    }

    void FixedUpdate()
    {
        // Verifica si el slime es objetivo y hay objetos detectados en la zona
        if (damagableCharacter.Targetable && detectionZone.detectedObjs.Count > 0)
        {
            // Calcula la dirección hacia el objeto detectado
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            // Aplica fuerza para mover alrededor del objeto
            rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Manejo de colisiones para aplicar knockback
    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        // Verifica si el objeto colisionado es damageable
        if (damageable != null)
        {
            // Calcula la dirección hacia el objeto colisionado
            Vector2 direction = (collider.transform.position - transform.position).normalized;

            // Calcula el vector de knockback
            Vector2 knockback = direction * knockbackForce;

            // Llama al método OnHit del objeto damageable para aplicar daño y knockback
            damageable.OnHit(damage, knockback);
        }
    }
}
