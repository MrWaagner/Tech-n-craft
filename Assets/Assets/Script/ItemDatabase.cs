using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class ItemDatabase : MonoBehaviour {

    public TextAsset itemInventory;
    public static List<Items.baseItem> inventoryItems = new List<Items.baseItem>();

    private List<Dictionary<string, string>> inventoryItemsDictionary = new List<Dictionary<string, string>>();
    private Dictionary<string, string> inventoryDictionary;

    void Awake()
    {
        readItemsFromDB();
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i].toString();
        }

    }

    public void readItemsFromDB()
    {
        XmlDocument xmlItems = new XmlDocument();
        xmlItems.LoadXml(itemInventory.text);
        XmlNodeList itemsList = xmlItems.GetElementsByTagName("item");

        foreach(XmlNode itemInfo in itemsList)
        {
            int rdyForValue = 0;
            int key = 0;
            int value = 0;
            Dictionary<int, int> itemCompo = new Dictionary<int, int>();
            XmlNodeList itemContent = itemInfo.ChildNodes;
            inventoryDictionary = new Dictionary<string, string>();
            foreach(XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "name":
                        inventoryDictionary.Add("name", content.InnerText);
                        break;
                    case "itemID":
                        inventoryDictionary.Add("itemID", content.InnerText);
                        break;
                    case "itemType":
                        inventoryDictionary.Add("itemType", content.InnerText);
                        break;
                    case "durability":
                        inventoryDictionary.Add("durability", content.InnerText);
                        break;
                    case "statModifier":
                        inventoryDictionary.Add("statModifier", content.InnerText);
                        break;
                    case "type":
                        inventoryDictionary.Add("type", content.InnerText);
                        break;
                    case "inventorySpace":
                        inventoryDictionary.Add("inventorySpace", content.InnerText);
                        break;
                    case "comp":
                        if (rdyForValue == 0)
                        {
                            key = int.Parse(content.InnerText);
                            rdyForValue = 1;
                            break;
                        }
                        else
                        {
                            value = int.Parse(content.InnerText);
                            itemCompo.Add(key, value);
                            rdyForValue = 0;
                            break;
                        }
                }
            }
            switch (inventoryDictionary["itemType"])
            {
                case "GEAR":
                    inventoryItems.Add(new Items.gear(inventoryDictionary, itemCompo));
                    break;
                case "TOOL":
                    inventoryItems.Add(new Items.tool(inventoryDictionary, itemCompo));
                    break;
                case "STORAGE":
                    inventoryItems.Add(new Items.storage(inventoryDictionary, itemCompo));
                    break;
                case "PLACEABLE":
                    inventoryItems.Add(new Items.placeable(inventoryDictionary, itemCompo));
                    break;
            }
        }
    }
}
