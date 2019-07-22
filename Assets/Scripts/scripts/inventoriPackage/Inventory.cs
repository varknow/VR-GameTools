using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<GameObject> GameObjects;
    public List<GameObject> ItemsList;
    private Item[] Panes;

    void Start() 
    {
        GameObjects = new List<GameObject>();
        Panes = GetComponentsInChildren<Item>();
        for (int i = 0; i < ItemsList.Count; i++)
        {
            GameObject newObj = Instantiate(ItemsList[i]);
            newObj.transform.SetParent(Panes[i].transform);
            newObj.transform.position = Panes[i].transform.position;
            GameObjects.Add(newObj);          
        }
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        for (int i = 0; i < ItemsList.Count; i++)
        {
            GameObjects[i].GetComponent<Collider>().isTrigger = true;
            GameObjects[i].transform.position = Panes[i].transform.position;
            GameObjects[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }

    public void Add(GameObject gameObject)
    {
        gameObject.transform.SetParent(this.gameObject.transform);
        gameObject.transform.position = Panes[ItemsList.Count -1].transform.position;
        gameObject.transform.rotation = Panes[ItemsList.Count -1].transform.rotation;
        gameObject.transform.SetParent(Panes[ItemsList.Count - 1].transform);

        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.transform.SetParent(this.gameObject.transform);

        GameObjects.Add(gameObject);
        ItemsList.Add(gameObject);
        UpdatePosition();
    }

    public void Remove(GameObject game)
    {
        Debug.Log("remove");
        Debug.Log(GameObjects.Remove(game));
        ItemsList.Remove(game);
        UpdatePosition();
    }
}