using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBasura : MonoBehaviour
{
    public string Nombre = "";
    public float Peso = 1f;
    public string Tipo = "";
    public float velocidad;
    private bool enSuelo = false;

    private void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisión detectada con: " + other.name); // Debug para verificar colisión

        if (other.CompareTag("Piso") && !enSuelo)
        {
            enSuelo = true; // Marcar como en el suelo para evitar duplicados

            // Cambiar el estado del Trigger
            var collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.isTrigger = false;
                Debug.Log("IsTrigger desactivado para: " + gameObject.name);
            }

            // Aumentar la contaminación
            Contaminacion.instance.AumentarContaminacion(1);
            Debug.Log("Contaminación aumentada");
        }
    }
}
