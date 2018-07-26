using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UnitIntelligence
// • encapsulating class for a unit's intelligence and behavior operation
//   · stores a set of potential actions that the unit operates through to find the one most appropriate for the current situation
public class UnitIntelligence : MonoBehaviour {

    // Variables //

    // variables for: file read/write of an action set file //
    [Header("File Read/Write")][SerializeField]
    public Object actionSetFile;

    // variables for: debugging action selection operation //
    [Header("Debug")][SerializeField]
    public bool displayActionSelection;

    // variables for: action selection and execution operation //
    [Header("Intelligence")][SerializeField]
    private Action[] actions;
    private string unitName;




    //Methods//

    // method - public: gets the action set for the instance //
    public Action[] GetActions() { return actions; }

    // method - public: sets the actions for the instance to the given set of actions //
    // * param LinkedList<Action> actionSet - set of all actions to set
    public void SetActions(LinkedList<Action> actionSet)
    {
        actions = new Action[actionSet.Count];

        int count = 0;
        foreach(Action curr in actionSet)
        {
            actions[count++] = curr;
        }

        //Returns Action Set in Debug console
        string output = gameObject.name + " Actions:\n";
        foreach(Action curr in actions)
        {
            output += curr.ToString() + "\n";
        }
        Debug.Log(output);
    }

    // method - public: selects an action from the possible options and executes it //
    public void SelectAction()
    {
        float bestAppropriateValue = -1.0f;
        int bestAction = -1;
        for(int i = 0; i < actions.Length; i++)
        {
            float currAppropriateValue = actions[i].GetScore();
            if(currAppropriateValue > bestAppropriateValue)
            {
                bestAppropriateValue = currAppropriateValue;
                bestAction = i;
            }
        }
        if(bestAction != -1)
        {
            actions[bestAction].Execute();
        }
    }

    // method - public: reads the instance's action set from the file //
    public void ReadInBehaviors()
    {
        if (!GetComponent<BehaviorSetReader>())
        {
            BehaviorSetReader reader = gameObject.AddComponent<BehaviorSetReader>();
            reader.actionSetFile = this.actionSetFile;
            reader.unitIntelligenceScript = this;
        }
        GetComponent<BehaviorSetReader>().ReadInBehaviors();
    }

    // method - public: writes the instance's action set to the file //
    public void WriteOutBehaviors()
    {
        if (!GetComponent<BehaviorSetReader>())
        {
            BehaviorSetReader reader = gameObject.AddComponent<BehaviorSetReader>();
            reader.actionSetFile = this.actionSetFile;
            reader.unitIntelligenceScript = this;
        }
        GetComponent<BehaviorSetReader>().WriteOutBehaviors();
    }




    //Updates//

    // once the instance is awake: //
    void Awake () {
        if (displayActionSelection)
        {
            GetComponent<Canvas>().enabled = true;
        }
        ReadInBehaviors();
	}
	
	// at each frame: //
	void Update () {
        SelectAction();
	}
}
