/*
Adapta el movimiento en el ejercicio 11 de forma que el cubo avance mirando siempre hacia la esfera, 
independientemente de la orientación de su sistema de referencia. Para ello,
el cubo debe girar de forma que el eje Z positivo apunte hacia la esfera.
Realiza pruebas cambiando la posición de la esfera mediante las teclas awsd
*/
using UnityEngine;

public class CuboPersigueYMiraEsfera : MonoBehaviour
{
  [Header("Velocidad de movimiento")]
  public float speed = 5f;

  [Header("Referencia a la esfera")]
  public Transform esfera; // arrastra la esfera aquí desde el inspector

  void Update()
  {
    if (esfera == null) return;

    // -----------------------------
    // 1️⃣ Rotar el cubo para mirar hacia la esfera
    // -----------------------------
    Vector3 targetPosition = esfera.position;
    targetPosition.y = transform.position.y; // mantener altura constante
    transform.LookAt(targetPosition);

    // -----------------------------
    // 2️⃣ Avanzar hacia la esfera usando su eje Z
    // -----------------------------
    Vector3 direccion = (esfera.position - transform.position).normalized;
    direccion.y = 0f; // asegurar que no suba o baje

    // Movimiento proporcional a speed y deltaTime
    transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
  }
}
