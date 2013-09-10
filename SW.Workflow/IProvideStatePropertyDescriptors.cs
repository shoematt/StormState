#region StormWorks Software LLC

// 
// StormWorks Software LLC :: Copyright (c) 2012 
// 
// SW
// SW.Workflow
// 
// File:	IProvideStatePropertyDescriptors.cs
// Author:	Matthew Shoemate
// Date:	08/27/2012
// 
// http://www.StormWorksSoftware.com
// 

#endregion

using System.ComponentModel;

namespace SW.Workflow
{
    public interface IProvideStatePropertyDescriptors
    {
        PropertyDescriptorCollection GetProperties ( );
    }
}