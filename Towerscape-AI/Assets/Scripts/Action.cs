using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Encapsulating class for a possible Action of a unit
public class Action {

    //Variables//

    private string                    name;           //Name argument passed in for Action
    private Behavior                  behavior;       //Behavior instance for Action
    private LinkedList<Consideration> considerations; //List of considerations to determine Action's appropriateness


    //Constructor//

    /*Constructor for an Action
        * param string          inName           - Name of Action read in from Excel sheet
        * param inBehavior      inBehavior       - Name of Behavior
        * param Consideration[] inConsiderations - 
        */
    public Action(string inName = "N/a", Behavior inBehavior = null)
    {
        this.name = inName;
        this.behavior = inBehavior;
        this.considerations = new LinkedList<Consideration>;
    }


    //Methods//

    /*Adds consideration to the Action's consideration list
     * param string inInput - Input type read in from Excel sheet
     * param string inCurve - Curve Type read in from Excel sheet
     * param float  m       - Slope of function
     * param float  k       - Vertical size of curve
     * param float  b       - Y-intercept (vertical shift)
     * param float  c       - X-intercept (horizontal shift)
     */
    public void AddConsideration(string inInput, string inCurve="QUADRATIC", float m=0.0f, float k=0.0f, float b=0.0f, float c=0.0f)
    {
        considerations.AddLast(new Consideration(inInput, inCurve, m, k, b, c));
    }

    //Executes Action's associated Behavior
    public void Execute()
    {
        behavior.Execute();
    }

    //Gets the appropriateness of the given Action
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


    //Inputs that impact an Action's appropriateness score
    private class Consideration
    {

        //Variables//

        private enum Curves
        {
            LOGARITHMIC,
            LOGISTIC,
            QUADRATIC
        };

        //Instance
        private Curves curve; //Function curve type
        private float m;      //Slope of function
        private float k;      //Vertical size of curve
        private float b;      //Y-intercept (vertical shift)
        private float c;      //X-intercept (horizontal shift)
        private Input input;  //Input instance for accessing game state information to run through consideration function


        //Constructor//

        /*Constructs consideration instance
         * param string inInput - Input type read in from Excel sheet
         * param string inCurve - Curve Type read in from Excel sheet
         * param float  m       - Slope of function
         * param float  k       - Vertical size of curve
         * param float  b       - Y-intercept (vertical shift)
         * param float  c       - X-intercept (horizontal shift)
         */
        public Consideration(string inInput, string inCurve = "QUADRATIC", float m = 0.0f, float k = 0.0f, float b = 0.0f, float c = 0.0f)
        {
            //Constructs Input instance
            this.input = new Input(inInput);

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
        }


        //Methods//

        //Returns the appropriateness value of the Consideration instance
        public float GetValue()
        {
            float rawValue;

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
    }
}
