using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ButtonHighlight : MonoBehaviour
{
    private PointerEventData m_PointerEventData;

    [SerializeField] private GameObject ghostImage;
    [SerializeField] GraphicRaycaster m_Raycaster;
    [SerializeField] EventSystem m_EventSystem;

    [HideInInspector] RectTransform canvasRect;
    void Update()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            ghostImage.SetActive(true);
            ghostImage.transform.position = results[0].gameObject.transform.position + new Vector3(43, 0, 0);
        }
        else
            ghostImage.SetActive(false);
    }
}