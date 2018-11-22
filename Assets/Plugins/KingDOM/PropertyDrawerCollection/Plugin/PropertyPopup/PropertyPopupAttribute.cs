/// <summary>
/// Property Name
/// </summary>
public class PropertyPopupAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Source for target selection
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// By type name
        /// </summary>
        TypeName,
        /// <summary>
        /// From field with type name
        /// </summary>
        FieldTypeName,
        /// <summary>
        /// From field with target for getting type
        /// </summary>
        FieldTarget,
        /// <summary>
        /// Type calculated in property 
        /// </summary>
        CalculatedType
    }
    
    //public int selectedValue = 0;
    /// <summary>
    /// Source for target selection
    /// </summary>
    public SourceType sourceType = SourceType.FieldTarget;
    /// <summary>
    /// Source name (by type name or field name)
    /// </summary>
    public string sourcePropertyName = "";
    /// <summary>
    /// Use only writeable field
    /// </summary>
    public bool canSet = false;
    /// <summary>
    /// Selection from a list of available properties
    /// </summary>
    /// <param name="sourceType">Source for target selection</param>
    /// <param name="sourcePropertyName">Source name (by type name or field name)</param>
    /// <param name="canSet">Use only writeable field</param>
    public PropertyPopupAttribute(SourceType sourceType, string sourcePropertyName, bool canSet = false)
    {
        this.sourceType = sourceType;
        if (!string.IsNullOrEmpty(sourcePropertyName))
            this.sourcePropertyName = sourcePropertyName;
        this.canSet = canSet;
    }
    /// <summary>
    /// Selection from a list of available properties
    /// </summary>
    /// <param name="sourceType">Source for target selection</param>
    /// <param name="canSet">use only writeable field</param>
    public PropertyPopupAttribute(SourceType sourceType, bool canSet = false)
        : this(sourceType, "", canSet)
    {
        
    }

}
