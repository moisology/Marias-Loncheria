using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicHandle : MonoBehaviour
{
    public AudioSource[] music = new AudioSource[1];
    bool playing;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playing)
        {
            music[0].Play();
            playing = true;
        }
        else if(time >= 192f)
        {
            time = 0f;
            playing = false;
        }
        time += Time.deltaTime;
    }
}
