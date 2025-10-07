/**
  Este script muestra en pantalla el vector de posición de un objeto.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPosicion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /// Obtenemos el componente Transform del objeto
        Transform ubication = GetComponent<Transform>();
        Debug.Log("Posición: " + ubication.position.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}