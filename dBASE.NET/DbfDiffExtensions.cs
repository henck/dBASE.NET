using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace dBASE.NET
{
    public static class DbfDiffExtensions
    {
        /// <summary>        
        /// Gets the difference between the actual and given dbf file        
        /// </summary>
        /// <param name="actualDbf">the current open dbf file</param>
        /// <param name="targetDbf">the dbf file to be compared</param>
        /// <returns></returns>
        public static List<DbfRecordDiff> GetDiff(this Dbf actualDbf,Dbf targetDbf)
        {            
            var dbfRecordDiff = new List<DbfRecordDiff>();            

            for (int i = 0; i < actualDbf.Records.Count; i++)
            {
                var diff = GetRecordDiff(actualDbf,actualDbf.Records[i],targetDbf.Records[i],i);
                if(diff.State == DiffState.Modified){
                    dbfRecordDiff.Add(diff);
                }
            }   
            if(actualDbf.Records.Count < targetDbf.Records.Count){                
                dbfRecordDiff.AddRange(targetDbf.Records.Select((r,i) => new DbfRecordDiff{
                    RecordIndex = i,
                    Record = r,
                    State = DiffState.Added,
                    ColumnsChanged = r.Data.Select((d,i) => new DbfColumnChange{
                        Field = actualDbf.Fields[i],
                        OldValue = d,
                        NewValue = d
                    }).ToList()
                }));
            }
            return dbfRecordDiff;
        }
        public static DbfRecordDiff GetRecordDiff(Dbf dbf, DbfRecord actualRecord,DbfRecord newRecord,int index)
        {
            if(actualRecord.Data.SequenceEqual(newRecord.Data)){
                return new DbfRecordDiff(newRecord,index);
            }            
            var recordDiff = new DbfRecordDiff();
            for (int i = 0; i < dbf.Fields.Count; i++){
                var columnActualData = ObjectToByteArray(actualRecord.Data[i]);
                var columnNewData = ObjectToByteArray(newRecord.Data[i]);    
                if(!columnActualData.SequenceEqual(columnNewData)){
                    recordDiff.ColumnsChanged.Add(new DbfColumnChange{                        
                        Field = dbf.Fields[i],
                        OldValue = actualRecord.Data[i],
                        NewValue = newRecord.Data[i]
                    });                    
                }
            }            
            recordDiff.State = DiffState.Modified;
            recordDiff.Record = newRecord;
            return recordDiff;
            

            static byte[] ObjectToByteArray(object obj)
            {
                if(obj == null)
                    return null;
                var bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
        }
        
        
    }
}