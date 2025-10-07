/*
Utilizar el eje “Horizontal” para girar el objetivo y que avance siempre en la dirección hacia adelante.
*/
using UnityEngine;

public class MoverYGirar : MonoBehaviour
{
  [Header("Velocidad de movimiento")]
  public float speed = 5f;

  [Header("Velocidad de rotación")]
  public float rotationSpeed = 100f; // grados por segundo

  void Update()
  {
    // -----------------------------
    // 1️⃣ Girar con eje Horizontal
    // -----------------------------
    float horizontal = Input.GetAxis("Horizontal"); // ← → o A/D
    transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

    // -----------------------------
    // 2️⃣ Avanzar siempre hacia adelante (eje Z del transform)
    // -----------------------------
    transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

    // -----------------------------
    // 3️⃣ Debug: dibujar la dirección hacia adelante
    // -----------------------------
    Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);
  }
}
