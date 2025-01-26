using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class clasificacionBasura : MonoBehaviour
{
    [SerializeField] private InventarioJugador inventario;
    [SerializeField] private ControladorContenedor adminContenedor;
    [SerializeField] private AudioSource efectoSonidoCorrecto;
    [SerializeField] private AudioSource efectoSonidoError;

    private GameObject activarContenedor;
    private bool cercaContenedor = false;

    private void Update()
    {
        if (cercaContenedor && Keyboard.current.fKey.wasPressedThisFrame && activarContenedor != null)
        {
            Debug.Log($"Abriendo contenedor: {activarContenedor.name}");
        }

        var basuraSeleccionada = inventario.ObtenerBasuraSeleccionada();

        if (Input.GetMouseButtonDown(1) && basuraSeleccionada != null && activarContenedor != null)
        {
            DepositarBasura(basuraSeleccionada, activarContenedor);
        }
    }

    private void DepositarBasura(itemBasura basura, GameObject contenedor)
    {
        if (contenedor.tag == basura.Tipo)
        {
            Debug.Log($"¡Basura clasificada correctamente en {contenedor.name}!");

            inventario.eliminarBasura(basura);
            inventario.DeseleccionarBasura();
            efectoSonidoCorrecto.Play();
        }
        else
        {
            efectoSonidoError.Play();
            Debug.Log($"Clasificación incorrecta. Penalización aplicada.");
            FindObjectOfType<AdministradorUI>().IncrementarTiempo(10f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (adminContenedor.EsContenedor(collision.gameObject))
        {
            activarContenedor = collision.gameObject;
            cercaContenedor = true;
            Debug.Log($"Cerca del contenedor: {activarContenedor.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == activarContenedor)
        {
            activarContenedor = null;
            cercaContenedor = false;
            Debug.Log("Saliste del contenedor.");
        }
    }

    private void abrirContenedor()
    {
        if (activarContenedor == null) return;

        Debug.Log($"Abriendo contenedor: {activarContenedor.name}");

        List<itemBasura> basura = inventario.obtenerBasuraRecolectada();
        for (int i = basura.Count - 1; i >= 0; i--)
        {
            var item = basura[i];
            if (item.Tipo == activarContenedor.tag)
            {
                Debug.Log($"Basura clasificada correctamente: {item.Nombre}");
                inventario.eliminarBasura(item);
            }
            else
            {
                Debug.Log("Basura incorrecta.");
            }
        }
    }
}
