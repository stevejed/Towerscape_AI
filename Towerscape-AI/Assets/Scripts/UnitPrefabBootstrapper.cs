using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabBootstrapper : MonoBehaviour {

    // Variables //

    // Variables for: Unit Intelligence generation //
    List<UnitIntelligence> unitPrefabs;
    Object[] unitActionSets;

    // Variables for: Unit State generation //
    private UnitState[] unitStates;
    public Object unitStateSet;


    // Updates //

	// Use this for initialization
	void Start () {
        // Generates Unit States
        unitStates = new UnitState[unitPrefabs.Count];
        for(int i = 0; i < unitPrefabs.Count; i++)
        {
            unitStates[i] = unitPrefabs[i].GetComponent<UnitState>();
        }

        //Generates Unit Intelligences
        BehaviorSetReader reader = new BehaviorSetReader();
        for(int i = 0; i < unitPrefabs.Count; i++)
        {
            reader.unitIntelligenceScript = unitPrefabs[i];
            reader.actionSetFile = unitActionSets[i];
            reader.ReadInBehaviors();
        }
	}
}
