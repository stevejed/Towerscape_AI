using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consideration
// • constitutes a Consideration, providing a weight for the associated Action's appropriateness score
//   · stores a function and input reference for encoding game state information for action selection

[System.Serializable]
public class Consideration {

    // variables //

    //variables for: function curve//
    [HideInInspector]
    public string name;   // setting - global - private: name of the Consideration (currently derives from input type)
    public enum Curves
    {
        LOGARITHMIC,
        LOGISTIC,
        QUADRATIC
    };
    [SerializeField][Tooltip("Type of curve for the Consideration.")]
    private Curves curve; // setting - global - private: type of curve for the given Consideration
    [SerializeField][Tooltip("Slope of the function\n (ex. y=<M>x + b)")]
    private float m;      // setting - global - private: slope of the Consideration function
    [SerializeField][Tooltip("Exponent of function\n (ex. y=mx^<k> + b)")]
    private float k;      // setting - global - private: vertical size of curve
    [SerializeField][Tooltip("Vertical shift\n (ex. y=mx + <b>)")]
    private float b;      // setting - global - private: y-intercept (vertical shift)
    [SerializeField][Tooltip("Horizontal shift\n (ex. y=m(x-<c>) + b)")]
    private float c;      // setting - global - private: x-intercept (horizontal shift)
    [SerializeField][Tooltip("Game-state variable Consideration derives from\n (ex. MyHealth, NearbyAllies, etc.)")]
    private Input input;  // setting - global - private: type of game state information to run through Consideration curve




    // constructors //

    // constructs an instance of a Consideration: //
    //  * param string inInput - Input type read in from Excel sheet
    //  * param string inCurve - Curve Type read in from Excel sheet
    //  * param float m        - Slope of function
    //  * param float k        - Vertical size of curve
    //  * param float b        - Y-intercept(vertical shift)
    //  * param float c        - X-intercept(horizontal shift)
    public Consideration(string inInput, string inCurve = "QUADRATIC", float m = 0.0f, float k = 0.0f, float b = 0.0f, float c = 0.0f)
    {
        //Constructs Input instance
        this.input = new Input(inInput);
        this.name = input.GetName();

        //Searches for Curve enum value of the given input
        string caps = inCurve.ToUpper();
        string[] names = System.Enum.GetNames(typeof(Curves));
        Curves[] values = (Curves[])System.Enum.GetValues(typeof(Curves));
        bool found = false;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Equals(caps))
            {
                curve = values[i];
                found = true;
                break;
            }
        }

        //Case where given input is not defined
        if (!found)
            throw new System.InvalidOperationException("No case currently in place for handling input '" + inCurve + "'.");

        this.m = m;
        this.k = k;
        this.b = b;
        this.c = c;
    }




    //Methods//

    // method - public: returns the Consideration's parameters as a string array //
    public string[] GetParameters()
    {
        string[] parameters = new string[6];
        parameters[0] = input.ToString();
        parameters[1] = curve.ToString();
        parameters[2] = m.ToString();
        parameters[3] = k.ToString();
        parameters[4] = b.ToString();
        parameters[5] = c.ToString();
        return parameters;
    }

    // method - public: gets the appropriateness score of the Consideration by running the input through the function //
    public float GetValue()
    {
        float rawValue = 0.0f;

        //Runs consideration input in order to get raw appropriateness value
        switch (curve)
        {
            case Curves.LOGARITHMIC:
                rawValue = m * Mathf.Log(input.GetValue() + c, k) + b;
                break;
            case Curves.LOGISTIC:
                rawValue = k / (1 + Mathf.Exp(-m * (input.GetValue() - c))) + b;
                break;
            case Curves.QUADRATIC:
                rawValue = m * Mathf.Pow(input.GetValue() - c, k) + b;
                break;
        }

        /*TO-DO: Normalization operations of function weights
            */

        return rawValue;
    }

    // method public: provides a string-based representation of the Consideration //
    public override string ToString()
    {
        string output = "Input: " + input.ToString();
        output += ", Curve: (" + curve.ToString() + ", M=" + m.ToString() + ", K=" + k.ToString() + ", B=" + b.ToString() + ", C=" + c.ToString() + ")";
        return output;
    }
}
