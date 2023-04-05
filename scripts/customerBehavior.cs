using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class customerBehavior : MonoBehaviour
{
    public GameObject[] icons;

    // Icons Indexes
    // 0 : money
    // 1 : X (Angry customer leaving)

    public GameObject row1TurningPoint, row2TurningPoint, register, exit;
    public GameObject[] tableTurningPoints;
    public GameObject[] seats;
    public GameObject player;

    private bool entering, waiting, walkingToTable, walkingToTurningPoint, walkingBackToTurningPoint, walkingToRegister, leaving, hasFallen;
    private Transform targetTurningPoint;
    private float waitingTimer, fallTimer;
    private int selectedSeatIndex;

    private movementHandle movement;
    private animationHandle anim;
    private orderHandle order;

    void Start()
    {
        movement = this.GetComponent<movementHandle>();
        anim = this.GetComponent<animationHandle>();
        order = this.GetComponent<orderHandle>();
        resetCharacter();
    }

    // order of events
    //
    // 1) enter
    // 2) walk to table
    // 3) order
    // 4) wait
    // 5) leave tip
    // 6) walk back to turning point
    // 7) walk to register
    // 8) leave

    void Update()
    {
        if (!hasFallen)
        {
            if (entering)
            {
                selectTurningRow();
            }
            else if (walkingToTurningPoint)
            {
                walk(targetTurningPoint.transform, ref walkingToTurningPoint, ref walkingToTable);
            }
            else if (walkingToTable)
            {
                anim.playAnimation(1);// running animation
                if (!movement.hasReachedPoint(tableTurningPoints[selectedSeatIndex].transform))
                {
                    movement.moveTowardsPoint(tableTurningPoints[selectedSeatIndex].transform, true);
                }
                else 
                {
                    this.gameObject.tag = "Waiting";
                    movement.stopMoving();
                    takeSeat(); 
                    order.order();
                }
            }
            else if (waiting)
            {
                anim.playAnimation(2);// sitting animation
            }
            else if (walkingBackToTurningPoint)
            {
                if (order.hasBeenServed())
                {
                    walk(targetTurningPoint.transform, ref walkingBackToTurningPoint, ref walkingToRegister);
                }
                else
                {
                    walk(targetTurningPoint.transform, ref walkingBackToTurningPoint, ref leaving);
                }
            }
            else if (walkingToRegister)
            {
                walk(register.transform, ref walkingToRegister, ref leaving);
            }
            else if (leaving)
            {
                this.anim.playAnimation(1);// running animation
                if (!movement.hasReachedPoint(exit.transform))
                {
                    movement.moveTowardsPoint(exit.transform, true);
                }
                else
                {
                    movement.stopMoving();
                    resetCharacter();
                    this.gameObject.SetActive(false); // leave scene
                }
            }
        }
        timersProgress();
    }

    void timersProgress()
    {
        // wait timer progress
        if (waiting)
        {
            waitingTimer += Time.deltaTime;
            if (order.hasBeenServed())// has been served
            {
                clearIcons();
                waitingTimer = 30f;
            }
            if (waitingTimer >= 30f)// waited too long
            {
                clearIcons();
                if (!order.hasBeenServed())
                {
                    this.gameObject.tag = "Customer";
                    icons[1].SetActive(true);// get angry
                }
                else
                {
                    icons[0].SetActive(true);
                    player.GetComponent<inventoryHandle>().takeMoney(order.getBalance());// pay
                    order.clearBalance();
                }
                seats[selectedSeatIndex].tag = "Available";
                waiting = false;
                walkingBackToTurningPoint = true;
                this.transform.position = tableTurningPoints[selectedSeatIndex].transform.position;
            }
        }

        // fall timer progress
        if (hasFallen)
        {
            fallTimer += Time.deltaTime;
            if (fallTimer >= 2f)
            {
                hasFallen = false;
                if (walkingToTurningPoint) { leaving = true; walkingToTurningPoint = false; }
                if (walkingToTable) { walkingBackToTurningPoint = true; walkingToTable = false; }
            }
        }
    }

    void resetCharacter()
    {
        clearIcons();
        selectedSeatIndex = -1;
        entering = true;
        waiting = walkingToTurningPoint = walkingToTable = leaving = hasFallen = false;
        waitingTimer = fallTimer = 0f;
        this.gameObject.tag = "Customer";
    }

    void clearIcons()
    {
        icons[0].SetActive(false);
        icons[1].SetActive(false);
        order.activateMenuIcons(false);
    }

    void selectTurningRow()
    {
        if(selectedSeatIndex == -1){ selectedSeatIndex = getFirstAvailableSeat(); }
        else
        {
            walkingToTurningPoint = true;
            entering = false;
            if (selectedSeatIndex < 4) { targetTurningPoint = row1TurningPoint.transform; return; }
            targetTurningPoint = row2TurningPoint.transform;
        }
    }

    int getFirstAvailableSeat()
    {
        for (int i = 0; i < 8; i++)
        {
            if (seats[i].tag == "Available")
            {
                seats[i].tag = "Taken";
                return i;
            }
        }
        return -1;
    }

    void walk(Transform target, ref bool turnOff, ref bool turnOn)
    {
        anim.playAnimation(1);// running animation
        if (!movement.hasReachedPoint(target))
        {
            movement.moveTowardsPoint(target, true);
        }
        else
        {
            movement.stopMoving();
            turnOff = false;
            turnOn = true;
        }
    }

    void takeSeat()
    {
        this.transform.position = seats[selectedSeatIndex].transform.position;
        movement.lookAtPoint(seats[selectedSeatIndex].transform);
        seats[selectedSeatIndex].tag = "Taken";
        walkingToTable = false;
        waiting = true;
    }

    void fall()
    {
        hasFallen = true;
        anim.playAnimation(3); // fall animation
        seats[selectedSeatIndex].tag = "Available";
        icons[1].SetActive(true);// get angry
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (waiting)
            {
                order.takeFood(other.gameObject);
            }
            else if(walkingToTurningPoint || walkingToTable)
            {
                fall();
            }
        }
    }

}
