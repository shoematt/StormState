#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	CyclicStateMapException.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System;

namespace SW.Workflow.Engine
{
    public class CyclicStateMapException : Exception
    {
        public CyclicStateMapException ( ) : base ( "An initial state could not be determined, the graph contains unresolvable cycles and is not flagged as such" )
        {
        }
    }
}