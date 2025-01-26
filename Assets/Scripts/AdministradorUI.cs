using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdministradorUI : MonoBehaviour
{
    [Header("Componentes de UI")]
    [SerializeField] private Slider sliderTiempo;
    [SerializeField] private Text textoTiempo;
    [SerializeField] private Text textoPuntaje;
    [SerializeField] private Button botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private Button botonVolver;
    [SerializeField] private Button botonReiniciar;
    [SerializeField] private Button botonSalir;

    [Header("Configuraciones de tiempo")]
    [SerializeField] private float tiempoTotal = 180f;
    private float tiempoRestante;

    [Header("Sonidos")]
    [SerializeField] private AudioSource musicaFondo;
    [SerializeField] private AudioSource efectoSonidoCorrecto;
    [SerializeField] private AudioSource efectoSonidoError;

    private int basuraClasificada = 0;
    private bool juegoPausado = false;

    private void Start()
    {
        tiempoRestante = tiempoTotal;
        ActualizarTiempoUI();
        ActualizarPuntajeUI();

        botonPausa.onClick.AddListener(TogglePausa);
        botonVolver.onClick.AddListener(TogglePausa);
        botonReiniciar.onClick.AddListener(ReiniciarJuego);
        botonSalir.onClick.AddListener(SalirDelJuego);

        if (musicaFondo != null) musicaFondo.Play();
    }

    private void Update()
    {
        if (!juegoPausado)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinDelJuego();
            }
            ActualizarTiempoUI();
        }
    }

    private void ActualizarTiempoUI()
    {
        if (sliderTiempo != null) sliderTiempo.value = tiempoRestante / tiempoTotal;
        if (textoTiempo != null) textoTiempo.text = $"{(int)tiempoRestante / 60:D2}:{(int)tiempoRestante % 60:D2}";
    }

    public void IncrementarBasuraClasificada()
    {
        basuraClasificada++;
        ActualizarPuntajeUI();
        if (efectoSonidoCorrecto != null) efectoSonidoCorrecto.Play();
    }

    public void ErrorClasificacion()
    {
        if (efectoSonidoError != null) efectoSonidoError.Play();
    }

    private void ActualizarPuntajeUI()
    {
        if (textoPuntaje != null) textoPuntaje.text = $"CLASIFICADO: {basuraClasificada}";
    }

    private void TogglePausa()
    {
        juegoPausado = !juegoPausado;
        Time.timeScale = juegoPausado ? 0 : 1;
        if (menuPausa != null) menuPausa.SetActive(juegoPausado);
    }

    private void ReiniciarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    private void FinDelJuego()
    {
        Debug.Log("¡Tiempo agotado! Fin del juego.");
        SceneManager.LoadScene("FinDelJuego");
    }
}
