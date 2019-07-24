using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DragObject : MonoBehaviour
{
    //Initialize Variables
    private GameObject getTarget;
    private GameObject hitObject;
    public GameObject GoalPositionSign;



    private Vector3 offsetValue;
    private Vector3 positionOfScreen;
    private Vector3 SpawnPoint;
    private Transform spawnPoint;

    public Transform[] Goals;
    public UnityEvent[] GoalEvents;

    public float TargetRadius = 5f;
    //public bool buttonDown;
    //public bool buttonUp;

    //private bool isMouseDragging;
    private bool firstPosSet = false;
    private bool isRemoved = false;
    private Inventory Inventory;

    // Use this for initialization
    void Start()
    {
        Inventory = GameObject.FindObjectOfType<Inventory>();

        hitObject = this.gameObject;
        if (!firstPosSet)
        {
            SpawnPoint = hitObject.transform.position;
            firstPosSet = true;
        }
        PlaceGoalSigns();
        hitObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void PlaceGoalSigns()
    {
        foreach (Transform g in Goals)
        {
            Instantiate(GoalPositionSign, g.position, GoalPositionSign.transform.rotation);
        }
    }

    void Update()
    {
        //Debug.Log(this.gameObject.transform.position);
        //Debug.Log("bDown" + buttonDown + "     bUp" + buttonUp);
        //Mouse Button Press Down
        //Input.GetMouseButtonDown(0)
        //if (buttonDown)
        //{
            //RaycastHit hitInfo;
            //getTarget = ReturnClickedObject(out hitInfo);
            // if (getTarget != null)
            
            //isMouseDragging = true;
            //Converting world position to screen position.
            //positionOfScreen = Camera.main.WorldToScreenPoint(hitObject.transform.position);
            // Input.mousePosition
            //Vector3 position = this.gameObject.transform.position;
            //offsetValue = hitObject.transform.position - Camera.main.ScreenToWorldPoint(position);
            
       // }

        //if (buttonUp)
        //{
            //isMouseDragging = false;
           // CheckDistance();
        //}
    }

    //Method to Return Clicked Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }

        return target;
    }


    public void CheckDistance()
    {
        //buttonUp = false;
        for (int i = 0; i < Goals.Length; i++)
        {
            float distance = Vector3.Distance(hitObject.transform.position, Goals[i].position);
            if (distance <= TargetRadius) SetAtGoal(i); else Reset();
        }
    }

    void SetAtGoal(int goalnumber)
    {
        hitObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        hitObject.transform.position = Goals[goalnumber].position;
        if (goalnumber < GoalEvents.Length)
        {
            GoalEvents[goalnumber].Invoke();
        }
        Inventory.Remove(gameObject);
        isRemoved = true;
    }

    void Reset()
    {
        hitObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        hitObject.transform.position = SpawnPoint;
        if (isRemoved)
        {
            Inventory.Add(this.gameObject);
            isRemoved = false;
        }

        Inventory.UpdatePosition();
    }
}