using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input
// • returns a particular type of game-state information based on the input type
[System.Serializable]
public class Input
{


    // variables //

    //variables for: input operation
    private string name;    // setting - global - private: name provided at initialization
    public  enum   Inputs   // setting - global - provided: enumeration of all possible inputs
    {
        MYHEALTH,           //Health of associated unit
        NEARBYALLIES,       //Count of nearby allies
        DISTANCETOENEMY,    //Distance to a given Enemy unit
        DISTANCETOOBJECTIVE //Distance to a given Objective object
    };
    private Inputs input;   // setting - global - private: type of game-state information this instance needs to use




    //Constructors//


    // constructs an input instance //
    // * param inName - name to be assigned to the input; also used for assigning Inputs type
    public Input(string inName)
    {
        name = inName;

        //Searches for Input enum value of the given input
        string caps = inName.ToUpper();
        string[] names = System.Enum.GetNames(typeof(Inputs));
        Inputs[] values = (Inputs[])System.Enum.GetValues(typeof(Inputs));
        bool found = false;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Equals(caps))
            {
                input = values[i];
                found = true;
                break;
            }
        }

        //Case where given input is not defined
        if (!found)
            throw new System.InvalidOperationException("No case currently in place for handling input '" + inName + "'.");
    }




    //Methods//


    // method - public: returns the Inputs enum value of the instance //
    public Inputs GetInput() { return input; }

    // method - public: returns the name of the instance //
    public string GetName() { return name; }

    // method - public: returns the value of the given game-state information //
    public float GetValue()
    {
        switch (input)
        {
            /* TO-DO:
             *  > Include functions for the necessary Inputs enum values
             */
        }

        return 0f;
    }

    // method - public: returns a string representation of the input //
    public override string ToString() { return input.ToString(); }
}
