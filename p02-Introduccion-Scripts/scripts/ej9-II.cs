/*
Mueve el cubo con las teclas de flecha arriba-abajo, izquierda-derecha a la velocidad 'speed'. 
Cada uno de estos ejes implican desplazamientos en el eje vertical y horizontal respectivamente. 
Mueve la esfera con las teclas W-S (movimiento vertical) y A-D (movimiento horizontal).
*/

using UnityEngine;

public class MovimientoCuboYEsfera : MonoBehaviour
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

    // Movimiento directo sin escalar por deltaTime
    transform.Translate(moveDirection * speed, Space.World);
  }
}
