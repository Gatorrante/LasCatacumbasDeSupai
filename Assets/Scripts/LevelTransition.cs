using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Carga un nuevo nivel cuando el objeto con la etiqueta especificada entra en el colisionador desencadenante
public class LevelTransition : MonoBehaviour
{
    // Etiqueta del objeto que se utilizará como objetivo para la transición de nivel (por ejemplo, "Player").
    public string tagTarget = "Player";

    // Nombre del escenario que se cargará al activarse la transición.
    public string sceneToLoad;

    void Start()
    {
        // Mensaje de registro al iniciar el objeto de transición de nivel.
        Debug.Log("Level Transition Start");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Mensaje de registro al detectar una colisión.
        Debug.Log("Collision");

        // Verifica si el objeto que activó la colisión tiene la etiqueta objetivo.
        if (collider.gameObject.tag == tagTarget)
        {
            // El objeto con la etiqueta objetivo entró en la forma del colisionador, así que cambia de nivel.
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        }
    }
}
