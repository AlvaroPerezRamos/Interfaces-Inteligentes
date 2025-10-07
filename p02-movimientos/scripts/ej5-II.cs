/**
  Este script detecta si se ha pulsado el espacio y mueve los objetos a una posición determinada.
  Esa posición es su posición actual más un desplazamiento.
  El desplazamiento está en un componente de tipo variable
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MoverConDesplazamiento : MonoBehaviour
{
  /// Objetos que van a hacer el movimiento
  GameObject _cubo;
  GameObject _cilindro;
  GameObject _esfera;


  // Start is called before the first frame update
  void Start()
  {
    /// Buscamos los objetos por su etiqueta "Cubo", "Cilindro" y "Esfera"
    _cubo = GameObject.FindWithTag("Cubo");
    _cilindro = GameObject.FindWithTag("Cilindro");
    _esfera = GameObject.FindWithTag("Esfera");
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetAxis("Jump") > 0)
    {
      /// Movemos los objetos a su posición actual más un desplazamiento
      Vector3 desplazamientoEsfera = Variables.Object(_esfera).Get<Vector3>("Desplazamiento");
      _esfera.transform.position = _esfera.transform.position + desplazamientoEsfera;
      Vector3 desplazamientoCilindro = Variables.Object(_cilindro).Get<Vector3>("Desplazamiento");
      _cilindro.transform.position = _cilindro.transform.position + desplazamientoCilindro;
      Vector3 desplazamientoCubo = Variables.Object(_cubo).Get<Vector3>("Desplazamiento");
      _cubo.transform.position = _cubo.transform.position + desplazamientoCubo;
    }
  }
}