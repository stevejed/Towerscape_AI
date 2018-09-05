using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabBootstrapper : MonoBehaviour {

    // Variables //

    // Variables for: Unit Intelligence generation //
    [SerializeField]
    private List<GameObject> unitPrefabs = new List<GameObject>();
    [SerializeField]
    private List<Object> unitActionSets = new List<Object>();

    // Variables for: Unit State generation //
    /* TO-DO
     * Add these variables when adding the Unit State reading from the .csv file
    private List<UnitState> unitStates = new List<UnitState>();
    private Object unitStateSet;
    */




    //Methods//

    // method - public: reads the action sets for each unit from the file //
    public void ReadInBehaviors()
    {
        /* TO-DO
         * Update to create the Unit States from the .csv file
        // Generates Unit States
        unitStates = new UnitState[unitPrefabs.Count];
        for(int i = 0; i < unitPrefabs.Count; i++)
        {
            unitStates[i] = unitPrefabs[i].GetComponent<UnitState>();
        }
        */

        //Generates Unit Intelligences from the unit action sets
        if (!GetComponent<BehaviorSetReader>())
            gameObject.AddComponent<BehaviorSetReader>();
        BehaviorSetReader reader = GetComponent<BehaviorSetReader>(); 
        for (int i = 0; i < unitPrefabs.Count; i++)
        {
            reader.unitIntelligenceScript = unitPrefabs[i].GetComponent<UnitIntelligence>();
            reader.actionSetFile = unitActionSets[i];
            reader.ReadInBehaviors();
        }
    }

    // method - public: writes out the action sets for each unit to their respective file //
    public void WriteOutBehaviors()
    {
        /* TO-DO
         * Update to write out Unit State values to the .csv file
         */

        //Writes out the unit action sets from the unit prefabs
        if (!GetComponent<BehaviorSetReader>())
            gameObject.AddComponent<BehaviorSetReader>();
        BehaviorSetReader reader = GetComponent<BehaviorSetReader>();
        for (int i = 0; i < unitPrefabs.Count; i++)
        {
            reader.unitIntelligenceScript = unitPrefabs[i].GetComponent<UnitIntelligence>();
            reader.actionSetFile = unitActionSets[i];
            reader.WriteOutBehaviors();
        }
    }




    // Updates //

    // Use this for initialization
    void Start ()
    {
        ReadInBehaviors();
	}
}
