using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrystalTriggerComponent : MonoBehaviour
{
    [NonSerialized] public LayerMask crystalLayer;

    [SerializeField] private UnityEvent action;
    [SerializeField] private ParticleSystem crystalParticles;

    private SceneController sceneController;
    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }
    public void CrystalPicker()
    {
        if(gameObject.layer == crystalLayer)
        {
            action?.Invoke();
        }
        if(gameObject.layer != crystalLayer )
        {
            sceneController.LoadScene("GameOverScene");
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void ActivateParticles()
    {
        var p = Instantiate(crystalParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
