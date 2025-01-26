using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ControladorEntrada))]
[RequireComponent(typeof(Animator))]
public class movimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 30f;
    [SerializeField] private float arrastre = 0.9f;

    private Rigidbody2D rb;
    private ControladorEntrada controladorEntrada;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controladorEntrada = GetComponent<ControladorEntrada>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MovimientoP();
        ActualizarAnimaciones();
    }

    private void MovimientoP()
    {
        Vector2 MovimientoEntrada = controladorEntrada.MovimientoEntrada;
        rb.velocity += MovimientoEntrada * velocidadMovimiento * Time.fixedDeltaTime;
        rb.velocity *= arrastre;

        float maxVelocidad = 45f;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocidad);
    }
    public void ModificarVelocidad(float ajustar)
    {
        velocidadMovimiento = Mathf.Max(5f, velocidadMovimiento + ajustar);
    }

    public void ResetearVelocidad()
    {
        velocidadMovimiento = 40f;
    }
    private void ActualizarAnimaciones()
    {
        Vector2 MovimientoEntrada = controladorEntrada.MovimientoEntrada;

        if (MovimientoEntrada.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (MovimientoEntrada.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animator.SetFloat("Velocidad", rb.velocity.magnitude);
    }

    public void Comer()
    {
        animator.SetTrigger("Comer");
    }

    public void Morir()
    {
        animator.SetTrigger("Morir");
        velocidadMovimiento = 0f;
    }
}
