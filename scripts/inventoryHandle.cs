using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryHandle : MonoBehaviour
{
    // Inventory Indexes
    // 0  : raw steak
    // 1  : cooked steak
    // 2  : tortilla
    // 3  : telera
    // 4  : egg
    // 5  : lechera
    // 6  : evaporated milk
    // 7  : sugar
    // 8  : coffee bean
    // 9  : milk
    // 10 : taco
    // 11 : torta
    // 12 : flan
    // 13 : coffee
    // 14 : jarrito
    // 15 : money

    public GameObject[] inventories;
    int[] inventory = {30,30,30,30,30,30,30,256,30,64,30,30,30,30,30};
    float[] prices = { 0.75f, 0.05f, 0.50f, 0.60f, 0.50f, 0.50f, 0.50f, 0.67f, 2.00f, 1.15f };
    float money = 100.00f;

    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            updateInventory(i);
        }
        updateMoney();
    }

    void updateMoney()
    {
        inventories[15].GetComponent<nameTag>().setName("$" + System.Math.Round(money, 2).ToString() + "/ $3000");
    }

    void updateInventory(int index)
    {
        inventories[index].GetComponent<nameTag>().setName(getInventoryCount(index).ToString());
    }

    public int getInventoryCount(int inventoryIndex)
    {
        return inventory[inventoryIndex];
    }

    public float getPrice(int inventoryIndex)
    {
        return prices[inventoryIndex];
    }

    public float getMoneyCount()
    {
        return money;
    }

    public void decrementInventory(int index, int amount)
    {
        if(index != -1)
        {
            inventory[index] -= amount;
            updateInventory(index);
        }
    }

    public void incrementInventory(int index, int amount)
    {
        if(index != -1)
        {
            inventory[index] += amount;
            updateInventory(index);
        }
    }

    public void putItemsBack(int item1, int item2, int item3)
    {
        if(item1 != -1)
        {
            incrementInventory(item1 + 10, 1);
        }
        if(item2 != -1)
        {
            incrementInventory(item2 + 10, 1);
        }
        if(item3 != -1)
        {
            incrementInventory(item3 + 10, 1);
        }
    }

    public void cook(int recipeIndex)
    {
        switch (recipeIndex)
        {
            case 0: // 3 tacos
                if (hasEnough(1, 3) && hasEnough(2, 3) && !isSlotFull(10,1))
                {
                    decrementInventory(1, 3);// cooked steak
                    decrementInventory(2, 3);// tortilla
                    incrementInventory(10, 1);// taco
                } 
                break;
            case 1: // torta
                if(hasEnough(1,3) && hasEnough(3, 1) && !isSlotFull(11, 1))
                {
                    decrementInventory(1, 3);// cooked steak
                    decrementInventory(3, 1);// telera
                    incrementInventory(11, 1);// torta
                }
                break;
            case 2: // flan
                if(hasEnough(4,1) && hasEnough(5,1) && hasEnough(6,1) && hasEnough(7, 16) && !isSlotFull(12, 1))
                {
                    decrementInventory(4, 1);// egg
                    decrementInventory(5, 1);// lechera
                    decrementInventory(6, 1);// evaporated milk
                    decrementInventory(7, 16);// sugar
                    incrementInventory(12, 1);// flan
                }
                break;
            case 3: // coffee
                if(hasEnough(8,1) && hasEnough(9, 4) && !isSlotFull(13, 1))
                {
                    decrementInventory(8, 1);// coffee bean
                    decrementInventory(9, 4);// milk
                    incrementInventory(13, 1);// coffee
                }
                break;
            case 4: // cooked steak
                if (hasEnough(0, 1) && !isSlotFull(1, 1))
                {
                    decrementInventory(0, 1);// raw steak
                    incrementInventory(1, 1);// cooked steak
                }
                break;
        }
    }

    public bool hasEnough(int index, int amount)
    {
        return inventory[index] - amount >= 0;
    }

    public void buy(int action)
    {
        if (canAfford(action - 5))
        {
            switch (action)
            {
                case 5:// raw steak
                    if (!isSlotFull(0,1)) { incrementInventory(0, 3); money -= prices[action - 5]; }
                    break;
                case 6:// tortilla
                    if (!isSlotFull(2, 1)) { incrementInventory(2, 3); money -= prices[action - 5]; }
                    break;
                case 7:// telera
                    if (!isSlotFull(3, 1)) { incrementInventory(3, 1); money -= prices[action - 5]; }
                    break;
                case 8:// egg
                    if (!isSlotFull(4, 1)) { incrementInventory(4, 1); money -= prices[action - 5]; }
                    break;
                case 9:// lechera
                    if (!isSlotFull(5, 1)) { incrementInventory(5, 1); money -= prices[action - 5]; }
                    break;
                case 10:// evaporated milk
                    if (!isSlotFull(6, 1)) { incrementInventory(6, 1); money -= prices[action - 5]; }
                    break;
                case 11:// sugar
                    if (!isSlotFull(7, 56)) { incrementInventory(7, 56); money -= prices[action - 5]; }
                    break;
                case 12:// coffee bean
                    if (!isSlotFull(8, 1)) { incrementInventory(8, 1); money -= prices[action - 5]; }
                    break;
                case 13:// milk
                    if (!isSlotFull(9, 64)) { incrementInventory(9, 64); money -= prices[action - 5]; }
                    break;
                case 14:// jarrito
                    if (!isSlotFull(14, 1)) { incrementInventory(14, 1); money -= prices[action - 5]; }
                    break;
            }
            updateMoney();
        }
    }

    private bool canAfford(int inventoryIndex)
    {
        return money - prices[inventoryIndex] >= 0f;
    }

    public bool hasReachedGoal()
    {
        return money >= 3000f;
    }

    bool isSlotFull(int inventoryIndex, int amount)
    {
        return inventory[inventoryIndex] + amount > 999;
    }

    public void takeMoney(float amount)
    {
        money += amount;
        updateMoney();
    }
}
