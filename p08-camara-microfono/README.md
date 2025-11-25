# Práctica 08 — Entrada: Cámara y Micrófono en Unity
**Autor:** Álvaro Pérez Ramos  
**Email:** alu0101574042@ull.edu.es  

## Descripción
Esta práctica consiste en utilizar dos componentes de entrada proporcionados por Unity:
- **Micrófono** → grabar y reproducir audio  
- **Cámara Web** → capturar vídeo y tomar fotogramas en PNG  

## Objetivos
1. Utilizar el micrófono para grabar audio mediante `Microphone.Start()`.  
2. Reproducir sonido grabado con `AudioSource`.  
3. Mostrar vídeo de una `WebCamTexture`.  
4. Capturar fotogramas en PNG.  
5. Mostrar dispositivos disponibles en consola.

## Estructura del proyecto
```
p08-camara-microfono/
├── audios/
├── img/
├── src/
│   ├── Camara.cs
│   └── Recoder.cs
└── README.md
```

## Parte 1 — Micrófono
### Configuración
1. Crear un `Empty GameObject` llamado `Recorder`.
2. Añadir un componente `AudioSource`.
3. Asignar el script [Recoder.cs](./src/Recoder.cs).

### Controles
- **R (mantener)** → Grabar  
- **R (soltar)** → Reproducir  

## Parte 2 — Cámara Web
### Configuración
1. Crear un `Plane` y asignarle el **tag `Plano`**.
2. Crear un `Empty GameObject` llamado `CamManager`.
3. Asignar el script [Camara.cs](./src/Camara.cs).

### Controles
| Tecla | Acción |
|-------|---------|
| **S** | Iniciar cámara |
| **P** | Parar cámara |
| **X** | Capturar imagen |

## Ejecucion
### Auidios de ejemplo
[katana](./audios/katana-370403.mp3)
[relaxing-music](./audios/relaxing-music-viking-horn-12-116626.mp3)
[sword-slash](./audios/sword-slash-315218.mp3)
[Camara-microfono.mp4](./img/Camara-Microfono.mp4)

## 🔗 Enlaces útiles
- Microphone: https://docs.unity3d.com/ScriptReference/Microphone.html  
- WebCamTexture: https://docs.unity3d.com/ScriptReference/WebCamTexture.html 