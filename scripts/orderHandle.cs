using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderHandle : MonoBehaviour
{
    public int characterIndex;
    public GameObject[] menuIcons;

    private bool[] selectedMenuItems = { false, false, false, false, false };
    private float[] menuItemPrices = { 3.00f, 6.00f, 7.00f, 2.00f, 2.00f };
    private float[] tips = { 20f, 8f, 0f, 2f, 2f, 2f, 12f, 0f, 8f, 30f, 6f, 14f, 2f, 8f, 0f, 0f, 10f, 8f };

    private float balance;
    // Menu Item Indexes
    // 0 : tacos
    // 1 : torta
    // 2 : flan
    // 3 : coffee
    // 4 : jarrito

    void Update()
    {
        for(int i = 0; i < 5; i++)
        {
            if (!selectedMenuItems[i])
            {
                menuIcons[i].SetActive(false);
            }
        }
    }

    public void activateMenuIcons(bool activate)
    {
        for(int i = 0; i < 5; i++)
        {
            if(activate && selectedMenuItems[i])
            {
                menuIcons[i].SetActive(true);
            }
            else
            {
                menuIcons[i].SetActive(false);
            }
        }
    }

    public void order()// the value -1 is used as an indicator for a 0% chance of selecting that item/index
    {
        switch (characterIndex)
        {
            case 0:// Dona FLores
                selectMenuItems(2, 2, 1, 0, 2, 3); break;
            case 1:// Dona Gomez
                selectMenuItems(10, 1, 1, 0, 2, 4); break;
            case 2:// Senora Jimenez
                selectMenuItems(-1, -1, 1, -1, -1, 4); break;
            case 3:// Senora Carmen
                selectMenuItems(1, 2, 3, 1, 2, 4); break;
            case 4:// Consuelo
                selectMenuItems(2, -1, 1, 0, -1, 4); break;
            case 5:// La Gaviota
                selectMenuItems(1, 1, 1, 0, 2, 4); break;
            case 6:// La Fresa
                selectMenuItems(-1, 1, 5, -1, 2, 3); break;
            case 7:// La Charra
                selectMenuItems(1, -1, 2, 0, -1, 3); break;
            case 8:// Don Billetes
                selectMenuItems(1, -1, 1, 1, -1, 3); break;
            case 9:// Don Gustavo
                selectMenuItems(2, -1, 1, 1, -1, 3); break;
            case 10:// Senor Pancho
                selectMenuItems(1, 10, 1, 0, 2, 4); break;
            case 11:// Senor Ignacio
                selectMenuItems(1, -1, 2, 1, -1, 4); break;
            case 12:// Martin
                selectMenuItems(1, 3, -1, 0, 2, -1); break;
            case 13:// Facundo
                selectMenuItems(-1, 1, -1, -1, 2, -1); break;
            case 14:// El Guarache
                selectMenuItems(-1, -1, 1, -1, -1, 3); break;
            case 15:// El Tortas
                selectMenuItems(1, 1, 1, 1, 2, 4); break;
            case 16:// El Charro
                selectMenuItems(1, -1, 1, 0, -1, 4); break;
        }
        activateMenuIcons(true);
    }

    public void selectMenuItems(int entreeChance, int dessertChance, int drinkChance, int menuItemIndex1, int menuItemIndex2, int menuItemIndex3)
    {
        System.Random rnd = new System.Random();
        if (entreeChance != -1 && rnd.Next(entreeChance) + 1 == entreeChance) { selectedMenuItems[menuItemIndex1] = true; }
        if (dessertChance != -1 && rnd.Next(dessertChance) + 1 == dessertChance) { selectedMenuItems[menuItemIndex2] = true; }
        if (drinkChance != -1 && rnd.Next(drinkChance) + 1 == drinkChance) { selectedMenuItems[menuItemIndex3] = true; }
    }

    public void takeFood(GameObject player)
    {
        int item1 = player.GetComponent<playerBehavior>().getItem1();
        int item2 = player.GetComponent<playerBehavior>().getItem2();
        int item3 = player.GetComponent<playerBehavior>().getItem3();

        if (item1 != -1 && selectedMenuItems[item1])
        {
            selectedMenuItems[item1] = false;
            player.GetComponent<playerBehavior>().removeItem1();
            balance += menuItemPrices[item1];
        }
        if (item2 != -1 && selectedMenuItems[item2])
        {
            selectedMenuItems[item2] = false;
            player.GetComponent<playerBehavior>().removeItem2();
            balance += menuItemPrices[item2];
        }
        if (item3 != -1 && selectedMenuItems[item3])
        {
            selectedMenuItems[item3] = false;
            player.GetComponent<playerBehavior>().removeItem3();
            balance += menuItemPrices[item3];
        }
    }

    public bool hasBeenServed()
    {
        for (int i = 0; i < 5; i++)
        {
            if (selectedMenuItems[i])
            {
                return false;
            }
        }
        return true;
    }

    public float getBalance()
    {
        return balance + tips[characterIndex];
    }

    public void clearBalance()
    {
        balance = 0f;
    }
}
