/*
Adapta el movimiento en el ejercicio 10 para que el cubo se mueva hacia la posición de la esfera. 
Debes considerar que el avance no debe estar influenciado por cuánto de lejos o cerca estén los dos objetos
*/
using UnityEngine;

public class CuboPersigueEsfera : MonoBehaviour
{
  [Header("Velocidad de movimiento")]
  public float speed = 5f; // velocidad configurable desde el inspector

  [Header("Referencia a la esfera")]
  public Transform esfera; // arrastra la esfera aquí desde el inspector

  void Update()
  {
    if (esfera == null) return; // prevenir errores si no se asigna

    // 1️⃣ Vector desde el cubo hacia la esfera
    Vector3 direccion = esfera.position - transform.position;

    // 2️⃣ Mantener la altura del cubo (y = constante)
    direccion.y = 0f;

    // 3️⃣ Normalizar para que la magnitud no influya en la velocidad
    Vector3 direccionNormalizada = direccion.normalized;

    // 4️⃣ Mover el cubo hacia la esfera a velocidad constante
    transform.Translate(direccionNormalizada * speed * Time.deltaTime, Space.World);
  }
}
