using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using CsvReadWrite;
using System.IO;

// BehaviorSetReader
// • class containing operations for read/write of a Unit Intelligence's action set
public class BehaviorSetReader : MonoBehaviour {

    //Variables//

    // variables for: action set read/write //
    [HideInInspector]
    public int rowsToSkip = 3;                      // setting - local: number of rows to skip (header lines)
    [HideInInspector]
    public Object actionSetFile;                    // setting - local: action set file to read/write from
    [HideInInspector]
    public UnitIntelligence unitIntelligenceScript; // connection - automatic: unit intelligence currently being read/written from
    private string filePath;                        // tracking - private: path to the action set file
    private List<string[]> headerLines;             // tracking - private: stores header lines from read-in




    //Methods//

    // method - public: reads in the action set from the associated file and sets the unit intelligence's action set to it //
    public void ReadInBehaviors()
    {
        //Error handling
        if (unitIntelligenceScript == null)
            throw new MissingReferenceException("'Unit Intelligence' instance not assigned. Please assign the file in the Inspector and try again.");
        if (actionSetFile == null)
            throw new MissingReferenceException("CSV file 'Action Set File' not assigned. Please assign the file in the Inspector and try again.");
        filePath = AssetDatabase.GetAssetPath(actionSetFile); //Gets file path
        if (filePath == "")
            throw new MissingReferenceException("CSV file 'Action Set File' could not be found.  Please assign a valid file in the Inspector and try again.");
        if (filePath.Substring(filePath.Length - 3, 3) != "csv")
            throw new MissingReferenceException("CSV file 'Action Set File' is not a valid .csv file.  Please assign a valid file in the Inspector and try again.");

        CsvReader reader = new CsvReader(filePath);
        headerLines = new List<string[]>();

        //Skips header lines
        for (int i = 0; i < rowsToSkip; i++)
        {
            headerLines.Add(reader.ReadRow());
        }

        //Initializes action list storage components
        LinkedList<Action> actionList = new LinkedList<Action>();
        Action curr = null;

        //Iterates through remaining rows for Action set
        while (true)
        {
            string[] currLine = reader.ReadRow();

            //If no more lines, then break
            if (currLine == null)
                break;

            if(currLine[0] != "")
            {
                curr = new Action(currLine[0]);
                actionList.AddLast(curr);
            }

            /*TO-DO: Develop Behavior construction method
             * 
             */

            if (currLine[4] != null)
            {
                //Adds Consideration to current Action
                float i = 0.0f;
                float cSlope = float.TryParse(currLine[6], out i) ? i : 0.0f;  //Consideration curve slope
                float cExp = float.TryParse(currLine[7], out i) ? i : 0.0f;    //Consideration curve exponent
                float cVert = float.TryParse(currLine[8], out i) ? i : 0.0f;   //Y-Intercept (vertical shift)
                float cHoriz = float.TryParse(currLine[9], out i) ? i : 0.0f; //X-Intercept (horizontal shift)

                curr.AddConsideration(currLine[4],                //Consideration name
                                      currLine[5],                //Consideration curve type
                                      cSlope, cExp, cVert, cHoriz);
            }
        }

        //Cleans up the reader
        reader.Close();
        reader.Dispose();

        //Adds actions to given Unit Intelligence
        unitIntelligenceScript.SetActions(actionList);
    }

    // method - public: writes out a unit intelligence's action set to the associated file //
    public void WriteOutBehaviors()
    {
        //Error handling
        if (unitIntelligenceScript == null)
            throw new MissingReferenceException("'Unit Intelligence' instance not assigned. Please assign the file in the Inspector and try again.");
        if (actionSetFile == null)
            throw new MissingReferenceException("CSV file 'Action Set File' not assigned. Please assign the file in the Inspector and try again.");
        filePath = AssetDatabase.GetAssetPath(actionSetFile); //Gets file path
        if (filePath == "")
            throw new MissingReferenceException("CSV file 'Action Set File' could not be found.  Please assign a valid file in the Inspector and try again.");
        if (filePath.Substring(filePath.Length - 3, 3) != "csv")
            throw new MissingReferenceException("CSV file 'Action Set File' is not a valid .csv file.  Please assign a valid file in the Inspector and try again.");

        //Reads, clears, then builds writer for file
        string[] fileLines = File.ReadAllLines(filePath);
        string tempPath = filePath.Substring(0, filePath.Length-4) + "_temp.csv";
        CsvWriter writer = new CsvWriter(tempPath);

        //Writes header lines
        for(int i = 0; i < rowsToSkip; i++)
        {
            if(fileLines.Length <= rowsToSkip)
            {
                //TODO update to read from header lines
                writer.WriteRow(fileLines[i]);
            }
            else
            {
                writer.WriteRow(fileLines[i]);
            }

        }

        //Writes in action items
        string[] actions = FormatActionsToCsv(unitIntelligenceScript.GetActions());
        for(int j = 0; j < actions.Length; j++)
        {
            writer.WriteRow(actions[j]);
        }

        //Cleans up the writer and writes to the file
        writer.Close();
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.Move(tempPath, filePath);
        File.Delete(tempPath);

        Debug.Log("Writing successful!");
    }

    // method - private: converts unit intelligence action set to CSV format //
    private string[] FormatActionsToCsv(Action[] actions)
    {
        LinkedList<string> actionStrings = new LinkedList<string>();
        foreach(Action currentAction in actions)
        {
            List<StringBuilder> currentActionStrings = new List<StringBuilder>(Mathf.Max(currentAction.GetConsiderations().Count, 0));//TODO update 0 value to be size of Behavior parameters
            for(int index = 0; index < currentActionStrings.Capacity; index++)
            {
                currentActionStrings.Add(new StringBuilder());
            }

            //Initializes values
            int place = 0;
            foreach(StringBuilder currentStringBuilder in currentActionStrings)
            {
                if(place++ == 0)
                {
                    currentStringBuilder.Append(currentAction.GetName() + ",<BEHAVIOR>,");
                }
                else
                {
                    currentStringBuilder.Append(",,");
                }
            }

            //TODO update to get behavior parameters
            place = 0;
            foreach(StringBuilder currentStringBuilder in currentActionStrings)
            {
                currentStringBuilder.Append(",,");
            }

            //Adds considerations to current action's strings
            place = 0;
            foreach(Consideration currConsideration in currentAction.GetConsiderations())
            {
                currentActionStrings[place++].Append(string.Join(",",currConsideration.GetParameters()));
            }

            //Converts StringBuilders to strings
            foreach(StringBuilder currentStringBuilder in currentActionStrings)
            {
                actionStrings.AddLast(currentStringBuilder.ToString());
            }
        }

        //Converts to array
        string[] linesToWrite = new string[actionStrings.Count];
        int i = 0;
        foreach (string curr in actionStrings)
        {
            linesToWrite[i++] = curr;
        }
        return linesToWrite;
    }
}
