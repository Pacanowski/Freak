using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    int currentSize;
    int maxSize = 4;

    int selectedSlot;

    public Image hotbarSelectedSlotImage;

    public Image hotbarSlot1Image;
    public Image hotbarSlot2Image;
    public Image hotbarSlot3Image;
    public Image hotbarSlot4Image;

    InputAction h1; //hotbar slot 1
    InputAction h2;
    InputAction h3;
    InputAction h4;


    void EmptyInventory()
    {
        for (int i = 0; i < maxSize; i++)
        {
            inventory.Add(Items.AIR);
        }
    }

    void Start()
    {
        h1 = InputSystem.actions.FindAction("slot1");
        h2 = InputSystem.actions.FindAction("slot2");
        h3 = InputSystem.actions.FindAction("slot3");
        h4 = InputSystem.actions.FindAction("slot4");

        EmptyInventory();

        AddItem(Items.Sword);
    }

    void FixedUpdate()
    {

        if (inventory[0] != Items.AIR)
        {
            hotbarSlot1Image.sprite = inventory[0].getImage().sprite;
        }
        if (inventory[1] != Items.AIR)
        {
            hotbarSlot2Image.sprite = inventory[1].getImage().sprite;
        }
        if (inventory[2] != Items.AIR)
        {
            hotbarSlot3Image.sprite = inventory[2].getImage().sprite;
        }
        if (inventory[3] != Items.AIR)
        {
            hotbarSlot4Image.sprite = inventory[3].getImage().sprite;
        }


        if (h1.IsPressed())
        {
            selectedSlot = 1;
        }
        if (h2.IsPressed())
        {
            selectedSlot = 2;
        }
        if (h3.IsPressed())
        {
            selectedSlot = 3;
        }
        if (h4.IsPressed())
        {
            selectedSlot = 4;
        }

    }


    void AddItem(Item item)
    {
        int slot = FindEmptySlot();

        if (slot > -1)
        {
            inventory[slot] = item;
            currentSize++;
        }
        else
        {
            Debug.Log("max size already reached");
        }
    }

    int FindEmptySlot()
    {
        int slot = 0;

        foreach (Item item in inventory)
        {
            if (item == Items.AIR)
            {
                return slot;
            }
            slot++;
        }
        return -1;
    }

    void DropItem(int slot)
    {
        inventory.RemoveAt(slot);
    }
}


