using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareDecoy : MonoBehaviour
{
    public AudioSource audio;
    private string _playerAudioTag = "PlayerAudio";
    public SphereCollider sphereCollider;

    public AudioClip [] OtherSounds;

    // Start is called before the first frame update
    void Start()
    {
        audio.maxDistance = sphereCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerAudioTag)
        {
            Debug.Log("JumpScare");
            PlayRandomSounds();
        }
    }

    //RandomPlayer
    public void PlayRandomSounds()
    {
        int randNumb = UnityEngine.Random.Range(0, OtherSounds.Length);
   
        audio.PlayOneShot(OtherSounds[randNumb],1);
    }

}
