/**
  Este script detecta la colisión con un objeto y notifica a los suscriptores
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notificador3 : MonoBehaviour
{
    /// Delegado para notificar
    public delegate void Mensaje();
    /// Evento para notificar
    public event Mensaje OnTriggerHumanoide1; // Para humanoide tipo 1
    public event Mensaje OnTriggerHumanoide2; // Para humanoide tipo 2

    void OnTriggerEnter(Collider other)
    {
        // ej 3
        if (other.CompareTag("Humanoide_Tipo1"))
        {
            Debug.Log("Cubo tocó Humanoide Tipo 1");
            OnTriggerHumanoide1?.Invoke();
        }
        else if (other.CompareTag("Humanoide_Tipo2"))
        {
            Debug.Log("Cubo tocó Humanoide Tipo 2");
            OnTriggerHumanoide2?.Invoke();
        }
    }
}