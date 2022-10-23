using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetIfDropOnGround : MonoBehaviour
{
    [SerializeField] private GameObject resetObject;
    private Vector3 initialPos;
    private Quaternion initialRot;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            GameObject generatedObject = Instantiate(resetObject, initialPos, initialRot);

            if(generatedObject.GetComponent<XRGrabInteractable>().enabled == false)
                Destroy(generatedObject);

            Destroy(gameObject);
        }
    }
}
