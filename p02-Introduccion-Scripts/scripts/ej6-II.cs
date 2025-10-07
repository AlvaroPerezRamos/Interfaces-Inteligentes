/*
  Agrega un campo velocidad a un cubo y asígnale un valor que se pueda cambiar en el inspector 
  de objetos. Muestra la consola el resultado de multiplicar la velocidad por el valor del eje 
  vertical y por el valor del eje horizontal cada vez que se pulsan las teclas flecha arriba-abajo 
  ó flecha izquierda-derecha. El mensaje debe comenzar por el nombre de la flecha pulsada. 
*/
using UnityEngine;

public class MovimientoConVelocidad : MonoBehaviour
{
  // Velocidad configurable desde el inspector
  public float velocidad = 5f;

  void Update()
  {
    // Leer ejes
    float ejeVertical = Input.GetAxis("Vertical");     // Flechas ↑ ↓ o W/S
    float ejeHorizontal = Input.GetAxis("Horizontal"); // Flechas ← → o A/D

    // Detectar flechas y mostrar el cálculo en consola

    if (Input.GetKey(KeyCode.UpArrow))
    {
      float resultado = velocidad * ejeVertical;
      Debug.Log("Flecha Arriba → Resultado: " + resultado);
    }

    if (Input.GetKey(KeyCode.DownArrow))
    {
      float resultado = velocidad * ejeVertical;
      Debug.Log("Flecha Abajo → Resultado: " + resultado);
    }

    if (Input.GetKey(KeyCode.LeftArrow))
    {
      float resultado = velocidad * ejeHorizontal;
      Debug.Log("Flecha Izquierda → Resultado: " + resultado);
    }

    if (Input.GetKey(KeyCode.RightArrow))
    {
      float resultado = velocidad * ejeHorizontal;
      Debug.Log("Flecha Derecha → Resultado: " + resultado);
    }
  }
}
