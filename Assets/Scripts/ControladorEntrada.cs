using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorEntrada : MonoBehaviour
{
    public Vector2 MovimientoEntrada { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovimientoEntrada = context.ReadValue<Vector2>();
    }
}
