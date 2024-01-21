using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUIManager : MonoBehaviour
{
    public static PassiveUIManager instance;

    [SerializeField] private GameObject passivePrefab;

     List<GameObject> listPasivas;



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
        listPasivas = new List<GameObject>();
    }

    public void AddPassiveIconToCanvas(Sprite icon, bool pasivaRepe)
    {
        //solo se informa de la lista de pasivas

        //si esta repetida
        if(pasivaRepe)
        {
            //cambiar text de pasiva x2
            passivePrefab.GetComponent<PassiveIconController>().ModifyText(1);
        }
        //si es nueva
        else
        {
            //instancia prefab de icono en una posicion 0,0,0 y sin rotacion y al hacerlo parent se ajusta al horizontal layout group automaticamente
            GameObject inst = Instantiate(passivePrefab, Vector3.zero, Quaternion.identity);
           

            //hacemos parent de GameObject Passives
            inst.transform.SetParent(this.gameObject.transform);
            inst.transform.localScale = Vector3.one;

            //llamamos a SetIcon de el propio prefab
            inst.GetComponent<PassiveIconController>().SetIcon(icon);
            //se añade a la lista
            listPasivas.Add(inst);
        }

       
    }

}
