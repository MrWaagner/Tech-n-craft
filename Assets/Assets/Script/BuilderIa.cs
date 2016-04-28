using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
################################
#           KESAKO ?           #
################################
Ce Script est insérer sur les 
builder. Il gère toutes les actions
liées à ces derniers
################################
#          A FAIRE             #
################################
- 
*/
public class BuilderIa : MonoBehaviour
{
    private GameObject _chest;
    public Class.inventory Inventory { get; set; }
    private GameObject[] _ressourceList; //tableau des différentes ressources présentes
    private const float Distance = 0.75f; //la distance des "pas" parcourut par le builder
    public string _mining { get; set; } //La ressource en train d'être récolter par le builder
    private int _ressourceID = 0; //L'ID de la ressource en train d'être récolter par le builder
    private Items.tool pickSlot;
    public float miningSpeed { get; set; }

    //normalement void Start(), cette fonction se lance au démarrage du jeu
    //IEnumerator permet ici uniquement à délayer le démarrage du script builder
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f); //le démarrage du script des builders se fait 1sec après le lancement du jeu
        Init(); //On initialise quelques valeurs au démarrage
    }

    void Init()
    {
        Inventory = new Class.inventory(40); //on initialise l'inventaire
        _chest = GameObject.Find("Chest"); //On repère la position du coffre
        _ressourceList = GameObject.FindGameObjectsWithTag("Mineral"); //On insère toutes les ressources dans la liste
        miningSpeed = 1f;
    }
    //La fonction act lance le builder au mining ou au stockage
    private void Act()
    {
        //si l'inventaire n'est pas plein -> Récolte les ressources / sinon -> stock les ressources récoltés
        StartCoroutine(Inventory.Full != true ? GoMining(_ressourceList[_ressourceID]) : GoStock());
    }
    //fonction permettant le changement de ressource récolté
    public void ChangeMining()
    {
        if (_ressourceID < _ressourceList.Length - 1) //si l'id de la ressource fait partie de la liste et est inférieur au dernier présent
        {
            StopAllCoroutines(); //On arrete la tache actuelle
            _ressourceID++; //On incrémente pour cibler la prochaine ressource
            _mining = _ressourceList[_ressourceID].transform.name; //On insère le nom de la ressource dans mining
            Act(); //On appelle Act() pour mettre le builder au boulot
        }
        else if (_ressourceID == _ressourceList.Length - 1) //si la ressource actuelle est la dernière de la liste
        {
            StopAllCoroutines();
            _ressourceID = 0; //on remet à 0 ressourceID pour revenir au début de la liste
            _mining = _ressourceList[_ressourceID].transform.name;
            Act();
        }
    }
    //Go mining va servir de "cerveau de travail"
    //IEnumerator servira ici à maintenir la fonction en éxecution
    IEnumerator GoMining(GameObject minerais)
    {
        //On déplace le builder jusqu'a la ressource désiré
        while (Vector3.Distance(transform.position, minerais.transform.position) > Distance)
        {
            GoTo(minerais);
            yield return new WaitForEndOfFrame();//A chaque fin de boucle, on attend la fin de la frame pour continuer la boucle
        }
        while (Inventory.Full != true) //Tant que le builder n'est pas full, on mine
        {
            yield return new WaitForSeconds(miningSpeed); //On attend le temps de récolte du builder
            Inventory.Add(minerais.GetComponent<MineralScript>().Mine()); //on ajoute à l'inventaire les ressources minés
        }
        Act();
    }
    private IEnumerator GoStock()
    {
        while (Vector3.Distance(transform.position, _chest.transform.position) > Distance)
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
        var targetPos = new Vector3(target.transform.position.x, Distance, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.5f * Time.deltaTime);
    }
}
