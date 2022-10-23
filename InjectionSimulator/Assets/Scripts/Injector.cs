using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;

public class Injector : MonoBehaviour
{
    [SerializeField] private GameObject potionInInjector, plunger, potionPrefab, dropVFX;
    [SerializeField] private float fullMedicine, initialSize;
    private bool interactWithPotion, interactWithPatient, fullPotion, completed, attachTotVial;
    private GameManager gameManager;
    private AudioSource audioSource;

    [SerializeField] AudioClip gettingMedicineClip, injectingClip, errorClip;
    [SerializeField] Transform dropVFXattach;
    private GameObject potionCollider;

    private void Start()
    {
        interactWithPatient = false;
        interactWithPotion = false;
        fullPotion = false;
        initialSize = potionInInjector.transform.localScale.y;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    public void activateInjector(ActivateEventArgs arg)
    {
        if (gameManager.currentTask <= 0)
        {
            gameManager.updateCurrentAttemp();
            gameManager.setTipsWindowText("Tips: you shouldn't interact with this item right now!");
            audioSource.PlayOneShot(errorClip);
            return;
        }
        if (!fullPotion)
        {
            if (potionInInjector.transform.localScale.y < fullMedicine)
            {
                if (interactWithPotion) {
                    if (!attachTotVial)
                    {
                        Destroy(potionCollider);
                        potionPrefab.SetActive(true);
                        //potionP.GetComponent<Rigidbody>().
                        attachTotVial = true;
                        gameManager.takeApart();
                    }
                    potionInInjector.transform.localScale += new Vector3(0, 0.01f, 0);
                    plunger.transform.localPosition += new Vector3(.02f, 0, 0);
                    audioSource.PlayOneShot(gettingMedicineClip);
                    gameManager.setTipsWindowText("Tips: getting the medicine. Keep Going!");
                }
                else
                {
                    audioSource.PlayOneShot(errorClip);
                    gameManager.updateCurrentAttemp();
                    gameManager.setTipsWindowText("Tips: you need to get the medicine from the vial");
                }
            }
            else
            {
                fullPotion = true;
                gameManager.filledMedicine();
                potionPrefab.transform.parent = null;
                potionPrefab.AddComponent<BoxCollider>();
                potionPrefab.AddComponent<Rigidbody>();
                potionPrefab.AddComponent<XRGrabInteractable>();
                Destroy(GameObject.Find("Potion").gameObject);
                gameManager.setTipsWindowText("Tips: finished getting the medicine");
            }
        }
        else
        {
            if (interactWithPatient)
            {
                if (potionInInjector.transform.localScale.y >= initialSize)
                {
                    audioSource.PlayOneShot(injectingClip);
                    potionInInjector.transform.localScale -= new Vector3(0, 0.01f, 0);
                    plunger.transform.localPosition -= new Vector3(.02f, 0, 0);
                    gameManager.setTipsWindowText("Tips: injecting the medicine. Keep Going!");
                }
                else
                {
                    gameManager.setTipsWindowText("Tips: finished Injecting");
                    if (!completed)
                    {
                        gameManager.completed();
                        completed = true;
                    }
                }
            }
            else
            {
                if (!completed) {
                    audioSource.PlayOneShot(errorClip);
                    gameManager.updateCurrentAttemp();
                    Instantiate(dropVFX, dropVFXattach.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    gameManager.setTipsWindowText("Tips: you need to inject the patient");
                }
            }
        }
    }

    public void hasBeenSelected(SelectEnterEventArgs arg)
    {
        if (gameManager.currentTask <= 0)
        {
            gameManager.setTipsWindowText("Tips: you shouldn't interact with this item right now!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Potion")
        {
            potionCollider = collision.gameObject;
            interactWithPotion = true;
        }
        if (collision.gameObject.tag == "Patient")
        {
            interactWithPatient = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Potion")
        {
            interactWithPotion = false;
        }
        if (collision.gameObject.tag == "Patient")
        {
            interactWithPatient = false;
        }
    }
}
