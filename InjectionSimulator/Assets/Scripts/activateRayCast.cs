using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class activateRayCast : MonoBehaviour
{
    [SerializeField]
    GameObject rayCast;

    [SerializeField]
    InputActionProperty rightTrigger;

    private void Update()
    {
        rayCast.SetActive(rightTrigger.action.ReadValue<float>()> 0.1f);
    }
}
