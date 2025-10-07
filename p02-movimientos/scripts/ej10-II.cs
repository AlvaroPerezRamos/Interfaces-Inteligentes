/*
Adapta el movimiento en el ejercicio 9 para que sea proporcional al tiempo transcurrido durante la generación del frame
*/
using UnityEngine;

public class MovimientoProporcionalATiempo : MonoBehaviour
{
  public float speed = 5f; // Velocidad configurable desde el inspector

  void Update()
  {
    float movHorizontal = 0f;
    float movVertical = 0f;

    if (CompareTag("Cubo"))
    {
      movHorizontal = Input.GetAxis("Horizontal");
      movVertical = Input.GetAxis("Vertical");
    }

    if (CompareTag("Esfera"))
    {
      if (Input.GetKey(KeyCode.W)) movVertical = 1f;
      if (Input.GetKey(KeyCode.S)) movVertical = -1f;
      if (Input.GetKey(KeyCode.D)) movHorizontal = 1f;
      if (Input.GetKey(KeyCode.A)) movHorizontal = -1f;
    }

    Vector3 moveDirection = new Vector3(movHorizontal, 0f, movVertical);

    if (moveDirection.magnitude > 1f)
      moveDirection.Normalize();

    // Movimiento escalado por deltaTime para ser independiente del frame rate
    transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
  }
}
