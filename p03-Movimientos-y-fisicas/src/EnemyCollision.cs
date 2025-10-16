using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyCollision : MonoBehaviour
{
    private void Start()
    {
        // Asegura que el rigidbody sea cinemático (no se cae)
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log($"{name} chocó con el jugador: {collision.collider.name}");
        }
    }
}
