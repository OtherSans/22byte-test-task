using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private CursorLockMode lockMode;
    void Start()
    {
        Cursor.lockState = lockMode;
    }
}
