using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AbilitiesPerRound : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPassivesTransform;
    [Header("Posibles Pasivas a coger en cada ronda")]
    [SerializeField]
    private GameObject[] pasivasTotales;

    private List<GameObject> pasivasTotalesList;

    [Header("Posicion para spawnear las pasivas")]
    [SerializeField]
    private Vector3 offsetPasiva1;
    [SerializeField]
    private Vector3 offsetPasiva2;
    [SerializeField]
    private Vector3 offsetPasiva3;

    [Header("Objeto Padre de las pasivas de cada ronda")]
    [SerializeField]
    private GameObject parentPasivasEachRound;

    private List<GameObject> pasivasRondaDisponibles;

    public static AbilitiesPerRound Instance;

    void Start()
    {
        MetodoFuncionalidadCrear3PasivasRonda();
    }

    //funcionalidad
    public void MetodoFuncionalidadCrear3PasivasRonda()
    {
        //inicializar lista ronda pasivas
        pasivasRondaDisponibles = new List<GameObject>();

        AñadirPasivasTotalesList();

        // Llamada al método para obtener 3 números sin repetición de pasivasTotales
        pasivasRondaDisponibles = SeleccionarNumeros(pasivasTotalesList, 3);

       

        //spawnear las 3 pasivas en una posicion del array del Vector3
        SpawnArrayVectoresPosicionesPasivas();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);

    }

    private void SpawnArrayVectoresPosicionesPasivas()
    {
        for (int i = 0; i < pasivasRondaDisponibles.Count; i++)
        {
            //activamos pasiva
            pasivasRondaDisponibles[i].gameObject.SetActive(true);
            //dar posicion
            switch (i)
            {
                case 0:
                {
                    pasivasRondaDisponibles[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasiva1;
                    break;
                }
                case 1:
                {
                    pasivasRondaDisponibles[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasiva2;
                    break;
                }
                case 2:
                {
                    pasivasRondaDisponibles[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasiva3;
                    break;
                }

            }
           
        }
    }

    private void AñadirPasivasTotalesList()
    {
        //inicializamos lista
        pasivasTotalesList = new List<GameObject>();
        //añadimos de array pasivasTotales a pasivasTotalesList
        // Puedes hacer lo que necesites con la lista de números seleccionados
        foreach (GameObject pasiva in pasivasTotales)
        {
            GameObject gameObjectPasiva = pasiva;
            pasivasTotalesList.Add(pasiva);
        }
        

    }

    List<GameObject> SeleccionarNumeros(List<GameObject> opciones, int cantidad)
    {
        List<GameObject> numerosSeleccionados = new List<GameObject>();

        // Verificar si hay suficientes opciones para seleccionar
        if (opciones.Count < cantidad)
        {
            Debug.LogWarning("No hay suficientes opciones para seleccionar.");
            return numerosSeleccionados;
        }

        // Obtener una copia de las opciones disponibles
        List<GameObject> opcionesDisponibles = new List<GameObject>(opciones);

        // Seleccionar números sin repetición
        for (int i = 0; i < cantidad; i++)
        {
            // Obtener un índice aleatorio dentro del rango de opciones disponibles
            int indiceAleatorio = Random.Range(0, opcionesDisponibles.Count);

            // Agregar el número seleccionado a la lista
            numerosSeleccionados.Add(opcionesDisponibles[indiceAleatorio]);

            // Eliminar la opción seleccionada para evitar repeticiones
            opcionesDisponibles.RemoveAt(indiceAleatorio);
        }

        return numerosSeleccionados;
    }

    //como se ha cogido la pasiva para la ronda se desactivan y destruyen el resto
    public void PasivaCogidaParaLaRondaDestruirResto()
    {
        foreach (GameObject pasiva in pasivasRondaDisponibles)
        {
            //si existe
            if(pasiva != null)
            {
                pasiva.SetActive(false);
            }
            
        }
    }
}
