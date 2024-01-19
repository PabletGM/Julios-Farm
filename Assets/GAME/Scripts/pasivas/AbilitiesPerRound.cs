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
    private GameObject[] allPassives; 

    private List<GameObject> listAllPasives;

    [Header("Posicion para spawnear las pasivas")]
    [SerializeField]
    private Vector3 offsetPasive1;
    [SerializeField]
    private Vector3 offsetPasive2;
    [SerializeField]
    private Vector3 offsetPasive3;

    [Header("Objeto Padre de las pasivas de cada ronda")]
    [SerializeField]
    private GameObject parentPassivesEachRound;

    private List<GameObject> passivesAvailables;

    public static AbilitiesPerRound Instance;

    void Start()
    {
        spawnRoundPassives();
    }

    //funcionalidad
    public void spawnRoundPassives()
    {
        //inicializar lista ronda pasivas
        passivesAvailables = new List<GameObject>();

        AñadirPasivasTotalesList();

        // Llamada al método para obtener 3 números sin repetición de pasivasTotales
        passivesAvailables = SelectNumbers(listAllPasives, 3);

       

        //spawnear las 3 pasivas en una posicion del array del Vector3
        SpawnPositionPassives();
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

    private void SpawnPositionPassives()
    {
        for (int i = 0; i < passivesAvailables.Count; i++)
        {
            //activamos pasiva
            passivesAvailables[i].gameObject.SetActive(true);
            //dar posicion
            switch (i)
            {
                case 0:
                {
                    passivesAvailables[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasive1;
                    break;
                }
                case 1:
                {
                    passivesAvailables[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasive2;
                    break;
                }
                case 2:
                {
                    passivesAvailables[i].gameObject.transform.position = spawnPassivesTransform.position + offsetPasive3;
                    break;
                }

            }
           
        }

       
    }

    

    private void AñadirPasivasTotalesList()
    {
        //inicializamos lista
        listAllPasives = new List<GameObject>();
        //añadimos de array pasivasTotales a pasivasTotalesList
        // Puedes hacer lo que necesites con la lista de números seleccionados
        foreach (GameObject pasiva in allPassives)
        {
            GameObject gameObjectPasiva = pasiva;
            listAllPasives.Add(pasiva);
        }
        

    }

    List<GameObject> SelectNumbers(List<GameObject> opciones, int cantidad)
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
    public void destroyOtherPassives()
    {
        foreach (GameObject pasiva in passivesAvailables)
        {
            //si existe
            if(pasiva != null)
            {
                pasiva.SetActive(false);
            }
            
        }

        foreach (GameObject pasiva in listAllPasives)
        {
            //si existe
            if (pasiva != null)
            {
                pasiva.SetActive(false);
            }

        }
    }
}
