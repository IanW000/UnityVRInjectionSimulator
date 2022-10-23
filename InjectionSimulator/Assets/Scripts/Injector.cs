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
    // Start is called before the first frame update

    private void Start()
    {
        interactWithPatient = false;
        interactWithPotion = false;
        fullPotion = false;
        initialSize = potionInInjector.transform.localScale.y;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
                    gameManager.setTipsWindowText("Tips: Getting the medicine");
                }
                else
                {
                    gameManager.setTipsWindowText("Tips: You need to put the needle in the vial to get the potion");
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
                    potionInInjector.transform.localScale -= new Vector3(0, 0.01f, 0);
                    plunger.transform.localPosition -= new Vector3(.02f, 0, 0);
                    gameManager.setTipsWindowText("Tips: Injecting the medicine");
                }
                else
                {
                    gameManager.setTipsWindowText("Tips: Finished Injecting");
                }
                    
            }
            else
            {
                gameManager.setTipsWindowText("Tips: You need to find a target for the syringe");
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
