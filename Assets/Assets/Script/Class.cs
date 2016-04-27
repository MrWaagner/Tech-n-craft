using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Class {
    public class Mineral
    {
        public new string Name;
        public int Nbr;
        public Mineral(string name, int nbr)
        {
            this.Name = name;
            this.Nbr = nbr;
        }
    }
    public class Mineable : MonoBehaviour
    {
        public string Loot;
        public string Name;
        public Mineable(string Loot, string Name)
        {
            this.Loot = Loot;
            this.Name = Name;
        }
    }
    public class inventory
    {
        public int Contient; //nombre total d'objet présent
        public int Diff; //nombre d'objet différent
        public bool Full;
        private int _maxInv;
        public List<Mineral> Inventory;
        public inventory(int MaxInv)
        {
            Contient = 0;
            Diff = 0;
            _maxInv = MaxInv;
            Full = false;
            Inventory = new List<Mineral>();
        }
        public void Add(Mineral mineral)
        {
            if (Contient != 0) //si l'inventaire n'est pas vide
            {
                int res = Find(mineral.Name); //on regarde si le minerais est deja présent (position sinon -1)
                if(Contient + mineral.Nbr > _maxInv) //si la somme du contenu + nouveau > maxInv
                {
                    int surplus = (Contient + mineral.Nbr) - _maxInv;
                    mineral.Nbr -= surplus;
                    Full = true;
                }
                if (res != -1) //si l'objet est présent
                {
                    Inventory[res].Nbr += mineral.Nbr;
                }
                else //s'il ne l'est pas
                {
                    Inventory.Add(mineral);
                    Diff += 1;
                }
            }
            else //si il est vide
            {
                Inventory.Add(mineral);
                Diff += 1;
            }
            Contient += mineral.Nbr;
        }
        public int Find(string item)
        {
            if (Contient != 0)
            {
                int i;
                for (i = 0; i < Diff; i++)
                {
                    if (Inventory[i].Name == item)
                    {
                        return i;
                    }
                }
                if (i == Diff && Inventory[i - 1].Name != item)
                {
                    return -1;
                }
            }
            return -1;
        }
        public void ClearInv()
        {
            Inventory.Clear();
            Full = false;
            Contient = 0;
            Diff = 0;
        }
        public int GetContient()
        {
            return Contient;
        }
        /*public void toString()
        {
            int i;
            for (i = 0; i < diff; i++)
            {
                System.Console.WriteLine(Inventory[i].nom + " x" + Inventory[i].nbr);
            }
        }*/
    }
}
