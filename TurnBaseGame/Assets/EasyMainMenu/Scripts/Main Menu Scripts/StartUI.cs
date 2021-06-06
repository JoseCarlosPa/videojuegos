using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{


    public AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscenaJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
