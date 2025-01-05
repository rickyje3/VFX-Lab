using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class VFXDemo : MonoBehaviour
{
    public float ScaleSize = 1f;
    float lastScaleSize = -1f;
    public VisualEffect LinkedEffect;

    // Start is called before the first frame update
    void Start()
    {
        LinkedEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastScaleSize != ScaleSize)
        {
            lastScaleSize = ScaleSize;

            LinkedEffect.SetFloat("Scale Size", ScaleSize);
        }
    }
}
