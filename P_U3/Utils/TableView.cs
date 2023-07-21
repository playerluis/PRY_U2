using System.Collections;
using P_U3.Models;

namespace P_U3.Utils;

public class TableView<T> : IEnumerable<T> where T : IModeable
{
    public string Tittle { get; set; }
    public string[] ColumnNames { get; }
    

    private List<T> Data { get; }
    

    public TableView(string tittle, string[] columnNames, List<T> data)
    {
        if (data == null)
            throw new Exception("Data must not be null");
        if (columnNames == null)
            throw new Exception("Column names must not be null");

        if (columnNames.Length == 0)
            throw new Exception("Column names must not be empty");

        var enumerable = data as T[] ?? data.ToArray();
        var tableables = data as T[] ?? enumerable.ToArray();

        if (!tableables.Any())
            throw new Exception("Data must not be empty");

        if (tableables.Length != enumerable.Length)
            throw new Exception("Data and tableables must be the same length");


        ColumnNames = columnNames;
        Data = data;
        Tittle = tittle;
    }

    public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}