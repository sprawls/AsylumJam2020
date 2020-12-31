using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSoundOn : MonoBehaviour
{
    public GameObject AudioPlayerToOpen;
    public Powerable powerSource;

    private void Awake()
    {
        powerSource = GetComponent<Powerable>();
        AudioPlayerToOpen.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (powerSource.Powered)
        {
            AudioPlayerToOpen.SetActive(true);
        }
        else
        {
            AudioPlayerToOpen.SetActive(false);
        }
    }
}
