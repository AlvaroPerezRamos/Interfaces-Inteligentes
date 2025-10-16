using UnityEngine;

public class ObjectLauncher : MonoBehaviour
{
    [Header("Fuerza de lanzamiento")]
    public float launchForce = 500f;
    public ForceMode forceMode = ForceMode.Force;

    [Header("Selector de objetos")]
    public string throwableTag = "Throwable"; // tag que deben tener los objetos

    // Puedes lanzar hacia adelante desde este objeto (Launcher). Si es null, usa Vector3.forward.
    public Transform launchOrigin;

    void Update()
    {
        // Usa Input.GetKeyDown (si usas el Input System nuevo, activa 'Both' o adapta el script)
        if (Input.GetKeyDown(KeyCode.X))
        {
            LaunchAll();
        }
    }

    void LaunchAll()
    {
        Rigidbody[] rbs;

        // Si hay launchOrigin y quieres lanzar solo los hijos:
        if (launchOrigin != null)
        {
            rbs = launchOrigin.GetComponentsInChildren<Rigidbody>();
        }
        else
        {
            // Busca por tag
            GameObject[] gos = GameObject.FindGameObjectsWithTag(throwableTag);
            rbs = new Rigidbody[gos.Length];
            for (int i = 0; i < gos.Length; i++)
                rbs[i] = gos[i].GetComponent<Rigidbody>();
        }

        foreach (Rigidbody rb in rbs)
        {
            if (rb == null) continue;

            // Limpio velocidad previa (opcional)
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Aplica fuerza: hacia la cámara o hacia adelante del launcher
            Vector3 dir;
            if (launchOrigin != null)
                dir = (launchOrigin.forward + Vector3.up * 0.2f).normalized;
            else
                dir = (Camera.main != null) ? (Camera.main.transform.forward + Vector3.up * 0.2f).normalized : (Vector3.forward + Vector3.up * 0.2f);

            rb.AddForce(dir * launchForce, forceMode);
        }

        Debug.Log("Lanzados " + rbs.Length + " objetos con fuerza " + launchForce);
    }
}
