using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;

public class GameController : MonoBehaviour
{
    Slider _medioAmbiente; //Slider visual de valores en HUD
    Slider _dinero;
    Slider _bienestar;
    Slider _comunidad;
    [SerializeField] int puntosVerde = 5; //Puntos globales (puntos del grupo)
    [SerializeField] int puntosAmarillo = 5;
    [SerializeField] int puntosAzul = 5;
    [SerializeField] int puntosRojo = 5;

    [SerializeField] UnityEngine.UI.Image image; //Imagen del reto
    [SerializeField] UnityEngine.UI.Image background; //Fondo del reto
    [SerializeField] TMP_Text text; //Texto del reto

    [SerializeField] Sprite[] retosImages; //Imagenes de los retos
    [SerializeField] string[] retosTexto; //El reto, texto que explica el reto
    bool newReto = true; //Hay que generar un nuevo reto?
    bool verReto = false; //Se está mostrando el reto por pantalla?
    public bool respuestaReto = false; //Se puede responder (leer la carta) al reto en este momento?


    void Start()
    {
        _medioAmbiente = GameObject.FindGameObjectWithTag("verde").GetComponent<Slider>(); //Busca el slider
        _medioAmbiente.value = puntosVerde; //Poner los sliders en los valores iniciales
        _dinero = GameObject.FindGameObjectWithTag("amarillo").GetComponent<Slider>();
        _dinero.value = puntosAmarillo;
        _bienestar = GameObject.FindGameObjectWithTag("azul").GetComponent<Slider>();
        _bienestar.value = puntosAzul;
        _comunidad = GameObject.FindGameObjectWithTag("rojo").GetComponent<Slider>();
        _comunidad.value = puntosRojo;

        newReto = true;
        verReto = false;
        respuestaReto = false;
    }

    private void Update()
    {
        if (newReto) //Genera un reto y lo muestra por pantalla
        {
            newReto = false;
            int random = Random.Range(0, retosTexto.Length);
            image.sprite = retosImages[random];
            text.text = retosTexto[random];
            image.enabled = true;
            background.enabled = true;
            text.enabled = true;
            StartCoroutine(WaitSecond()); //Espera antes de permitir respuestas para que no se superpongan los inputs
        }

        if ((verReto && Input.GetButtonDown("Jump")) || (verReto && Input.touchCount > 0)) //Añadir inputs de movil - Espera input para mostrar camara
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                // Verifica si el toque actual es un toque de inicio
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    
                    verReto = false;
                    image.enabled = false;
                    background.enabled = false;
                    text.enabled = false;
                    respuestaReto = true;
                }
            }
            verReto = false;
            image.enabled = false;
            background.enabled = false;
            text.enabled = false;
            respuestaReto = true; //Aparece la camara para escanear la respuesta
        }

    }

    public void UpdateStatus(int verdes, int amarillos, int azules, int rojos) //Recibe los puntos de la carta "escaneada" y actualiza las puntuaciones
    {
        puntosVerde += verdes;
        _medioAmbiente.value = puntosVerde;// aumenta el slider
        puntosAmarillo += amarillos;
        _dinero.value = puntosAmarillo;// aumenta el slider
        puntosAzul += azules;
        _bienestar.value = puntosAzul;// aumenta el slider
        puntosRojo += rojos;
        _comunidad.value = puntosRojo;// aumenta el slider

        respuestaReto = false; //No permite más respuestas
        newReto = true; // Genera un nuevo reto
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(0.6f);
        verReto = true;
    }

}
