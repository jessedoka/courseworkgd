using UnityEngine;
using System;
using System.Collections;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse 

// https://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/

// modified by: Sebastian Lague

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string conditionalSourceField;
    public int enumIndex;

    public ConditionalHideAttribute(string boolVariableName)
    {
        conditionalSourceField = boolVariableName;
    }

    public ConditionalHideAttribute(string enumVariableName, int enumIndex)
    {
        conditionalSourceField = enumVariableName;
        this.enumIndex = enumIndex;
    }

}


