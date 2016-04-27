using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ui : MonoBehaviour {
    public Button Test;
    private GameObject _chest;
    private List<Craft.craft> _craftList;
    private int _multiplier = 20;
    private GameObject[] _playerList;
    MouseController _mm;

    // Use this for initialization
    IEnumerator Start() {
        yield return new WaitForSeconds(0.5f);
        //CreateButton(test, )
        _craftList = Craft.CraftList;
        _chest = GameObject.Find("Chest");
        _playerList = GameObject.FindGameObjectsWithTag("IA");
        _mm = FindObjectOfType<MouseController>();
    }
    void PlayerGui()
    {
        int ttHeight = 10; //défini le y des boites
        foreach(GameObject ia in _playerList)
        {
            int boxHeight = (ia.GetComponent<BuilderIa>().Inventory.Diff == 0) ? 50:(30 * (ia.GetComponent<BuilderIa>().Inventory.Diff)) + 20;
            GUI.Box(new Rect(10, ttHeight, 100, boxHeight), ia.name);
            if (ia.GetComponent<BuilderIa>().Inventory.Contient != 0)
            {
                int i = 1;
                foreach (Class.Mineral item in ia.GetComponent<BuilderIa>().Inventory.Inventory)
                {
                    GUI.Box(new Rect(20, ttHeight+(_multiplier*i++), 80, 20), item.Name + ": " + item.Nbr);
                }
            }
            else
            {
                GUI.Box(new Rect(20, ttHeight+20, 80, 20), "Vide !");
            }
            ttHeight += boxHeight + 10;
        }
    }
    void ChestGui()
    {
        int ttHeight = 10; //défini le y des boites
        int boxHeight = (_chest.GetComponent<ChestScript>().Inventory.Diff == 0) ? 50 : (30 * (_chest.GetComponent<ChestScript>().Inventory.Diff)) + 20;
        GUI.Box(new Rect(10, ttHeight, 100, boxHeight), "Inventaire");
        if (_chest.GetComponent<ChestScript>().Inventory.Contient != 0)
        {
            int i = 1;
            foreach (Class.Mineral item in _chest.GetComponent<ChestScript>().Inventory.Inventory)
            {
                GUI.Box(new Rect(20, ttHeight + (_multiplier * i++), 80, 20), item.Name + ": " + item.Nbr);
            }
        }
        else
        {
            GUI.Box(new Rect(20, ttHeight + 20, 80, 20), "Vide !");
        }
        ttHeight += boxHeight + 10;
    }
    void TestGui()
    {
        //GUI.Box(new Rect(20, Screen.height - 50, 30, 20), chest.GetComponent<ChestScript>().inventory.find("Iron").ToString());
    }
    void ActionMenuGui()
    {
        if (_mm.SelectedObject != null)
        {
            switch (_mm.SelectedObject.transform.tag)
            {
                case "IA":
                    IaActionMenu();
                    break;
                case "Stuff":
                    switch (_mm.SelectedObject.transform.name)
                    {
                        case "Chest":
                            ChestActionMenu();
                            break;
                    }
                    break;
                case "Mineral":
                    MineralActionMenu();
                    break;
            }
        }
    }
    void IaActionMenu()
    {
        //Display change mining menu
        GUI.Box(new Rect(10, Screen.height - 60, 100, 50), "mine");
        if (GUI.Button(new Rect(20, Screen.height - 40, 80, 20), _mm.SelectedObject.GetComponent<BuilderIa>().GetMining()))
        {
            _mm.SelectedObject.GetComponent<BuilderIa>().ChangeMining();
        }
        int boxHeight = (_mm.SelectedObject.GetComponent<BuilderIa>().Inventory.Diff == 0) ? 50 : (30 * (_mm.SelectedObject.GetComponent<BuilderIa>().Inventory.Diff)) + 20;
        GUI.Box(new Rect(10, Screen.height-(70+boxHeight), 100, boxHeight), _mm.SelectedObject.name);
        if (_mm.SelectedObject.GetComponent<BuilderIa>().Inventory.Contient != 0)
        {
            int i = 1;
            foreach (Class.Mineral item in _mm.SelectedObject.GetComponent<BuilderIa>().Inventory.Inventory)
            {
                GUI.Box(new Rect(20, Screen.height-(70+boxHeight) + (_multiplier * i++), 80, 20), item.Name + ": " + item.Nbr);
            }
        }
        else
        {
            GUI.Box(new Rect(20, Screen.height-120 + 20, 80, 20), "Vide !");
        }
        CraftMenu();
    }
    void CraftMenu()
    {
        int height = 40;
        GUI.Box(new Rect(Screen.width - 110, 10, 100, (30 + (30 * _craftList.Count - 1))), "craft");
        foreach(var item in ItemDatabase.inventoryItems)
        {
            if(GUI.Button(new Rect(Screen.width - 100, height, 80, 20), item.name))
            {
                //_mm.SelectedObject.GetComponent<BuilderIa>().craft(item);
            }
            height += 30;
        }
    }
    void ChestActionMenu()
    {

    }
    void MineralActionMenu()
    {

    }
    void OnGUI()
    {
        if (Time.time > 1)
        {
            //playerGUI();
            ChestGui();
            TestGui();
            ActionMenuGui();
        }
    }
}

