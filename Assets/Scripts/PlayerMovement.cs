
using System;
using System.Collections;
using System.Transactions;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    [Header("Rotation")]
    [SerializeField] private Transform cam;
    [SerializeField, Range(0, 1)] private float turnSmoothTime;

    [Header("SphereGizmo")]
    [SerializeField] private float radius;
    [SerializeField] private Vector3 center;
    [SerializeField] private LayerMask layerMask;

    private Collider[] grabbingColliders = new Collider[1];
    private PlayerInput playerMovement;
    private CharacterController character;
    private CrystalTriggerComponent crystalComponent;
    private ParticleSystem slashParticles;

    private Vector2 input;
    private Vector3 direction;

    private float turnSmoothVelocity;
    private float timer;
    private int crystalPicker;
    private bool canGrab = true;


    private void Awake()
    {
        playerMovement = new PlayerInput();
    }
    private void OnEnable()
    {
        playerMovement.Move.Enable();
    }
    private void OnDisable()
    {
        playerMovement.Move.Disable();
    }
    private void Start()
    {
        character = GetComponent<CharacterController>();
        slashParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            character.Move(speed * Time.deltaTime * moveDir.normalized);
        }

        SphereGizmo();
        GrabTimer();
    }
    public void SphereGizmo()
    {
        crystalPicker = Physics.OverlapSphereNonAlloc(transform.position + center, radius, grabbingColliders, layerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = crystalPicker != 0 ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + center, radius);
    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y).normalized;
    }

    public void Grab(InputAction.CallbackContext context)
    {
        if (!context.started) return;


        if (context.started)
        {
            if (canGrab)
            {
                Grabbing();
                canGrab = false;
                timer = 0;
            }
        }
    }
    private void Grabbing()
    {
        slashParticles.Play();
        for (int i = 0; i < crystalPicker; i++)
        {
            crystalComponent = grabbingColliders[i].GetComponent<CrystalTriggerComponent>();
            if (crystalComponent != null)
            {
                crystalComponent.CrystalPicker();
            }
        }
    }
    private void GrabTimer()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            canGrab = true;
        }
    }
}
