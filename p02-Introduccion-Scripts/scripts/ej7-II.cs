/*
Mapea la tecla H a la función disparo. 
*/
using UnityEngine;
public class DisparoConH : MonoBehaviour
{
  void Update()
  {
    // Detectar si se ha pulsado la tecla mapeada en Input Manager ("FireH")
    if (Input.GetButtonDown("FireH"))
    {
      Disparo();
    }
  }

  void Disparo()
  {
    Debug.Log("¡Disparo ejecutado con la tecla H!");
    // Aquí podrías instanciar un proyectil, reproducir sonido, etc.
  }
}
