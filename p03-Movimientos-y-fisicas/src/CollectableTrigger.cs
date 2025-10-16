using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollectableTrigger : MonoBehaviour
{
    private void Start()
    {
        // Asegúrate de que sea un trigger
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"El jugador recolectó: {name}");
            // Aquí puedes sumar puntos, reproducir sonidos, etc.
            gameObject.SetActive(false);
        }
    }
}
