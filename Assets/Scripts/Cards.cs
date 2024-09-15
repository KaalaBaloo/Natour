using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Cards : MonoBehaviour
{
    [SerializeField] int puntosVerde = 1; //Los puntos que suma o resta la carta que tiene este script
    [SerializeField] int puntosAmarillo = 20;
    [SerializeField] int puntosAzul = 1;
    [SerializeField] int puntosRojo = 1;

    GameController controller; //Busca al Game Controller general
    DefaultObserverEventHandler observer; //Obtiene los datos del observer de la carta (el script que detecta la imagen AR)

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("detector").GetComponent<GameController>();
        observer = GetComponent<DefaultObserverEventHandler>();
    }

    private void Update()
    {
        if(observer.seen && controller.respuestaReto) //Booleano que indica si esta "viendo" la carta y si se puede responder
        {

            //foreach (Touch touch in Input.touches) //Si detecta pulsar pantalla del movil
            //{
            //    if (touch.phase == TouchPhase.Began)
            //    {
            //        controller.UpdateStatus(puntosVerde, puntosAmarillo, puntosAzul, puntosRojo); //Manda los datos al card detector para actualizar las puntuaciones
            //    }
            //}
            if(Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {

                        controller.UpdateStatus(puntosVerde, puntosAmarillo, puntosAzul, puntosRojo); //Manda los datos al card detector para actualizar las puntuaciones
                    }
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                
                controller.UpdateStatus(puntosVerde, puntosAmarillo, puntosAzul, puntosRojo); //Manda los datos al card detector para actualizar las puntuaciones
            }
        }
      
    }
}
