using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // Etiqueta del objeto que se está buscando, en este caso etiqueta Player
    public string tagTarget = "Player";

    // Lista que almacena los objetos detectados actualmente en la zona.
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    // Detecta cuando un objeto entra en la zona de detección.
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Verifica si el objeto que entró tiene la etiqueta objetivo.
        if (collider.gameObject.tag == tagTarget)
        {
            // Agrega el objeto a la lista de objetos detectados.
            detectedObjs.Add(collider);
        }
    }

    // Detecta cuando un objeto sale de la zona de detección.
    void OnTriggerExit2D(Collider2D collider)
    {
        // Verifica si el objeto que salió tiene la etiqueta objetivo.
        if (collider.gameObject.tag == tagTarget)
        {
            // Remueve el objeto de la lista de objetos detectados.
            detectedObjs.Remove(collider);
        }
    }
}
