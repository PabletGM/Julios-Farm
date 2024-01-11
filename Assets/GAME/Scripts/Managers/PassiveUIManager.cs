using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUIManager : MonoBehaviour
{
    public static PassiveUIManager instance;

    [SerializeField] private GameObject passivePrefab;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        
        
    }

    public void AddPassiveIconToCanvas(Sprite icon)
    {
        //solo se informa de la lista de pasivas

        //instancia prefab de icono en una posicion 0,0,0 y sin rotacion y al hacerlo parent se ajusta al horizontal layout group automaticamente
        GameObject inst = Instantiate(passivePrefab, Vector3.zero, Quaternion.identity);


        //hacemos parent de GameObject Passives
        inst.transform.parent = this.gameObject.transform;

        //llamamos a SetIcon de el propio prefab
        inst.GetComponent<PassiveIconController>().SetIcon(icon);
    }

}
