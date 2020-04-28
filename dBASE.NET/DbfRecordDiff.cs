using System.Text;
using System.Collections.Generic;

namespace dBASE.NET
{
    public class DbfRecordDiff
    {
        public DbfRecordDiff()
        {
            
        }
        public DbfRecordDiff(DbfRecord record,int recordIndex)
        {            
            Record = record;
            RecordIndex = recordIndex;
            State = DiffState.Unmodified;            
        }        
        public int RecordIndex { get; set; }
        public DbfRecord Record { get; set; }
        public List<DbfColumnChange> ColumnsChanged { get; set; } = new List<DbfColumnChange>();       
        public DiffState State { get; set; }
        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Index: {0}, State: {1}", this.RecordIndex, this.State);

            if ((this.State == DiffState.Modified) || (this.State == DiffState.Added))
            {
                strBuilder.AppendLine();
                strBuilder.AppendLine("  {");
                foreach (DbfColumnChange columnDiff in this.ColumnsChanged)
                {
                    strBuilder.Append("    { ");
                    strBuilder.Append(columnDiff.Field.Name);

                    if (this.State == DiffState.Modified)
                    {
                        strBuilder.Append(", '");
                        strBuilder.Append(columnDiff.OldValue);
                        strBuilder.Append("'");
                    }

                    strBuilder.Append(", '");
                    strBuilder.Append(columnDiff.NewValue);
                    strBuilder.Append("'");
                    strBuilder.AppendLine("    }, ");
                }

                strBuilder.AppendLine("  }");
            }

            return strBuilder.ToString();
        }
    }
    public class DbfColumnChange
    {        
        public DbfField Field { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

    }
    public enum DiffState
    {
        Modified,
        Unmodified,
        Added         
    }
}