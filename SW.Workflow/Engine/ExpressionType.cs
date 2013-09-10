#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	ExpressionType.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.Engine
{
    [ Serializable ]
    public enum ExpressionType
    {
        Is,
        And,
        Or,
        Not,
        Custom,
        Evaluate
    }
}