using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameTag : MonoBehaviour
{
    public string customerName;
    public TextMesh tag;
    public GameObject targetPoint;
    public bool isButton;
    // Start is called before the first frame update
    void Start()
    {
        tag = this.GetComponent<TextMesh>();
        tag.text = customerName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isButton)
        {
            lookAtPoint(targetPoint.transform);
        }
    }

    void lookAtPoint(Transform target)
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos - new Vector3(0,0,0.001f), new Vector3(0, 1, 0));
        transform.rotation = rotation * Quaternion.Euler(0, 90, 0);
    }

    public void setName(string name)
    {
        customerName = name;
        tag.text = customerName;
    }

    public string getName() { return customerName; }
}
