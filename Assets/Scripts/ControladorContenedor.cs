using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorContenedor : MonoBehaviour
{
    [SerializeField] private GameObject[] contenedores;
    private GameObject activarContenedor;
    [SerializeField] private float activarIntervalos = 5f;
    [SerializeField] private float duracionActivo = 10f;

    private void Start()
    {
        StartCoroutine(Activatecontenedores());
    }

    private IEnumerator Activatecontenedores()
    {
        while (true)
        {
            if (activarContenedor == null)
            {
                int randomIndex = Random.Range(0, contenedores.Length);
                activarContenedor = contenedores[randomIndex];

                EstablecerContenedor(activarContenedor, true);

                yield return new WaitForSeconds(duracionActivo);

                EstablecerContenedor(activarContenedor, false);
                activarContenedor = null;
            }

            yield return new WaitForSeconds(activarIntervalos);
        }
    }

    private void EstablecerContenedor(GameObject contenedor, bool isActive)
    {
        SpriteRenderer renderer = contenedor.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        renderer.color = new Color(color.r, color.g, color.b, isActive ? 1f : 0.5f);
        contenedor.GetComponent<BoxCollider2D>().enabled = isActive;
    }

    public GameObject ObtenerContenedorAct()
    {
        return activarContenedor;
    }
    public bool EsContenedor(GameObject obj)
    {
        return obj.CompareTag("Organicos") || obj.CompareTag("Envases") || obj.CompareTag("Carton");
    }
}
