using UnityEngine;
using UnityEngine.UI;

// DebugActionCanvas
// • visualizes the associated Unit Intelligence's Action selection process
//   · shows all of Unit's Actions and their appropriateness Score on the Canvas
public class DebugActionCanvas : MonoBehaviour
{
    // variables //

    // variables for: action-selection visualization //
    private Text actions; // connection - manual: Text object for listing Actions
    private Text scores;  // connection - manual: Text object for listing Scores




    // updating //


	// before the start: //
	void Awake () {
        // initializes Text references
        foreach (Text currText in GetComponentsInChildren<Text>())
        {
            if (currText.gameObject.name.Equals("Actions"))
                actions = currText;
            else if (currText.gameObject.name.Equals("Scores"))
                scores = currText;
        }
    }
	
	// at each update: //
	void Update () {
        // updates the text of the action-selection visualization Text objects
        actions.text = "ACTIONS:\n";
        scores.text = "SCORES\n";
        foreach(Action currAction in GetComponentInParent<UnitIntelligence>().GetActions())
        {
            actions.text += currAction.GetName() + "\n";
            scores.text += currAction.GetScore().ToString("0.00") + "\n";
        }
	}
}
