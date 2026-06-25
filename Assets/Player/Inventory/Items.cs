using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{

    public Image axeImage;
    public GameObject axeModel;

    public Image swordImage;
    public GameObject swordModel;

    public Image airImage;
    public GameObject airModel;

    public static Item AIR;
    public static Item Sword;
    public static Item Axe;


    void Awake()
    {
        AIR = new Item(0, "air", airImage, airModel);
        Sword = new Item(1, "sword", swordImage, swordModel);
        Axe = new Item(2, "axe", axeImage, axeModel);
    }

}

public class Item
{
    public int Id;
    public string Name;
    public Image Img;
    public GameObject Model;

    public Item(int id, string name, Image img, GameObject model)
    {
        Id = id;
        Name = name;
        Img = img;
        Model = model;
    }

    public int getID()
    {
        return Id;
    }

    public string getName()
    {
        return Name;
    }

    public Image getImage()
    {
        return Img;
    }

    public GameObject getModel()
    {
        return Model;
    }

}
