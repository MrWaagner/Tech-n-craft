using UnityEngine;
using System.Collections.Generic;

/*
################################
#          A FAIRE             #
################################
- Finir de créer les objets
*/

public class Items : MonoBehaviour
{
    //public Item[] _items { get; set; }
    
    public class baseItem
    {
        public string name { get; set; }
        //private int damage { get; set; }
        //private Compo compo { get; set; }
        public int itemID { get; set; }
        public Dictionary<int, int> compo; //compo ID, nbr
        public enum ItemTypes
        {
            GEAR,
            TOOL,
            STORAGE,
            PLACEABLE
        }
        public ItemTypes itemType { get; set; }
        public baseItem(Dictionary<string, string> itemsDictionary)
        {
            name = itemsDictionary["name"];
            itemID = int.Parse(itemsDictionary["itemID"]);
            itemType = (ItemTypes)System.Enum.Parse(typeof(baseItem.ItemTypes), itemsDictionary["itemType"]);
            //compo = itemsDictionary["compo"];
            toString();
        }
        public baseItem() { }
        public virtual void toString() { }
        public string whatIsNeeded()
        {
            string fullString = " Need:";
            foreach(var comp in compo)
            {
                fullString += " " + comp.Value + "x " + findIDName(comp.Key);
            }
            return fullString;
        }
        public baseItem findIDItem(int ID)
        {
            return ItemDatabase.inventoryItems.Find(item => item.itemID == ID);
        }
        public string findIDName(int ID)
        {
            return ItemDatabase.inventoryItems.Find(item => item.itemID == ID).name;
        }
    }
    public class gear : baseItem
    {
        public enum GearTypes
        {
            BOOT,
            GLOVE,
            TOOLBELT
        }
        public GearTypes gearType { get; set; }
        public int StatModifier { get; set; }
        public gear(Dictionary<string, string> itemsDictionary, Dictionary<int, int> itemCompo)
        {
            name = itemsDictionary["name"];
            itemID = int.Parse(itemsDictionary["itemID"]);
            itemType = (ItemTypes)System.Enum.Parse(typeof(baseItem.ItemTypes), itemsDictionary["itemType"]);
            gearType = (GearTypes)System.Enum.Parse(typeof(gear.GearTypes), itemsDictionary["type"]);
            StatModifier = int.Parse(itemsDictionary["statModifier"]);
            compo = itemCompo;
        }
        public string whatItMod()
        {
            switch (gearType.ToString())
            {
                case "BOOT":
                    return "moveSpeed";
                case "GLOVE":
                    return "miningSpeed";
                case "TOOLBELT":
                    return "craftSpeed";
                default:
                    return "Error";
            }
        }
        public override void toString()
        {
            print(this.name +
                " ID: " + this.itemID +
                " Type: " + this.itemType +
                " Gear: " + gearType +
                " StatModified: " + whatItMod() + " + " + StatModifier +
                whatIsNeeded()
            );
        }
    }
    public class tool : baseItem
    {
        public int durability { get; set; }
        public int StatModifier { get; set; }
        public enum ToolTypes
        {
            PICKAXE,
            AXE,
            WRENCH
        }
        public ToolTypes toolType { get; set; }
        public tool(Dictionary<string, string> itemsDictionary, Dictionary<int, int> itemCompo)
        {
            name = itemsDictionary["name"];
            itemID = int.Parse(itemsDictionary["itemID"]);
            itemType = (ItemTypes)System.Enum.Parse(typeof(baseItem.ItemTypes), itemsDictionary["itemType"]);
            durability = int.Parse(itemsDictionary["durability"]);
            StatModifier = int.Parse(itemsDictionary["statModifier"]);
            toolType = (ToolTypes)System.Enum.Parse(typeof(tool.ToolTypes), itemsDictionary["type"]);
            compo = itemCompo;
        }
        public string whatItMod()
        {
            switch (toolType.ToString())
            {
                case "PICKAXE":
                    return "SpeedOnMineral";
                case "AXE":
                    return "SpeedOnVegetable";
                case "WRENCH":
                    return "craftSpeed";
                default:
                    return "Error";
            }
        }
        public override void toString()
        {
            print(this.name +
                " ID: " + this.itemID +
                " Type: " + this.itemType +
                " Tool: " + toolType +
                " StatModified: " + whatItMod() + " + " + StatModifier +
                " Durability: " + durability +
                whatIsNeeded()
            );
        }
    }
    public class storage : baseItem
    {
        public Class.inventory inventory { get; set; }
        public enum StorageTypes
        {
            BACKPACK
        }
        public StorageTypes storageType { get; set; }
        public storage(Dictionary<string, string> itemsDictionary, Dictionary<int, int> itemCompo)
        {
            name = itemsDictionary["name"];
            itemID = int.Parse(itemsDictionary["itemID"]);
            itemType = (ItemTypes)System.Enum.Parse(typeof(baseItem.ItemTypes), itemsDictionary["itemType"]);
            inventory = new Class.inventory(int.Parse(itemsDictionary["inventorySpace"]));
            storageType = (StorageTypes)System.Enum.Parse(typeof(storage.StorageTypes), itemsDictionary["type"]);
            compo = itemCompo;
        }
    }
    public class placeable : baseItem
    {
        public int stack { get; set; }
        //corps
        //peut etre placé
        public placeable(Dictionary<string, string> itemsDictionary, Dictionary<int, int> itemCompo)
        {
            name = itemsDictionary["name"];
            itemID = int.Parse(itemsDictionary["itemID"]);
            itemType = (ItemTypes)System.Enum.Parse(typeof(baseItem.ItemTypes), itemsDictionary["itemType"]);
            compo = itemCompo;
        }
    }
}
