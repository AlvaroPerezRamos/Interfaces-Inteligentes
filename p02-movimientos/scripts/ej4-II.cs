/**
  Este script muestra en pantalla la distancia entre dos objetos.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarDistancias : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    /// Buscamos los objetos por su etiqueta "Cubo" y "Cilindro"
    GameObject cubo = GameObject.FindWithTag("Cubo");
    GameObject cilindro = GameObject.FindWithTag("Cilindro");
    /// Calculamos la distancia entre los dos objetos
    float distancia = Vector3.Distance(cubo.transform.position, cilindro.transform.position);
    /// Mostramos la distancia en pantalla
    Debug.Log("Distancia entre cubo y cilindro: " + distancia);
  }

  // Update is called once per frame
  void Update()
  {

  }
}