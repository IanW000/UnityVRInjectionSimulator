using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Injector : MonoBehaviour
{
    [SerializeField] private GameObject potionInInjector,plunger;
    [SerializeField] private float fullMedicine, initialSize;
    private bool interactWithPotion,interactWithPatient,fullPotion;
    private GameManager gameManager;
    private AudioSource audioSource;
    [SerializeField] AudioClip gettingMedicineClip, injectingClip,errorClip;

    private bool completed;

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
        if (!fullPotion)
        {
            if (potionInInjector.transform.localScale.y < fullMedicine)
            {
                if (interactWithPotion) { 
                    potionInInjector.transform.localScale += new Vector3(0, 0.01f, 0);
                    plunger.transform.localPosition += new Vector3(.02f, 0, 0);
                    audioSource.PlayOneShot(gettingMedicineClip);
                    gameManager.setTipsWindowText("Tips: Getting the medicine. Keep Going!");
                }
                else
                {
                    audioSource.PlayOneShot(errorClip);
                    gameManager.setTipsWindowText("Tips: You need to get the medicine from the vial next");
                }
            }
            else
            {
                fullPotion = true;
                gameManager.filledMedicine();
                gameManager.setTipsWindowText("Tips: Finished getting the medicine");
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
                    gameManager.setTipsWindowText("Tips: Injecting the medicine. Keep Going!");
                }
                else
                {
                    gameManager.setTipsWindowText("Tips: Finished Injecting");
                    if (!completed)
                    {
                        gameManager.completed();
                        completed = true;
                    }
                }
            }
            else
            {
                audioSource.PlayOneShot(errorClip);
                gameManager.setTipsWindowText("Tips: You need to inject the patient next");
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Potion")
        {
            interactWithPotion = true;
        }
        if (collision.gameObject.tag == "Patient")
        {
            interactWithPatient = true;
        }
        Debug.Log(collision.gameObject.name);
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
