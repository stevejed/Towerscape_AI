using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavior
// • constitutes an abstract class from which all Behaviors inherit
public abstract class Behavior {

    //Variables//

    public enum Behaviors //TODO document this; what's the standard for enums and their values?
    {
        NONE,
        MOVE,
        /*TO-DO: List out possible behaviors
         * 
         */
         // ****NOTE**** Place an enum value here (in all caps) corresponding to the behavior type requested by the designers
    };
    private string name;                           // setting - global - private: name of the behavior
    private Dictionary<string, string> parameters; // setting - global - private: set of parameters for rewriting to the file




    //Constructors//

    // method - public: instantiates a behavior instance; provides a source for adding operations to all inheriting behavior classes //
    //   * param inName   - Name of the behavior 
    //   * param inParams - Set of parameters
    protected Behavior(string inName = "NONE", Dictionary<string, string> inParams = null)
    {
        name = inName;
        parameters = inParams;
    }




    //Methods//

    // method - public: executes the behavior's operation on the given unit //
    public abstract void Execute();

    // method - public: gets the name of the behavior //
    public string GetName(){ return name; }

    //method - public: returns a string array of the behavior parameters for //
    public string[] GetParameters()
    {
        string[] outParams = new string[parameters.Count];
        int i = 0;
        foreach(KeyValuePair<string, string> entry in parameters)
        {
            outParams[i++] = entry.Key + "," + entry.Value;
        }
        return outParams;
    }

    // method - public: returns a string representation of the behavior //
    public override string ToString()
    {
        string output = name;
        foreach(KeyValuePair<string, string> param in parameters)
        {
            output += "\n          " + param.Key + ": " + param.Value;
        }
        return output;
    }
}
