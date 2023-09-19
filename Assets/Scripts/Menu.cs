using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Llamamos a los objetos de la flecha e indice
    public GameObject flecha, lista;

    private AudioSource SonidoSeleccion;


    // Indice por defecto en la posición 0
    int indice = 0;
    // Start is called before the first frame update
    void Start()
    {
        Dibujar();
        SonidoSeleccion = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Definimos los botones de nav del menú

        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if(up){
            indice++;
            SonidoSeleccion.Play();
        }
        if(down){
            indice--;
            SonidoSeleccion.Play();
        }

        if(indice > lista.transform.childCount-1) indice = 0;
        else if (indice < 0) indice = lista.transform.childCount-1;

        if (up || down) Dibujar();

        if(Input.GetKeyDown("return")) Accion();
    }

    void Dibujar()
    {
        // La lista se posicionará a través del pivote de sus hijos

        Transform opcion = lista.transform.GetChild(indice);
        flecha.transform.position = opcion.position;
    }

    void Accion()
    {
        // Dependiendo de la seleccion saldremos o iremos a la escena "name"

        Transform opcion = lista.transform.GetChild(indice);
        if (opcion.gameObject.name == "Salir")
        {
            Application.Quit();
        } 
        else 
        {
            SceneManager.LoadScene(opcion.gameObject.name);
        }
        
    }
}
