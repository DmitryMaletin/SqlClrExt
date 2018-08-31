using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Diagnostics;

public partial class PerfCounters
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SetInstanceCounter(SqlString CategoryName, SqlString CounterName, SqlString InstanceName, Int64 Value)
    {
        PerformanceCounter counter = new PerformanceCounter();
        counter.CategoryName = CategoryName.ToString();
        counter.CounterName = CounterName.ToString();
        counter.InstanceName = InstanceName.ToString();
        counter.ReadOnly = false;
        counter.RawValue = Value;
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void IncrimentInstanceCounter(SqlString CategoryName, SqlString CounterName, SqlString InstanceName, Int64 Value)
    {
        PerformanceCounter counter = new PerformanceCounter();
        counter.CategoryName = CategoryName.ToString();
        counter.CounterName = CounterName.ToString();
        counter.InstanceName = InstanceName.ToString();
        counter.ReadOnly = false;
        counter.IncrementBy(Value);
    }
}
