using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffect : MonoBehaviour
{

    private Material _material;
    public Renderer renderComponent;
    private MaterialPropertyBlock materialPropertyBlock;

    [SerializeField]
    protected Color baseColor;

    [SerializeField]
    protected Color damageColor;

    private float hitTimeEffect = 0;
    private float totalHitTimeEffect = 0.25f;


    private void Start()
    {
        //renderComponent = GetComponent<Renderer>();
        _material = renderComponent.material;
        materialPropertyBlock = new MaterialPropertyBlock();
        
    }
    

    public IEnumerator HitEffect() 
    {   
        hitTimeEffect = 0f; 
        float sin = 0f;
        while (sin >= 0f)
        {
            hitTimeEffect += Time.deltaTime;
            float elapsedTime = hitTimeEffect / totalHitTimeEffect;
            sin = Mathf.Sin(elapsedTime); materialPropertyBlock.SetColor("_EmissionColor", Color.Lerp(baseColor, damageColor, sin));
            renderComponent.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }
    }
}
