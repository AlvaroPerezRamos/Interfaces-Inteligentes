/**
  Este script detecta la colisión con un objeto y notifica a los suscriptores
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notificador : MonoBehaviour
{
    /// Delegado para notificar
    public delegate void Mensaje();
    public event System.Action OnAproximacionObjetoReferencia1;
    public event System.Action OnAproximacionObjetoReferencia2;

    void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Cilindro_Referencia_1"))
        {
            OnAproximacionObjetoReferencia1?.Invoke();
        }
    else if (other.CompareTag("Cilindro_Referencia_2"))
        {
            OnAproximacionObjetoReferencia2?.Invoke();
        }
    }
}