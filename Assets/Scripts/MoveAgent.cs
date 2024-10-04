using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MoveAgent : Agent
{
    Rigidbody myRigidBody;
     
    //public GameObject cell0, cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10, cell11, cell12, cell13, cell14, cell15, cell16, cell17, cell18, cell19, cell20, cell21, cell22, cell23, cell24, cell25, cell26, cell27, cell28, cell29, cell30, cell31, cell32, cell33, cell34, cell35;

    //private bool wall_flag;
    private bool bonus_flag;
    public float forceCoeff = 10;
    public int width = 0;
    private HashSet<Vector3> visitedCells;
    private bool hasEnteredCell = false;
    private Vector3 oldCell;
    private GameObject currentCell;
    private CellColorChanger cellColorChanger;
    

    private List<GameObject> bonus = new List<GameObject>();

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        //wall_flag = false;

        visitedCells = new HashSet<Vector3>();

        currentCell = GameObject.Find("MazeCell");
    }

    public override void OnEpisodeBegin(){
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);

        visitedCells.Clear();
        hasEnteredCell = false;
        
        /*
        cellColorChanger = currentCell.GetComponentInChildren<CellColorChanger>();
        for(float i=0.5f; i<=5.5f; i++){
            for(float j=0.5f; j<=5.5f; j++){
                GameObject obj = GameObject.Find("Cell_"+new Vector3(i, 0.0f, j).ToString());
                obj.GetComponentInChildren<CellColorChanger>().ChangeColor("blue");
            }
        }*/

        /*
        for(float i=0.5f; i<=5.5f; i++){
            for(float j=0.5f; j<=5.5f; j++){
            GameObject MyGameObject = GameObject.Find("Cell_"+new Vector3(i, 0.0f, j).ToString());
            
            int ResetLayer = LayerMask.NameToLayer("wall");

            GameObject leftwalltransform = MyGameObject.transform.GetChild(0).gameObject;
            if (leftwalltransform != null)
            {
                // Ottieni il GameObject figlio
                //GameObject LeftWallGameObject = leftwalltransform.gameObject;

                // Cambia il layer del GameObject figlio
                leftwalltransform.layer = ResetLayer;
            }

            GameObject rightwalltransform = MyGameObject.transform.GetChild(1).gameObject;
            if (rightwalltransform != null)
            {
                // Ottieni il GameObject figlio
                //GameObject RightWallGameObject = rightwalltransform.gameObject;

                // Cambia il layer del GameObject figlio
                rightwalltransform.layer = ResetLayer;
            }

            GameObject backwalltransform = MyGameObject.transform.GetChild(2).gameObject;
            if (backwalltransform != null)
            {
                // Ottieni il GameObject figlio
                //GameObject BackWallGameObject = backwalltransform.gameObject;

                // Cambia il layer del GameObject figlio
                backwalltransform.layer = ResetLayer;
            }
            
            GameObject frontwalltransform = MyGameObject.transform.GetChild(3).gameObject;
            if (frontwalltransform != null)
            {
                // Ottieni il GameObject figlio
                //GameObject FrontWallGameObject = frontwalltransform.gameObject;

                // Cambia il layer del GameObject figlio
                frontwalltransform.layer = ResetLayer;
            }  
            }
        }
        */
        

        foreach(GameObject camel in bonus)
        {
            camel.SetActive(true);
        }
    }

    public override void OnActionReceived(ActionBuffers actions){
        Vector3 azioni = Vector3.zero;
        azioni.x = actions.ContinuousActions[0];
        azioni.z = actions.ContinuousActions[1];
        myRigidBody.AddForce(azioni * forceCoeff);
        
        /*
        if (wall_flag){
            wall_flag = false;
            SetReward(-0.5f);
        }
        */

        if (bonus_flag){
            bonus_flag = false;
            SetReward(10f);
        }

        
        // Vector3 roundedPosition = new Vector3(Mathf.Ceil(transform.localPosition.x), Mathf.Ceil(transform.localPosition.y), Mathf.Ceil(transform.localPosition.z));

        // if(roundedPosition.y > 0 & roundedPosition.x <= 5.5f & roundedPosition.z <= 5.5f & roundedPosition.x >= 0.5f & roundedPosition.z >= 0.5f){

        //     if(oldCell != roundedPosition){
        //     hasEnteredCell = false ;
        //     }
        //     if (!visitedCells.Contains(roundedPosition))
        //     {
        //         SetReward(1.5f);
        //         visitedCells.Add(roundedPosition);
        //         hasEnteredCell = true;
        //         oldCell = roundedPosition;

        //         /*
        //         GameObject obj = GameObject.Find("Cell_"+new Vector3(roundedPosition.x-0.5f, 0.0f, roundedPosition.z-0.5f).ToString());
        //         obj.GetComponentInChildren<CellColorChanger>().ChangeColor("orange"); */

        //         GameObject myGameObject = GameObject.Find("Cell_"+new Vector3(roundedPosition.x-0.5f, 0.0f, roundedPosition.z-0.5f).ToString());
        //         //Debug.Log("Cell_"+new Vector3(roundedPosition.x-0.5f, 0.0f, roundedPosition.z-0.5f).ToString());
        //         int newLayer = LayerMask.NameToLayer("visited");

        //         GameObject leftwalltransform = myGameObject.transform.GetChild(0)?.gameObject;

        //         if (leftwalltransform != null)
        //         {
        //             //Debug.Log("layer cambiato");
        //             // Ottieni il GameObject figlio
        //             //GameObject LeftWallGameObject = leftwalltransform.gameObject;

        //             // Cambia il layer del GameObject figlio
        //             leftwalltransform.layer = newLayer;
        //         }

        //         GameObject rightwalltransform = myGameObject.transform.GetChild(1)?.gameObject;
        //         if (rightwalltransform != null)
        //         {
        //             // Ottieni il GameObject figlio
        //             //GameObject RightWallGameObject = rightwalltransform.gameObject;

        //             // Cambia il layer del GameObject figlio
        //             rightwalltransform.layer = newLayer;
        //         }

        //         GameObject backwalltransform = myGameObject.transform.GetChild(2)?.gameObject;
        //         if (backwalltransform != null)
        //         {
        //             // Ottieni il GameObject figlio
        //             //GameObject BackWallGameObject = backwalltransform.gameObject;

        //             // Cambia il layer del GameObject figlio
        //             backwalltransform.layer = newLayer;
        //         }

        //         GameObject frontwalltransform = myGameObject.transform.GetChild(3)?.gameObject;
        //         if (frontwalltransform != null)
        //         {
        //             // Ottieni il GameObject figlio
        //             //GameObject FrontWallGameObject = frontwalltransform.gameObject;

        //             // Cambia il layer del GameObject figlio
        //             frontwalltransform.layer = newLayer;
        //         }   
        //     }
        //     else if(visitedCells.Contains(roundedPosition))
        //     {
        //         if (!hasEnteredCell)
        //         {
        //             Debug.Log("Cella già visitata");
                
        //             SetReward(-0.5f); // Assegna la reward negativa
                    
        //             hasEnteredCell = true; // Imposta la variabile a true per evitare ulteriori reward negative nella stessa cella
        //             oldCell = roundedPosition ;

        //             /*
        //             GameObject obj = GameObject.Find("Cell_"+new Vector3(roundedPosition.x-0.5f, 0.0f, roundedPosition.z-0.5f).ToString());
        //             obj.GetComponentInChildren<CellColorChanger>().ChangeColor("red");
        //             */
        //         }
        //     }
        // }
        
        if (transform.localPosition.y < 0){

            SetReward(100f);
            EndEpisode();
        }

    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(myRigidBody.velocity.x);
        sensor.AddObservation(myRigidBody.velocity.z);

        /*
        foreach (Vector3 cell in AllPossibleCells())
        {
            if (visitedCells.Contains(cell))
            {
                sensor.AddObservation(1.0f); // Mark the cell as visited
            }
            else
            {
                sensor.AddObservation(0.0f); // Mark the cell as not visited
            }
        }
        */
    }

    public override void Heuristic (in ActionBuffers actionsOut){
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
    
    /*
    private void OnTriggerEnter(Collider other) {  
        if(other.CompareTag("bonus")){
            bonus_flag = true;
            other.gameObject.SetActive(false);
            bonus.Add(other.gameObject);
        }
    }*/
    

    private void OnGUI()
    {
        // Disegna il testo del reward nello schermo di gioco
        GUI.Label(new Rect(10, 10, 200, 20), "Reward: " + GetCumulativeReward());
    }

    
    private IEnumerable<Vector3> AllPossibleCells()
    {
        // Define the range of your cells based on your environment's dimensions
        for (float i = 0.5f; i <= width-0.5f; i++)
        {
            for (float j = 0.5f; j <= width-0.5f; j++)
            {
                yield return new Vector3(i, 0.0f, j);
            }
        }
    }

}