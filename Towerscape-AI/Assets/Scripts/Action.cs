using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Action
// • encapsulates a possible Action for a Unit to perform
//   · stores set of Considerations and an associated Behavior
//   · provides an 'Appropriateness Score' based on Unit's Considerations
//   · provides a means of executing the associated Behavior
[System.Serializable]
public class Action
{

    // variables //


    // variables for: action scoring and behavior execution //
    [SerializeField][HideInInspector]
    private string name;                        // setting - global - private: action name
    private Behavior behavior;                  // setting - global - private: behavior for action to potentially execute
    [SerializeField]
    private List<Consideration> considerations; // setting - global - private: set of considerations for determining the Action's appropriateness score




    //Constructor//

    // constructs an instance of an Action: //
    // * param inName     - name of the Action read in from the file
    // * param inBehavior - behavior of the Action read in from the file
    public Action(string inName = "N/a", Behavior inBehavior = null, List<Consideration> inConsiderations = null)
    {
        this.name = inName;
        this.behavior = inBehavior;
        if (inConsiderations == null)
            this.considerations = new List<Consideration>();
        else
            this.considerations = inConsiderations;
    }


    //Methods//

    // method - public: constructs an instance of a Consideration and adds it to the Action's list: //
    //  * param string inInput - Input type read in from Excel sheet
    //  * param string inCurve - Curve Type read in from Excel sheet
    //  * param float m        - Slope of function
    //  * param float k        - Vertical size of curve
    //  * param float b        - Y-intercept(vertical shift)
    //  * param float c        - X-intercept(horizontal shift)
    public void AddConsideration(string inInput, string inCurve="QUADRATIC", float m=0.0f, float k=0.0f, float b=0.0f, float c=0.0f)
    {
        considerations.Add(new Consideration(inInput, inCurve, m, k, b, c));
    }

    // method - public: executes the Action's associated behavior //
    public void Execute()
    {
        Debug.Log("Action: " + name + " was selected.");
        if(behavior != null) //TODO this should never happen, fix to output exception instead
        {
            behavior.Execute();
        }
    }

    // method - public: returns the action's considerations //
    public List<Consideration> GetConsiderations() { return considerations; }

    // method - public: returns the name of the action //
    public string GetName() { return name; }

    // method - public: returns the name of the action's behavior //
    public string GetBehaviorName() { return behavior.GetName(); }

    // method - public: returns the action's behavior parameters //
    public string[] GetBehaviorParameters() { return behavior.GetParameters(); }
    
    // method - public: returns the appropriateness score of the given Action //
    public float GetScore()
    {
        float apprScore = 1.0f;

        foreach(Consideration curr in considerations)
        {
            apprScore *= curr.GetValue();
        }

        /*TO-DO: Normalization of appropriateness value
         */

        return apprScore;
    }

    // method - public: provides a string-based representation //
    public override string ToString()
    {
        string output = "Name: " + name + "\n     Behavior: ";
        output += (behavior != null) ? behavior.ToString() : "NONE";
        output += "\n     Considerations:\n";
        foreach (Consideration curr in considerations)
        {
            output += "          " + curr.ToString() + "\n";
        }
        return output;
    }
}
