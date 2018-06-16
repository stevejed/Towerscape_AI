using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIntelligence : MonoBehaviour {

    //Variables//

    private Action[] actions;


    //Methods//

    /*Takes in a fully parsed action set and adds it to the given UnitIntelligence
     * param LinkedList<Action> actionSet - Set of all Actions as a LinkedList
     */
    public void AddActions(LinkedList<Action> actionSet)
    {
        actions = new Action[actionSet.Count];

        int count = 0;
        foreach(Action curr in actionSet)
        {
            actions[count++] = curr;
        }
    }


    //Updates//

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
