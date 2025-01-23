using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ControladorEntrada))]
public class movimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 30f;
    [SerializeField] private float arrastre = 0.9f;

    private Rigidbody2D rb;
    private ControladorEntrada controladorEntrada;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controladorEntrada = GetComponent<ControladorEntrada>();
    }

    private void FixedUpdate()
    {
        MovimientoP();
    }

    private void MovimientoP()
    {
        Vector2 MovimientoEntrada = controladorEntrada.MovimientoEntrada;
        rb.velocity += MovimientoEntrada * velocidadMovimiento * Time.fixedDeltaTime;
        rb.velocity *= arrastre;

        float maxVelocidad = 45f;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocidad);
        Debug.Log(rb.velocity);
    }
}
