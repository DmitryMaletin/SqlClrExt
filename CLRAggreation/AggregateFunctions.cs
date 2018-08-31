using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
    Format.UserDefined, 
    IsInvariantToOrder = false, 
    IsInvariantToNulls = true,  
    IsInvariantToDuplicates = false, 
    MaxByteSize = -1)]
public struct Concatenate : IBinarySerialize
{
    private StringBuilder sb;
    private string del;

    public Boolean IsNull { get; private set; }
    public void Init()
    {
        sb = new StringBuilder();
        del = string.Empty;
        this.IsNull = true;
    }

    public void Accumulate(SqlString Value, SqlString Delimiter)
    {
        if (!Delimiter.IsNull & Delimiter.Value.Length > 0)
        {
            del = Delimiter.Value;
            if (sb.Length > 0) sb.Append(Delimiter.Value);
        }
        sb.Append(Value.Value);
        if (Value.IsNull == false) this.IsNull = false;
    }

    public void Merge(Concatenate Group)
    {
        if (sb.Length > 0 & Group.sb.Length > 0) sb.Append(del);
        sb.Append(Group.sb.ToString());
    }

    public SqlString Terminate()
    {
        return new SqlString(sb.ToString());
    }

    void IBinarySerialize.Read(System.IO.BinaryReader r)
    {
        del = r.ReadString();
        sb = new StringBuilder(r.ReadString());

        if (sb.Length != 0) this.IsNull = false;
    }

    void IBinarySerialize.Write(System.IO.BinaryWriter w)
    {
        w.Write(del);
        w.Write(sb.ToString());
    }
}
