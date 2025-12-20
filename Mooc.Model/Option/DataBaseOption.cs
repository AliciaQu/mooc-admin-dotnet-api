using Mooc.Shared.Enum;

namespace Mooc.Model.Option;

public class DataBaseOption
{
    public const string Section = "DataBase";

    public bool DataSeed { get; set; }
    public DBType Type {  get; set; } 

    /// <summary>
    /// 
    /// </summary>
    public string ConnectionString {  get; set; }
}
