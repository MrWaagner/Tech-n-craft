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
    private bool isMoving;

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

    //La fonction act lance le builder à la récolte ou au stockage
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
    //IEnumerator servira ici à maintenir la fonction à travers les fps
    IEnumerator GoMining(GameObject minerais)
    {
        StartCoroutine(GoTo(minerais));
        while (isMoving) { yield return new WaitForEndOfFrame(); }
        while (Inventory.Full != true) //Tant que le builder n'est pas full, on mine
        {
            yield return new WaitForSeconds(miningSpeed); //On attend le temps de récolte du builder
            Inventory.Add(minerais.GetComponent<MineralScript>().Mine()); //on ajoute à l'inventaire les ressources minés
        }
        Act(); //si l'on sort des deux whiles alors le builder est full et reprend depuis Act()
    }

    //Fonction de base pour allez stocker les ressources dans le coffre
    private IEnumerator GoStock()
    {
        StartCoroutine(GoTo(_chest));
        while (isMoving) { yield return new WaitForEndOfFrame(); }
        for (var i = 0; i < Inventory.Diff; i++) //Pour tout les images différents,
        {
            _chest.GetComponent<ChestScript>().Inventory.Add(Inventory.Inventory[i]); //on les stock dans le coffre
            yield return new WaitForEndOfFrame();
        }
        Inventory.ClearInv(); //On nettoie l'inventaire
        Act(); //Et on reprend depuis le début
    }

    private IEnumerator GoTo(GameObject target) //La fonction de base pour le déplacement
    {
        isMoving = true;
        var targetPos = new Vector3(target.transform.position.x, 0.75f, target.transform.position.z); //On set la position de la destination
        while (Vector3.Distance(transform.position, targetPos) > Distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.5f*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;
    }
}
