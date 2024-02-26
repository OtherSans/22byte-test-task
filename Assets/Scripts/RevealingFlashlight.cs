using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RevealingFlashlight : MonoBehaviour
{
    //Code for RevealShader to work
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;
    void Update()
    {
        if (Mat && SpotLight != null)
        {
            Mat.SetVector("MyLightPosition", SpotLight.transform.position);
            Mat.SetVector("MyLightDirection", -SpotLight.transform.forward);
            Mat.SetFloat("MyLightAngle", SpotLight.spotAngle);
        }
    }
}
