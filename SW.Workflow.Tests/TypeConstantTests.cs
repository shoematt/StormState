using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Orca.Core.Constants;
using Orca.Core.Extensions;

namespace Orca.Workflow.Tests
{
    [ TestFixture ]
    public class TypeConstantTests
    {
        private readonly List<string> _failedTypes = new List<string>( );

        /// <summary>
        ///   Verifies the type string.
        /// </summary>
        /// <param name = "typeString">The type string.</param>
        public void VerifyTypeString( string typeString )
        {
            Type type = Type.GetType( typeString );
            if ( type == null )
            {
                _failedTypes.Add( typeString );
            }
        }

        /// <summary>
        ///   Verifies the type strings.
        /// </summary>
        [ Test ]
        public void VerifyTypeStrings( )
        {
            VerifyTypeString( TypeConstants.ActionProviders.ActionBlockActionProvider );
            VerifyTypeString( TypeConstants.ActionProviders.DecisionPointActionProvider );
            VerifyTypeString( TypeConstants.ActionProviders.ShapeBaseActionProvider );

            VerifyTypeString( TypeConstants.Painters.ActionBlockPainter );
            VerifyTypeString( TypeConstants.Painters.ConnectionPainter );
            VerifyTypeString( TypeConstants.Painters.DecisionPointShapePainter );
            VerifyTypeString( TypeConstants.Painters.PolygonShapePainter );

            VerifyTypeString( TypeConstants.Editors.EventHookSelector );
            VerifyTypeString( TypeConstants.Editors.ActionBlockEditor );
            VerifyTypeString( TypeConstants.Editors.DecisionPointExpressionEditor );
            VerifyTypeString( TypeConstants.Editors.ReturminatorEditor );
            VerifyTypeString( TypeConstants.Editors.UpdatePropertyActionEditor );
            VerifyTypeString( TypeConstants.Editors.ExpressionDescriptorUITypeEditor );
            VerifyTypeString( TypeConstants.Editors.WorkTypeReferenceEditor );

            VerifyTypeString( TypeConstants.SystemVariableDescriptionProvider );
            VerifyTypeString( TypeConstants.ObjectCollectionValueProvider );

            VerifyTypeString( TypeConstants.ServiceProviders.FlowDiagramServices );

            VerifyTypeString( TypeConstants.TypeDescriptionProviders.ObjectCollectionTypeDescriptionProvider );
            // VerifyTypeString( TypeConstants.TypeDescriptionProviders.SystemVariableTypeDescriptionProvider );

            VerifyTypeString( TypeConstants.CommandHandlers.PolygonFactoryCommandHandler );
            VerifyTypeString( TypeConstants.CommandHandlers.ShapeFactoryCommandHandler );

            StringBuilder failMsg = new StringBuilder( );

            _failedTypes.CallOnEach( x => failMsg.Append( x + " , " ) );

            Assert.AreEqual( 0, _failedTypes.Count, failMsg.ToString( ) );
        }
    }
}