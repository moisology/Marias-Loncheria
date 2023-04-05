using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customerHandle : MonoBehaviour
{
    public GameObject[] seats;
    public GameObject[] customers;

    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        clearAll();
    }

    // Update is called once per frame
    void Update()
    {
        if (seatAvailable() && t >= 5f)
        {
            activateCustomer(getRandNum());
        }
        timerProgress();
    }

    void timerProgress()
    {
        t += Time.deltaTime;
        if(t > 5.02f)
        {
            t = 0f;
        }
    }

    bool seatAvailable()
    {
        for(int i = 0; i < seats.Length; i++)
        {
            if(seats[i].gameObject.tag == "Available")
            {
                return true;
            }
        }
        return false;
    }

    void activateCustomer(int index)
    {
        if (!customers[index].gameObject.activeSelf)
        {
            customers[index].SetActive(true);
        }
    }

    void clearAll()
    {
        for(int i = 0; i < customers.Length; i++)
        {
            customers[i].gameObject.SetActive(false);
        }
    }

    int getRandNum()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(customers.Length);
    }
}
