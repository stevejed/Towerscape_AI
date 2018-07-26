using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Text;

// CsvReadWrite
// • provides a set of classes for reading and writing to a CSV file
//   · CsvReader extends System.IO.StreamReader and can read in a CSV file
//   · CsvWriter extends System.IO.StreamWriter and can write to a CSV file
//     - CsvWriter can either be given a string array, which it will comma-separate, or be given a formatted string
namespace CsvReadWrite
{
    // class for reading in a CSV file
    public class CsvReader : StreamReader
    {
        // Constructors //

        // constructs a CsvReader instance w/ a given stream //
        public CsvReader(Stream stream) : base(stream) { }

        // constructs a CsvReader instance w/ a given file //
        public CsvReader(string filename) : base(filename) { }




        // Methods //

        // method - public: reads the next row of the CSV file and returns as a string array by the comma-separations //
        public string[] ReadRow()
        {
            string currLine = ReadLine();
            if (currLine != null)
                return currLine.Split(',');
            else
                return null;
        }
    }

    // class for writing to a CSV file
    public class CsvWriter : StreamWriter
    {
        // Constructors //

        // constructs a CsvWriter instance w/ a given stream //
        public CsvWriter(Stream stream) : base(stream) { }

        // constructs a CsvWriter instance w/ a given file //
        public CsvWriter(string filename) : base(filename) { }




        // Methods //

        // method - public: converts the given string array into a csv format and writes to the file //
        public void WriteRow(string[] line)
        {
            StringBuilder toWrite = new StringBuilder();
            foreach(string currItem in line)
            {
                toWrite.Append(currItem + ",");
            }
            WriteRow(toWrite.ToString(0, toWrite.Length - 1));
        }

        // method - public: writes the given string to the file //
        public void WriteRow(string line)
        {
            WriteLine(line);
        }
    }
}
