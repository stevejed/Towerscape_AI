using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using Excel = Microsoft.Office.Interop.Excel;

//Class for accessing an Excel file and its information
public class ExcelReader : MonoBehaviour {
    private Excel.Application app;      //Excel application
    private Excel.Workbook workbook;    //current Excel workbook
    private Excel._Worksheet currSheet; //current Excel worksheet
    private Excel.Range range;          //worksheet range
    public int rowCount = 0;            //current Excel worksheet row count
    public int colCount = 0;            //current Excel worksheet col count

    //Excel file reader constructor
    //  filePath - directory for the excel file to read
    public ExcelReader(string filePath) {
        if (filePath == null)
            throw new ArgumentException("The provided path does not point to a valid Excel file (.xlsx).");
        Excel._Application xlApp = new Excel.Application();
        this.workbook = xlApp.Workbooks.Open(@filePath);
        this.currSheet = (Excel._Worksheet) this.workbook.Sheets[1];
    }

    //Excel file reader constructor for handling given GameObject instance
    public ExcelReader(UnityEngine.Object file) : this(UnityEditor.AssetDatabase.GetAssetPath(file)){ }

    //Gets a reference to the worksheet at the given index
    //  index    - index of worksheet to access (count starts at 1)
    public void GetSheet(int index)
    {
        if(index < 1 || index > this.workbook.Sheets.Count)
        {
            throw new IndexOutOfRangeException("index (" + index.ToString() + ") out of bounds, remember index count starts at 1.");
        }
        this.currSheet = (Excel._Worksheet) this.workbook.Sheets[index];
        this.range = this.currSheet.UsedRange;
        this.rowCount = range.Rows.Count;
        this.colCount = range.Columns.Count;
    }

    //Gets the value of a given cell's value
    //  row      - index of accessing cell's row (count starts at 1)
    //  col      - index of accessing cell's col (count starts at 1)
    public object GetValue(int row, int col)
    {
        if (range.Cells[row, col] != null) {
            Excel.Range temp = (Excel.Range) this.range.Cells[row, col];
            return temp.Value2;
        }else
            return null;
    }

    //Performs all closing operations for Excel file access
    public void Cleanup()
    {
        //cleanup
        GC.Collect();
        GC.WaitForPendingFinalizers();

        //release com objects to fully kill excel process from running in the background
        Marshal.ReleaseComObject(this.range);
        Marshal.ReleaseComObject(this.currSheet);

        //close and release
        workbook.Close();
        Marshal.ReleaseComObject(this.workbook);

        //quit and release
        this.app.Quit();
        Marshal.ReleaseComObject(this.app);
    }

    //public static void main(string[] args){
    //    excelReader test = new excelReader('');
    //}
}
