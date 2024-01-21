using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveIconController : MonoBehaviour
{
    private int initialNumber = 0;
    public TMP_Text textNumber;
    public void SetIcon(Sprite icon)
    {
        this.gameObject.GetComponent<Image>().sprite = icon;
    }

    public void ModifyText(int number)
    {
        initialNumber += number;
        if(initialNumber!=0)
        {
            textNumber.text = Convert.ToString(initialNumber);
        }
        
    }


}
