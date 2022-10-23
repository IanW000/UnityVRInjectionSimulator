using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Hands;
    [SerializeField] Material gloveMat;
    [SerializeField] TextMeshProUGUI taskWindow;
    [SerializeField] TextMeshProUGUI tipsWindow;

    [SerializeField] Transform[] generatePos;
    [SerializeField] GameObject cloudParticles, vialBase, vialCap, injector, gloveBox;
    private AudioSource audioSource;
    [SerializeField] AudioClip generate, success;
    public int currentTask;
    [SerializeField] InterfaceManager interfaceManager;

    private GameObject[]generateObjects;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTask = 0;
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nStep 1: Take out a glove from the glove box and drop them on the other hand to put them on");
        

        generateObjects= new GameObject[] {vialBase,vialCap,injector,gloveBox};
        for(int i = 0; i < generateObjects.Length; i++)
        {
            GameObject obj = generateObjects[i];
            int randomizeArray = Random.Range(0, i);
            generateObjects[i] = generateObjects[randomizeArray];
            generateObjects[randomizeArray] = obj;
        }
        StartCoroutine(initialize());
    }
    public void wearGloves()
    {
        foreach(GameObject hand in Hands)
        {
            hand.GetComponent<Renderer>().material = gloveMat;
        }
        currentTask++;
        audioSource.PlayOneShot(success);

        setTaskWindowText("Finished Tasks("+currentTask+"/4)\r\nStep 2: Pick up the syringe and attach it to the vial to get the medicine");
        setTipsWindowText("Tips: fullfil the syringe with medicine next");
    }
    public void takeApart()
    {
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nStep 3: Draw medicine from the vial and then take the syringe and vial apart");
    }
    public void filledMedicine()
    {
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nStep 4: Insert the assembled syringe into the patient's arm/deltoid");
    }
    public void completed()
    {
        interfaceManager.gameEnd(5);
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nCongratulations! You finished the injector simulator for " + " times. The game will be reset soonly");
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
    private IEnumerator initialize()
    {
        setTipsWindowText("Tips: please wait until all items being generated");
        yield return new WaitForSeconds(2);
        
        for(int i =0;i<generateObjects.Length;i++)
        {
            audioSource.PlayOneShot(generate);
            instantiateObjects(generateObjects[i], generatePos[i]);
            yield return new WaitForSeconds(2);
        }
        setTipsWindowText("Tips: you should use Grip and Trigger button to interact with the items");
    }
}
