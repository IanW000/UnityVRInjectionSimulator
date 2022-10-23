using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Hands;
    [SerializeField] Material gloveMat;
    [SerializeField] TextMeshProUGUI taskWindow, tipsWindow, lastGamesAttemps, currentAttemps;
    [SerializeField] InterfaceManager interfaceManager;
    [SerializeField] Transform[] generatePos;
    [SerializeField] GameObject cloudParticles, vialBase, vialCap, injector, gloveBox;
    [SerializeField] AudioClip generate, success;

    public int currentTask, attempNum;
    private AudioSource audioSource;
    private GameObject[]generateObjects;

    private void Start()
    {
        attempNum = 0;
        audioSource = GetComponent<AudioSource>();
        currentTask = 0;
        currentAttemps.text = attempNum.ToString();
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nStep 1: Take out a glove from the glove box and drop them on the other hand to put them on");

        string json = System.IO.File.ReadAllText(Application.dataPath + "/saveJson.text");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);

        foreach(int atm in loadedPlayerData.numberOfAttemps)
        {
            lastGamesAttemps.text += atm.ToString() + "  ";
        }

        generateObjects = new GameObject[] {vialBase,vialCap,injector,gloveBox};
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
        string loadedJson = System.IO.File.ReadAllText(Application.dataPath + "/saveJson.text");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(loadedJson);
        loadedPlayerData.numberOfAttemps.Add(attempNum);
        loadedPlayerData.numberOfAttemps.RemoveAt(0);

        string json = JsonUtility.ToJson(loadedPlayerData);
        System.IO.File.WriteAllText(Application.dataPath + "/saveJson.text", json);

        interfaceManager.gameEnd(5);
        audioSource.PlayOneShot(success);
        currentTask++;
        setTaskWindowText("Finished Tasks(" + currentTask + "/4)\r\nCongratulations! You finished the injector simulator. The game will be reset soonly");
    }

    public void setTipsWindowText(string tips)
    {
        tipsWindow.text = tips;
    }

    public void setTaskWindowText(string tips)
    {
        taskWindow.text = tips;
    }

    public void updateCurrentAttemp()
    {
        attempNum++;
        currentAttemps.text = attempNum.ToString();
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

    private class PlayerData
    {
        public List<int> numberOfAttemps;
    }

}
