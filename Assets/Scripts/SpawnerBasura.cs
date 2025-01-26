using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBasura : MonoBehaviour
{
    [Header("Prefabs de basura")]
    public GameObject[] prefabsPlastico;
    public GameObject[] prefabsOrganico;
    public GameObject[] prefabsCartonPapel;

    [Header("Configuraciones de Spawn")]
    public float intervaloInicial = 3f;
    public float intervaloMinimo = 0.5f;
    public float reduccionIntervalo = 0.1f;
    public float velocidadAumento = 0.2f;
    public float velocidadMaxima = 5f;

    private float intervaloActual;
    private float velocidadActual;

    private void Start()
    {
        intervaloActual = intervaloInicial;
        velocidadActual = 0.2f;
        StartCoroutine(GenerarBasura());
    }

    private IEnumerator GenerarBasura()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloActual);

            // Seleccionar tipo de basura al azar
            GameObject prefabSeleccionado = SeleccionarBasuraAleatoria();

            // Generar basura en una posición aleatoria
            if (prefabSeleccionado != null)
            {
                Vector2 posicion = new Vector2(
                    Random.Range(-8f, 8f), // Ajusta estos valores al ancho de tu pantalla
                    transform.position.y
                );
                GameObject basura = Instantiate(prefabSeleccionado, posicion, Quaternion.identity);

                // Configurar velocidad de la basura
                itemBasura scriptBasura = basura.GetComponent<itemBasura>();
                if (scriptBasura != null)
                {
                    scriptBasura.velocidad = velocidadActual;
                }
            }

            // Reducir intervalo y aumentar velocidad
            if (intervaloActual > intervaloMinimo)
            {
                intervaloActual -= reduccionIntervalo;
            }

            if (velocidadActual < velocidadMaxima)
            {
                velocidadActual += velocidadAumento;
            }
        }
    }

    private GameObject SeleccionarBasuraAleatoria()
    {
        int tipo = Random.Range(0, 3); // 0 = Plástico, 1 = Orgánico, 2 = CartónPapel
        switch (tipo)
        {
            case 0:
                return prefabsPlastico[Random.Range(0, prefabsPlastico.Length)];
            case 1:
                return prefabsOrganico[Random.Range(0, prefabsOrganico.Length)];
            case 2:
                return prefabsCartonPapel[Random.Range(0, prefabsCartonPapel.Length)];
            default:
                return null;
        }
    }
}
