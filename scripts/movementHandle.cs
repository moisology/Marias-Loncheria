using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementHandle : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public bool hasReachedPoint(Transform target)
    {
        float tx = target.transform.position.x;
        float x = this.transform.position.x;
        float tz = target.transform.position.z;
        float z = this.transform.position.z;
        return (absoluteDif(tx, x) <= 0.01f) && (absoluteDif(tz,z) <= 0.01f);
    }

    public void moveTowardsPoint(Transform target, bool customer)
    {
        lookAtPoint(target);
        var step = movementSpeed * Time.deltaTime;
        if (customer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            adjustSpeed(target);
            rb.MovePosition(transform.position + transform.right * step);
        }
    }

    public void lookAtPoint(Transform target)
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos - new Vector3(0,0,0.001f), new Vector3(0, 1, 0));
        transform.rotation = rotation * Quaternion.Euler(0, -90, 0);
    }

    public void stopMoving()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public bool isMoving()
    {
        return rb.velocity.x > 0f || rb.velocity.z > 0f;
    }

    private float absoluteDif(float a, float b)
    {
        return a > b ? a - b : b - a;
    }

    private bool slowDown(Transform target)
    {
        float tx = target.transform.position.x;
        float x = this.transform.position.x;
        float tz = target.transform.position.z;
        float z = this.transform.position.z;
        return (absoluteDif(tx, x) <= 0.15f) && (absoluteDif(tz, z) <= 0.15f);
    }

    private void adjustSpeed(Transform target)
    {
        if (slowDown(target))
        {
            movementSpeed = 0.25f;
        }
        else
        {
            movementSpeed = 4f;
        }
    }

    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public void correctPosition()
    {
        // correct x position
        if (this.transform.position.x >= 2.6f)
        {
            this.transform.position = new Vector3(2f, 0.575f, this.transform.position.z);
        }
        else if (this.transform.position.x <= -3.56f)
        {
            this.transform.position = new Vector3(-3f, 0.575f, this.transform.position.z);
        }

        // correct z position
        if (this.transform.position.z >= 5f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.575f, 4.5f);
        }
        else if (this.transform.position.z <= -5.15f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.575f, -4.5f);
        }

        // correct y position
        if (this.transform.position.y != 0.575f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.575f, this.transform.position.z);
        }
    }
}
