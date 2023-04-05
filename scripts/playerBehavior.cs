using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    // -1 : empty (default item value)
    private int item1, item2, item3;
    private bool falling;
    private float fallTimer;
    
    public GameObject tray;
    public Transform t;

    private animationHandle anim;
    private inventoryHandle inventory;
    private buttonHandle button;
    private movementHandle movement;
    
    void Start()
    {
        fallTimer = 0f;
        item1 = item2 = item3 = -1;
        falling = false;
        anim = this.GetComponent<animationHandle>();
        movement = this.GetComponent<movementHandle>();
        inventory = this.GetComponent<inventoryHandle>();
        button = this.GetComponent<buttonHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.hasReachedPoint(t))
        {
            movement.stopMoving();
        }
        if(this.transform.position.x <= -2.6)// if in kitchen
        {
            tray.SetActive(false);
            if (button.getTouchedButton(t) != -1)// if a button was touched
            {
                handleAction(button.getTouchedButton(t));
            }
        }
        else
        {
            tray.SetActive(true);
        }
        if (!falling)
        {
            if(!movement.hasReachedPoint(t))
            {
                walk(t);
            }
            else
            {
                stand();
            }
        }
        movement.correctPosition();
        timerProgress();
    }

    void timerProgress()
    {
        if (falling)
        {
            fallTimer += Time.deltaTime;
            if(fallTimer >= 2f)
            {
                falling = false;
                fallTimer = 0f;
            }
        }
    }

    public int getItem1() { return item1; }
    public int getItem2() { return item2; }
    public int getItem3() { return item3; }

    public void removeItem1() { item1 = -1; }
    public void removeItem2() { item2 = -1; }
    public void removeItem3() { item3 = -1; }

    public void setItem1(int val) { item1 = val; }
    public void setItem2(int val) { item2 = val; }
    public void setItem3(int val) { item3 = val; }

    void walk(Transform target)
    {
        if(this.transform.position.x <= -2.6)// if in kitchen
        {
            anim.playAnimation(1);// running animation
        }
        else
        {
            anim.playAnimation(3);// running with tray animation
        }
        movement.moveTowardsPoint(t, false);
    }

    void stand()
    {
        if(this.transform.position.x <= -2.6)// if in kitchen
        {
            anim.playAnimation(0);// stand animation
        }
        else
        {
            anim.playAnimation(2);// stand with tray animation
        }
    }

    public bool canTakeItem()
    {
        return item1 == -1 || item2 == -1 || item3 == -1;
    }

    public void takeItem(int itemIndex)
    {
        if(item1 == -1)
        {
            setItem1(itemIndex);
        }
        else if(item2 == -1)
        {
            setItem2(itemIndex);
        }
        else if(item3 == -1)
        {
            setItem3(itemIndex);
        }
    }

    void clearItems()
    {
        item1 = item2 = item3 = -1;
    }

    void fall()
    {
        falling = true;
        anim.playAnimation(4);// fall animation
        clearItems();// drop all items
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Customer")
        {
            fall();
        }
        else if (other.gameObject.tag == "Wall")
        {
            movement.setSpeed(-1f);
            t.transform.position = this.transform.position;
            movement.stopMoving();
        }
    }

    void handleAction(int action)
    {
        if(action >= 0 && action <= 4)// cook item
        {
            inventory.cook(action);
        }
        else if(action >= 5 && action <= 14)// buy item
        {
            inventory.buy(action);
        }
        else if(action >= 15 && action <= 19 && canTakeItem() && inventory.hasEnough(action - 5, 1))// take item
        {
            inventory.decrementInventory(action - 5, 1);
            takeItem(action - 15);
        }
        if(action == 20)// put items back
        {
            inventory.putItemsBack(item1, item2, item3);
            clearItems();
        }
    }
}
