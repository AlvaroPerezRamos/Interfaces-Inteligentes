using UnityEngine;
using System.IO;

public class Camara : MonoBehaviour
{
    private Material tvMaterial;
    private WebCamTexture webcamTexture;
    private string savePath;
    private int captureCounter = 1;

    void Start()
    {
        GameObject plane = GameObject.FindWithTag("Plano");

        if (plane == null)
        {
            Debug.LogError("No se encontró un objeto con el tag 'Plano'.");
            return;
        }

        tvMaterial = plane.GetComponent<Renderer>().material;

        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogError("No se detectaron cámaras.");
            return;
        }

        foreach (var cam in WebCamTexture.devices)
            Debug.Log("Cámara encontrada: " + cam.name);

        string selectedCamera = WebCamTexture.devices[0].name;
        webcamTexture = new WebCamTexture(selectedCamera);
        Debug.Log("Cámara seleccionada: " + selectedCamera);

        savePath = Application.persistentDataPath;
        Debug.Log("Las imágenes se guardarán en: " + savePath);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            tvMaterial.mainTexture = webcamTexture;
            webcamTexture.Play();
            Debug.Log("Captura de video iniciada.");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            webcamTexture.Stop();
            Debug.Log("Captura de video detenida.");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (webcamTexture.isPlaying)
            {
                Texture2D snapshot = new Texture2D(webcamTexture.width, webcamTexture.height);
                snapshot.SetPixels(webcamTexture.GetPixels());
                snapshot.Apply();

                string fileName = Path.Combine(savePath, "Capture_" + captureCounter + ".png");
                File.WriteAllBytes(fileName, snapshot.EncodeToPNG());
                captureCounter++;

                Debug.Log("Imagen guardada en: " + fileName);
            }
            else
            {
                Debug.LogWarning("No se puede capturar imagen: la cámara no está activa.");
            }
        }
    }
}
