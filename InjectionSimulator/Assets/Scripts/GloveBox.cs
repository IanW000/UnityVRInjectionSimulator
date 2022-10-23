using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GloveBox : MonoBehaviour
{
    [SerializeField] private GameObject glove, generateGlovePos, cloudParticles;
    private GameManager gameManager;
    private AudioSource audioSource;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }
    public void generateGloves(SelectEnterEventArgs arg)
    {
        Instantiate(glove, generateGlovePos.transform.position, Quaternion.identity);
        Instantiate(cloudParticles, transform.position, Quaternion.identity);
        audioSource.Play();
        gameManager.setTipsWindowText("Interact the glove with your both hands");
    }
}
