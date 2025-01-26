using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Contaminacion : MonoBehaviour
{
     public static Contaminacion instance;

    [Header("Medidor de Contaminación")]
    [SerializeField] private Slider medidorContaminacion;
    [SerializeField] private int maxContaminacion = 25;
    [SerializeField] private int contaminacionActual = 0;
    [SerializeField] private GameObject Perdiste; 

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AumentarContaminacion(int cantidad)
    {
        contaminacionActual += cantidad;
        medidorContaminacion.value = (float)contaminacionActual / maxContaminacion;

        if (contaminacionActual >= maxContaminacion)
        {
            FinDelJuego();
        }
    }

    private void FinDelJuego()
    {
        Debug.Log("¡Juego terminado! La contaminación alcanzó el límite.");
        Perdiste.SetActive(true);
        Time.timeScale = 0;
    }
}
