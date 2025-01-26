using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorContenedor : MonoBehaviour
{
    [SerializeField] private GameObject[] contenedores;
    private GameObject activarContenedor;
    [SerializeField] private float activarIntervalos = 2f;
    [SerializeField] private float duracionActivo = 10f;

    private GameObject ultimoContenedorActivado;
    private int vecesActivadoConsecutivas = 0;
    private const int MAX_ACTIVACIONES_CONSECUTIVAS = 2;

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
                GameObject nuevoContenedor;
                do
                {
                    int randomIndex = Random.Range(0, contenedores.Length);
                    nuevoContenedor = contenedores[randomIndex];
                } while (nuevoContenedor == ultimoContenedorActivado && vecesActivadoConsecutivas >= MAX_ACTIVACIONES_CONSECUTIVAS);

                activarContenedor = nuevoContenedor;

                if (nuevoContenedor == ultimoContenedorActivado)
                {
                    vecesActivadoConsecutivas++;
                }
                else
                {
                    ultimoContenedorActivado = nuevoContenedor;
                    vecesActivadoConsecutivas = 1;
                }

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