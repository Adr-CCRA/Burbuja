using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(movimientoJugador))]
public class InventarioJugador : MonoBehaviour
{
    [Header("Inventario")]
    [SerializeField] private Transform transformarBurbuja;
    [SerializeField] private Vector3 baseScale = Vector3.one;
    [SerializeField] private float scaleFactor = 0.03f;
    [SerializeField] private int maxItems = 5;
    [SerializeField] private float impactoPeso = 1f;
    [SerializeField] private Text contadorBasura; 
    [SerializeField] private Transform inventarioUI;
    [SerializeField] private GameObject prefabSlot;

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
            movimientoP.Comer();
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
        GameObject nuevoSlot = Instantiate(prefabSlot, inventarioUI);
        Image spriteHolder = nuevoSlot.transform.Find("SpriteBasura").GetComponent<Image>();
        spriteHolder.sprite = itemBasura.GetComponent<SpriteRenderer>().sprite;
        nuevoSlot.GetComponent<Button>().onClick.AddListener(() => OnClickBasura(itemBasura));

        itemBasura.gameObject.SetActive(false);
    }
    private void ActualizarTamBurbuja()
    {
        transformarBurbuja.localScale = baseScale + Vector3.one * (pesoActual * scaleFactor);
    }
    private void ActualizarCantBasura()
    {
        contadorBasura.text = $"BASURA: {basuraRecogida.Count}/{maxItems}";
    }
    public void LimpiarInventario()
    {
        basuraRecogida.Clear();
        pesoActual = 0f;
        movimientoP.ResetearVelocidad();
        transformarBurbuja.localScale = baseScale;
        ActualizarCantBasura(); 
        foreach (Transform slot in inventarioUI)
        {
            Destroy(slot.gameObject);
        }
    }

    public List<itemBasura> obtenerBasuraRecolectada()
    {
        return basuraRecogida;
    }

    public void eliminarBasura(itemBasura item)
    {
        if (basuraRecogida.Contains(item))
        {
            int index = basuraRecogida.IndexOf(item);
            basuraRecogida.RemoveAt(index);
            pesoActual -= item.Peso;
            movimientoP.ModificarVelocidad(item.Peso * impactoPeso);
            Destroy(inventarioUI.GetChild(index).gameObject);

            ActualizarTamBurbuja();
            ActualizarCantBasura();
        }
    }

    private itemBasura basuraSeleccionada;

    public void SeleccionarBasura(itemBasura basura)
    {
        basuraSeleccionada = basura;
        Debug.Log($"Basura seleccionada: {basura.Tipo}");
    }

    public itemBasura ObtenerBasuraSeleccionada()
    {
        return basuraSeleccionada;
    }

    public void DeseleccionarBasura()
    {
        basuraSeleccionada = null;
    }

    public void OnClickBasura(itemBasura basura)
    {
        SeleccionarBasura(basura);
    }
}
