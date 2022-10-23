using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GloveBox : MonoBehaviour
{
    [SerializeField] private GameObject glove, generateGlovePos, cloudParticles;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void generateGloves(ActivateEventArgs arg)
    {
        Instantiate(glove, generateGlovePos.transform.position, Quaternion.identity);
        Instantiate(cloudParticles, transform.position, Quaternion.identity);
        gameManager.setTipsWindowText("Hold the glove with both hands");
    }
}
