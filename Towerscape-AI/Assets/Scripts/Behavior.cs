using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavior
// • constitutes an abstract class from which all Behaviors inherit
public abstract class Behavior : MonoBehaviour {

    //Variables//

    public enum Behaviors
    {
        /*TO-DO: List out possible behaviors
         * 
         */
    };




    //Constructors//

    public Behavior()
    {

    }


    //Methods//

    //Executes the behavior's operation on the given unit
    public void Execute()
    {
        return;
    }
}
