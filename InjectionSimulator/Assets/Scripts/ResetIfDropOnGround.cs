using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetIfDropOnGround : MonoBehaviour
{
    [SerializeField] private GameObject resetObject;
    private Vector3 initialPos;
    private Quaternion initialRot;
    private AudioSource fallSFX;
    private GameManager gameManager;

    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;
        fallSFX = GameObject.FindGameObjectWithTag("FallSFX").GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            fallSFX.Play();
            gameManager.updateCurrentAttemp();

            GameObject generatedObject = Instantiate(resetObject, initialPos, initialRot);

            if(generatedObject.GetComponent<XRGrabInteractable>().enabled == false)
                Destroy(generatedObject);

            Destroy(gameObject);
        }
    }
}
