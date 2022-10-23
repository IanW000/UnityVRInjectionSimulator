using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class activateRayCast : MonoBehaviour
{
    [SerializeField]
    private GameObject rayCast;

    [SerializeField]
    private InputActionProperty rightTrigger;

    private void Update()
    {
        rayCast.SetActive(rightTrigger.action.ReadValue<float>()> 0.1f);
    }
}
