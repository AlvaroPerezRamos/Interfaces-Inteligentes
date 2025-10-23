/**
  Este script detecta la colisión con un objeto y notifica a los suscriptores
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notificador2 : MonoBehaviour
{
    /// Delegado para notificar
    public delegate void Mensaje();
    /// Evento para notificar
    public event Mensaje OnTrigger;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cilindro_1")
        {
            OnTrigger?.Invoke();
        }
    }
}