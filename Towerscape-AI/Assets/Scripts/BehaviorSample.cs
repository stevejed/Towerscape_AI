using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BehaviorSample
// • sample Behavior class for showcasing how to develop a Behavior
public class BehaviorSample : Behavior {

    // Variables //

    // variables for: behavior inheritance //
    private string name;                           // setting - global - private: name of the behavior
    private Dictionary<string, string> parameters; // setting - global - private: parameters of the behavior

    // variables for: class-specific operation //
    // ****NOTE**** Place any class-specific variables (Target, Speed, etc.) here

    // Constructors //

    // method - public: instantiates a behavior move instance //
    //   * param inName   - Name of the behavior 
    //   * param inParams - Set of parameters
    public BehaviorSample(string inName = "NONE", Dictionary<string, string> inParams = null) : base(inName, inParams)
    {
        //****NOTE**** Assign any class-specific variables (Target, Speed, etc.) here
    }




    // Methods //

    // method - public: executes the behavior operation associated with the given behavior class //
    public override void Execute()
    {
        Debug.Log("Behavior: " + name + " has been executed!");
        //****NOTE **** Perform any behavior execution operations here
    }
}
