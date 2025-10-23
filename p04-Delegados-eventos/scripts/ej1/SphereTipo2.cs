/**
  Este script está a la escucha del notificador
  Cuando el notificador notifica, la esfera se mueve hacia el objeto con tag "Cilindro"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTipo2 : MonoBehaviour
{
    /// Notificador
    public Notificador notificator;
    public float speed = 10.0f;
    Vector3 direction;
    GameObject target;
    bool follow = false;

    // Start is called before the first frame update
    void Start()
    {
        notificator.OnTriggerEsfera += miRespuesta; /// Suscripción al evento
        target = GameObject.FindWithTag("Cilindro_1"); /// Busca el objeto con tag "Cilindro"
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }

    /// Respuesta a la notificación
    void miRespuesta()
    {
        follow = true;
    }
}