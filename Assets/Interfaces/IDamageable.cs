using UnityEngine;

// Interfaz para objetos que pueden recibir daño
public interface IDamageable
{
    // Propiedad para la salud del objeto
    float Health { set; get; }

    // Propiedad que indica si el objeto es objetivo (targetable)
    bool Targetable { set; get; }

    // Propiedad que indica si el objeto es invulnerable (invincible)
    bool Invincible { set; get; }

    // Método llamado cuando el objeto recibe daño y se aplica un empuje (knockback)
    void OnHit(float damage, Vector2 knockback);

    // Método llamado cuando el objeto recibe daño sin empuje adicional
    void OnHit(float damage);

    // Método llamado cuando el objeto es destruido
    void OnObjectDestroyed();
}
