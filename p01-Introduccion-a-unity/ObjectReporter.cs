using UnityEngine;

public class ObjectReporter : MonoBehaviour
{
    void Start()
    {
        // Buscar todos los objetos activos en la escena (sin ordenar → más rápido)
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            // Ignorar objetos sin tag (Untagged)
            if (obj.CompareTag("Untagged")) continue;

            Debug.Log($"Objeto: {obj.name} | Tag: {obj.tag} | Posición: {obj.transform.position}");
        }
    }
}
