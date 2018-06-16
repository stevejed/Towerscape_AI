using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel = Microsoft.Office.Interop.Excel;

//Script for parsing in Unit Intelligence scripts from given Excel Behavior Set
public class BehaviorSetReader : MonoBehaviour {

    //Variables//

    private ExcelReader        excelReader;
    public  Object             set;
    public  UnitIntelligence[] unitIntelligenceScripts;


    //Methods//

    /*Passes in the Action list
     * 
     */
    private void ParseUnit(int index)
    {
        UnitIntelligence unit = unitIntelligenceScripts[index];
        excelReader.GetSheet(index);

        LinkedList<Action> actionList = new LinkedList<Action>();
        Action curr = null;

        for (int row = 4; row < excelReader.rowCount; row++)
        {
            if (excelReader.GetValue(row, 1) != null)
            {
                curr = new Action(excelReader.GetValue(row, 1).ToString());
                actionList.AddLast(curr);
            }

            /*TO-DO: Develop Behavior construction method
             * 
             */

            //Adds to considerations
            curr.AddConsideration((string) excelReader.GetValue(row, 5),
                                  (string) excelReader.GetValue(row, 6),
                                  (float)  excelReader.GetValue(row, 7),
                                  (float)  excelReader.GetValue(row, 8),
                                  (float)  excelReader.GetValue(row, 9),
                                  (float)  excelReader.GetValue(row, 10));
        }

        //Adds actions to current unit script
        unit.AddActions(actionList);
    }


    //Updates//

    void Start () {
        excelReader = new ExcelReader(set);

        for (int i = 0; i < unitIntelligenceScripts.Length; i++)
            ParseUnit(i);
	}


}
