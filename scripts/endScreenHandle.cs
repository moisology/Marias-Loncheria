using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endScreenHandle : MonoBehaviour
{
    public GameObject flight, beach;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        flight.SetActive(true);
        beach.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= 15f)
        {
            flight.SetActive(false);
            beach.SetActive(true);
        }
        else if(time >= 30f)
        {
            SceneManager.LoadScene("title");
        }
        time += Time.deltaTime;
    }
}
