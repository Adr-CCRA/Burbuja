using UnityEngine;

public class LimitarMovimiento : MonoBehaviour
{
    private float limiteIzquierda, limiteDerecha, limiteInferior, limiteSuperior;

    private void Start()
    {
        Camera camara = Camera.main;
        float distanciaZ = transform.position.z - camara.transform.position.z;

        Vector3 esquinaInferiorIzquierda = camara.ViewportToWorldPoint(new Vector3(0, 0, distanciaZ));
        Vector3 esquinaSuperiorDerecha = camara.ViewportToWorldPoint(new Vector3(1, 1, distanciaZ));

        limiteIzquierda = esquinaInferiorIzquierda.x;
        limiteDerecha = esquinaSuperiorDerecha.x;
        limiteInferior = esquinaInferiorIzquierda.y;
        limiteSuperior = esquinaSuperiorDerecha.y;
    }

    private void Update()
    {
        Vector3 posicion = transform.position;

        posicion.x = Mathf.Clamp(posicion.x, limiteIzquierda, limiteDerecha);
        posicion.y = Mathf.Clamp(posicion.y, limiteInferior, limiteSuperior);

        transform.position = posicion;
    }
}
