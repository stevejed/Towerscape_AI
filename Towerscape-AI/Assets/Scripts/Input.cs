using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input
// > Handles a Consideration's input argument
//    - Each Consideration has one Input
//    - Gets the value of the associated Input type for the given unit
public class Input {

    //Variables//

    public  enum   Inputs   //Enumeration of all input types
    {
        MYHEALTH,           //Health of associated unit
        NEARBYALLIES,       //Count of nearby allies
        DISTANCETOENEMY,    //Distance to a given Enemy unit
        DISTANCETOOBJECTIVE //Distance to a given Objective object
    };

    //Instance variables
    private Inputs input;   //Inputs enum value of the instance
    private string name;    //Name of the input passed in from the imported sheet


    //Constructors//

    /*Constructs an input instance with the given Inputs enum type
     * param string inName - name of the input to define
     */
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

    //Returns the Inputs enum value of the given instance
    public Inputs GetInput()
    {
        return input;
    }

    //Returns the name of the input passed in from the imported sheet
    public string GetName()
    {
        return name;
    }

    //Gets the value of the input based on the Inputs enum value
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
}
