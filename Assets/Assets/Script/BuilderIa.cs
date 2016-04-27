using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
################################
#          A FAIRE             #
################################
- 
*/
public class BuilderIa : MonoBehaviour
{
    private GameObject _chest;
    public Class.inventory Inventory;
    private GameObject[] _mineralList;
    private const float Distance = 0.75f;
    //Craft.Craft.craftList craftList;
    List<Craft.craft> _craftList;
    public bool IsTargeted = false;
    private string _mining;
    private int _idMineral;
    private Items.tool pickSlot;

    public string GetMining()
    {
        return _mining;
    }

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        _craftList = Craft.CraftList;
        Init();
        //craftList = Craft.Craft.crea
        //StartCoroutine(GoMining(choosen()));
    }
    IEnumerator Test()
    {
        GameObject test = Choosen();
        while (Vector3.Distance(transform.position, test.transform.position) >= Distance)
        {
            print(Vector3.Distance(transform.position, test.transform.position));
            GoTo(test);
            yield return new WaitForEndOfFrame();
        }
    }

    private GameObject Choosen()
    {
        GameObject choosen;
        return choosen = _mineralList[Random.Range(0, _mineralList.Length)];
    }

    void Init()
    {
        Inventory = new Class.inventory(40);
        _chest = GameObject.Find("Chest");
        _mineralList = GameObject.FindGameObjectsWithTag("Mineral");
    }
    // Update is called once per frame
    void Update() { }
    private void Act()
    {
        StartCoroutine(Inventory.Full != true ? GoMining(_mineralList[_idMineral]) : GoStock());
    }

    private void Act(GameObject choosen)
    {
        StartCoroutine(Inventory.Full != true ? GoMining(choosen) : GoStock());
    }

    public void ChangeMining()
    {
        if (_mining != null)
        {
            if (_idMineral < _mineralList.Length - 1)
            {
                StopAllCoroutines();
                _idMineral++;
                _mining = _mineralList[_idMineral].transform.name;
                Act(_mineralList[_idMineral]);
            }
            else if (_idMineral == _mineralList.Length - 1)
            {
                StopAllCoroutines();
                _idMineral = 0;
                _mining = _mineralList[_idMineral].transform.name;
                Act(_mineralList[_idMineral]);
            }
        }
        else
        {
            StopAllCoroutines();
            _mining = _mineralList[_idMineral].transform.name;
            Act(_mineralList[_idMineral]);
        }
    }
    IEnumerator GoMining(GameObject minerais)
    {
        if (Inventory.Full != true)
        {
            while (Vector3.Distance(transform.position, minerais.transform.position) > 0.75f)
            {
                GoTo(minerais);
                yield return new WaitForEndOfFrame();
            }
            //print("Je mine");
            yield return new WaitForSeconds(1f);
            Inventory.Add(minerais.GetComponent<MineralScript>().Mine());
            StartCoroutine(GoMining(minerais));
        }
        else {
            Act();
        }
    }
    private IEnumerator GoStock()
    {
        while (Vector3.Distance(transform.position, _chest.transform.position) > 0.75f)
        {
            GoTo(_chest);
            yield return new WaitForEndOfFrame();
        }
        for (var i = 0; i < Inventory.Diff; i++)
        {
            _chest.GetComponent<ChestScript>().Inventory.Add(Inventory.Inventory[i]);
        }
        Inventory.ClearInv();
        Act();
        yield return new WaitForEndOfFrame();
    }
    private void GoTo(GameObject target)
    {
        var targetPos = new Vector3(target.transform.position.x, 0.75f, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.5f * Time.deltaTime);
    }
    private Craft.craft ChooseCraft()
    {
        return _craftList[Random.Range(0, _craftList.Count - 1)];
    }
    public void craft(Items item)
    {
        GoTo(_chest);

    }
    bool canCraft(Items item)
    {
        return false;
    }
}
