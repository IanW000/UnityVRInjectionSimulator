using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Glove : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private GameManager gameManager;
    private bool leftGrab, rightGrab;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void hasBeenSelected(SelectEnterEventArgs arg)
    {
        if (grabInteractable.selectingInteractor.name == "LeftHand")
            leftGrab = true;
        if (grabInteractable.selectingInteractor.name == "RightHand")
            rightGrab = true; 

        if(leftGrab && rightGrab)
        {
            gameManager.wearGloves();
            Destroy(gameObject);
        }
    }
}
