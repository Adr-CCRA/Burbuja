using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(movimientoJugador))]
public class InventarioJugador : MonoBehaviour
{
    [Header("Inventario")]
    [SerializeField] private Transform transformarBurbuja;
    [SerializeField] private Vector3 baseScale = Vector3.one;
    [SerializeField] private float scaleFactor = 0.1f;
    [SerializeField] private int maxItems = 3;
    [SerializeField] private float impactoPeso = 0.2f;
    [SerializeField] private Text contadorBasura; 

    public List<itemBasura> basuraRecogida = new List<itemBasura>();
    private movimientoJugador movimientoP;
    private float pesoActual = 0f;

    private void Awake()
    {
        movimientoP = GetComponent<movimientoJugador>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        itemBasura itemBasura = collision.GetComponent<itemBasura>();
        if (itemBasura != null && basuraRecogida.Count < maxItems)
        {
            recogerBasura(itemBasura);
        }
    }

    private void recogerBasura(itemBasura itemBasura)
    {
        basuraRecogida.Add(itemBasura);
        pesoActual += itemBasura.Peso;
        movimientoP.ModificarVelocidad(-itemBasura.Peso * impactoPeso);
        ActualizarTamBurbuja();
        ActualizarCantBasura(); 
        Destroy(itemBasura.gameObject);
    }
    private void ActualizarTamBurbuja()
    {
        transformarBurbuja.localScale = baseScale + Vector3.one * (pesoActual * scaleFactor);
    }
    private void ActualizarCantBasura()
    {
        contadorBasura.text = $"Basura: {basuraRecogida.Count}/{maxItems}";
    }
    public void LimpiarInventario()
    {
        basuraRecogida.Clear();
        pesoActual = 0f;
        movimientoP.ResetearVelocidad();
        transformarBurbuja.localScale = baseScale;
        ActualizarCantBasura(); 
    }

    public List<itemBasura> obtenerBasuraRecolectada()
    {
        return basuraRecogida;
    }

    public void eliminarBasura(itemBasura item)
    {
        if (basuraRecogida.Contains(item))
        {
            basuraRecogida.Remove(item);
            pesoActual -= item.Peso;
            movimientoP.ModificarVelocidad(item.Peso * impactoPeso);
            ActualizarTamBurbuja();
            ActualizarCantBasura();
        }
    }
}
