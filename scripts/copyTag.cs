using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyTag : MonoBehaviour
{
    public GameObject other;

    // Update is called once per frame
    void Update()
    {
        other.GetComponent<nameTag>().setName(this.GetComponent<nameTag>().getName());
    }
}
