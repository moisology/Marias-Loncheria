using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Button Indexes

// for cooking

// 0: tacos
// 1: torta
// 2: flan
// 3: coffee
// 4: cooked steak

// for buying

// 5:  raw steak
// 6:  tortilla
// 7:  telera
// 8:  egg
// 9:  lechera
// 10: evaporated milk
// 11: sugar
// 12: coffee bean
// 13: milk
// 14: jarrito

// for grabbing

// 15: tacos
// 16: torta
// 17: flan
// 18: coffee
// 19: jarrito

// for removing

// 20: X
public class buttonHandle : MonoBehaviour
{
    public GameObject[] buttons;

    public int getTouchedButton(Transform target)
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                for(int i = 0; i < buttons.Length; i++)
                {
                    if (buttonTouched(i, target))
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }

    bool buttonTouched(int buttonIndex, Transform target)
    {
        float tx = target.transform.position.x;
        float x = buttons[buttonIndex].transform.position.x;
        float tz = target.transform.position.z;
        float z = buttons[buttonIndex].transform.position.z;
        return (absoluteDif(tx, x) <= 0.3f) && (absoluteDif(tz, z) <= 0.3f);
    }

    private float absoluteDif(float a, float b)
    {
        return a > b ? a - b : b - a;
    }

}
