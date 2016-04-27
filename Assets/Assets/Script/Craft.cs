using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

/*
################################
#          A FAIRE             #
################################
-
*/
public class Craft : MonoBehaviour
{
    public class craft
    {
        public Hashtable InputItem { get; set; }

        public string OutputItem { get; set; }

        public craft(string output, Hashtable input)
        {
            OutputItem = output;
            InputItem = input;
        }
    }

    private const string Path = "Assets/fichier.txt";
    private readonly char[] _delimiteChar = { '=', '+' };
    public static List<craft> CraftList;

    // Use this for initialization
    void Start()
    {
        CraftList = CreateCrafting();
        //printList(craftList);
    }
    public void PrintList(List<craft> list)
    {
        foreach(craft item in list)
        {
            //print(item.toString());
        }
    }

    public List<craft> CreateCrafting()
    {
        var craftList = new List<craft>();
        foreach (string line in File.ReadAllLines(Path)) //traitement pour chacune des lignes du fichier
        {

            Hashtable temp = new Hashtable();
            string[] comp = line.Split(_delimiteChar); //Decoupe la ligne en fonction des caractères définie dans delimeChar
            for(int i = 1; i<comp.Length; i++)
            {
                temp.Add(comp[i], 1); //et mets les mots découpés dans un tableau
            }
            craftList.Add(new craft(comp[0], temp));  //Ajoute le tableau de craft à la liste
        }
        return craftList;
    }

    public 

    // Update is called once per frame
    void Update()
    {

    }
}   