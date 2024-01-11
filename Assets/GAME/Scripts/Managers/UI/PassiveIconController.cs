using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveIconController : MonoBehaviour
{
    public void SetIcon(Sprite icon)
    {
        this.gameObject.GetComponent<Image>().sprite = icon;
    }
}
