using UnityEngine;

public class DetectorSuelo : MonoBehaviour
{
    public float posicionY;
    public float posicionX;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, posicionY, transform.position.z);
    }
}
