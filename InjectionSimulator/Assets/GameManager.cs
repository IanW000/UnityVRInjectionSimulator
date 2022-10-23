using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Hands;
    [SerializeField] Material gloveMat;
    [SerializeField] TextMeshProUGUI taskWindow;
    [SerializeField] TextMeshProUGUI tipsWindow;

    [SerializeField] Transform vialBasePos,vialCapPos,injectorPos;
    [SerializeField] GameObject cloudParticles, vialBase, vialCap, injector;
    private AudioSource audioSource;
    [SerializeField] AudioClip generate, success;
    public int currentTask;
    [SerializeField] InterfaceManager interfaceManager;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTask = 0;
        setTaskWindowText("Finished Tasks(" + currentTask + "/3)\r\nStep 1: Take out a glove from the glove box and drop them on the other hand to put them on");
        setTipsWindowText("Tips: Use Grip and Trigger button to interact with the items");
    }
    public void wearGloves()
    {
        foreach(GameObject hand in Hands)
        {
            hand.GetComponent<Renderer>().material = gloveMat;
        }
        currentTask++;
        audioSource.PlayOneShot(success);

        audioSource.PlayOneShot(generate);

        instantiateObjects(vialBase, vialBasePos);
        instantiateObjects(vialCap, vialCapPos);
        instantiateObjects(injector, injectorPos);

        setTaskWindowText("Finished Tasks("+currentTask+"/3)\r\nStep 2: Pick up the syringe and attach it to the vial to get the medicine");
        setTipsWindowText("Tips: syringe is on your right");
    }
    public void filledMedicine()
    {
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/3)\r\nStep 3: Insert the assembled syringe into the patient's arm/deltoid");
    }
    public void completed()
    {
        interfaceManager.gameEnd(5);
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/3)\r\nCongratulations! You finished the injector simulator for " + " times. The game will be reset soonly");
    }
    public void setTipsWindowText(string tips)
    {
        tipsWindow.text = tips;
    }
    public void setTaskWindowText(string tips)
    {
        taskWindow.text = tips;
    }
    public void instantiateObjects(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, transform.position, Quaternion.identity);
        Instantiate(cloudParticles, transform.position, Quaternion.identity);
    }
}
