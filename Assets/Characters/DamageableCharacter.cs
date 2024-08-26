using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    // Referencias a objetos en la escena
    public GameObject healthText;
    public bool disableSimulation = false;
    public bool canTurnInvincible = false;
    public float invincibilityTime = 0.25f;

    // Componentes necesarios
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D physicsCollider;
    private Canvas sceneCanvas;

    // Estado del personaje
    private bool isAlive = true;
    private float invincibleTimeElapsed = 0f;

    // Propiedades públicas
    public float Health
    {
        set
        {
            // Cuando la salud disminuye, reproduce la animación de golpe y muestra el daño como texto
            if (value < _health)
            {
                animator.SetTrigger("hit");

                // Genera texto de daño justo encima del personaje
                HealthText healthTextInstance = Instantiate(healthText).GetComponent<HealthText>();
                RectTransform textTransform = healthTextInstance.GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                textTransform.SetParent(sceneCanvas.transform);
                healthTextInstance.textMesh.text = (_health - value).ToString();
            }

            _health = value;

            if (_health <= 0)
            {
                // Si la salud es igual o menor a cero, el personaje está muerto
                animator.SetBool("isAlive", false);
                Targetable = false;

                // Reinicia la escena solo si el jugador muere
                RestartScenePlayerOnly();
            }
        }
        get
        {
            return _health;
        }
    }

    // Propiedades de interfaz
    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;

            if (disableSimulation)
            {
                rb.simulated = false;
            }

            physicsCollider.enabled = value;
        }
    }

    public bool Invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;

            if (_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    // Variables internas
    public float _health = 3;
    public bool _targetable = true;
    public bool _invincible = false;

    // Método llamado al inicio
    public void Start()
    {
        // Obtén referencias a componentes
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();

        // Asegúrate de que el slime esté vivo al inicio del script
        animator.SetBool("isAlive", isAlive);

        // Busca el objeto Canvas en la escena
        sceneCanvas = GameObject.FindObjectOfType<Canvas>();

        // Advertencia si no se encuentra el prefab del texto de salud
        if (healthText == null)
        {
            Debug.LogWarning("El prefab del texto de salud no está configurado en " + gameObject.name);
        }

        // Advertencia si no se encuentra un objeto Canvas en la escena
        if (sceneCanvas == null)
        {
            Debug.LogWarning("No se encontró un objeto Canvas en la escena por " + gameObject.name);
        }
    }

    // Método llamado cuando el personaje recibe daño con knockback
    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible)
        {
            Health -= damage;

            // Aplica fuerza al slime
            // Impulso para fuerzas instantáneas
            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTurnInvincible)
            {
                // Activa la invulnerabilidad y el temporizador
                Invincible = true;
            }
        }
    }

    // Método llamado cuando el personaje recibe daño sin knockback
    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Health -= damage;

            if (canTurnInvincible)
            {
                // Activa la invulnerabilidad y el temporizador
                Invincible = true;
            }
        }
    }

    // Método llamado cuando el objeto es destruido
public void OnObjectDestroyed()
{
    // Comprobar si el objeto actual tiene la etiqueta "Enemy"
    if (CompareTag("Enemy"))
    {
        // Si es un enemigo, elimínalo de la escena
        Destroy(gameObject);
    }
    else if (CompareTag("Boss"))
    {
        // Si es el jefe, carga la escena "Level 03"
        SceneManager.LoadScene("outro");
    }
    else if (CompareTag("Player"))
    {
        // Si es el jugador, reinicia la escena
        RestartScenePlayerOnly();
    }
}

    // Método llamado en cada fixed update
    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }

    // Método para reiniciar la escena solo si el jugador muere
    private void RestartScenePlayerOnly()
    {
        // Comprobar si el objeto actual es el jugador antes de reiniciar la escena
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
