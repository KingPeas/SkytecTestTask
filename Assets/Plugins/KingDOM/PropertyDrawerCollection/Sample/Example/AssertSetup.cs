using System;
using System.Reflection;
using UnityEngine;

[ExecuteInEditMode]
public class AssertSetup:MonoBehaviour {

    public enum CompareType
    {
        [EnumLabel("Not checking")]
        None,
        [EnumLabel("<")]
        Less,
        [EnumLabel("<=")]
        LessOrEqual,
        [EnumLabel("==")]
        Equal,
        [EnumLabel(">=")]
        MoreOrEqual,
        [EnumLabel(">")]
        More,
        [EnumLabel("!=")]
        NotEqual,
        [EnumLabel("Value is set")]
        NotEmpty
    }
    [TypePopup]
    [PropertyArgs(tip = "Seeking script in the tree of visualization")]
    public string script = "";
    public CompareValue[] compareValues = new CompareValue[0];

    //private string oldType = null;

    public bool Check(object obj)
    {
        bool res = true;
        foreach (CompareValue compareValue in compareValues)
        {
            res = res && checkCompare(compareValue, obj);
            if (!res) break;
        }
        return res;
    }

    bool checkCompare(CompareValue c, object obj)
    {
        object val;
        bool isSet = false;
        if (c.compare == CompareValue.CompareType.None)
            return true;

        if (string.IsNullOrEmpty(c.property))
            return true;


        FieldInfo field = obj.GetType().GetField(c.property);
        if (field != null)
        {
            val = field.GetValue(obj);
            isSet = true;
        }
        else
        {
            PropertyInfo prop = obj.GetType().GetProperty(c.property);
            if (prop != null)
            {
                val = prop.GetValue(obj, null);
                isSet = true;
            }
            else
            {
                val = null;
            }
        }

        if (isSet)
        {
            return Check2Value(c, val);
        }
        else
            return false;
    }


    bool Check2Value(CompareValue c, object val)
    {
        Type t = val.GetType();
        try
        {
			object val2;
			if (t.IsEnum)
				val2 = Enum.Parse(t, c.propertyValue);
			else
				val2 = Convert.ChangeType(c.propertyValue, t);

            switch (c.compare)
            {
                case CompareValue.CompareType.NotEqual:
                    return val != val2;
                    break;
                case CompareValue.CompareType.NotEmpty:
                    if (t.IsEnum)
                        return false;
                    if (t == typeof(bool))
                        return (bool)val == false;
                    if (t.IsValueType)
                    {
                        val2 = Assembly.GetExecutingAssembly().CreateInstance(t.FullName);
                    }
                    else
                    {
                        val2 = null;
                    }
                    return val != val2;
                    break;

            }

            if (val is IComparable)
            {
                int res = (val as IComparable).CompareTo(val2);
                switch (c.compare)
                {
                    case CompareValue.CompareType.Less:
                        return res < 0;
                        break;
                    case CompareValue.CompareType.LessOrEqual:
                        return res <= 0;
                        break;
                    case CompareValue.CompareType.More:
                        return res > 0;
                        break;
                    case CompareValue.CompareType.MoreOrEqual:
                        return res >= 0;
                        break;
                    case CompareValue.CompareType.Equal:
					return res == 0;
					break;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }

        
        
        return false; 
    }
}
