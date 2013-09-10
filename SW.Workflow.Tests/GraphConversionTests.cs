using NUnit.Framework;
using Orca.Workflow.UI;
using Orca.Workflow.UI.Shapes;

namespace Orca.Workflow.Tests
{
    [TestFixture]
    public class GraphConversionTests
    {
        public GraphConversionTests( )
        {
        }

        [SetUp]
        public void Setup( )
        {

        }

        public void Build_Basic_Graph( )
        {
            FlowDiagramControl flowDiagram = new FlowDiagramControl( );

            var initialState = new InitialState( );
            var dp = new DecisionPoint( );
            var finalState = new FinalState( );

            flowDiagram.AddShape( initialState );
            flowDiagram.AddShape( dp );
            flowDiagram.AddShape( finalState );

            initialState.Connection.To = dp;
            dp.True.To = finalState;
            dp.False.To = finalState;

        }

    //    [Test]
        public void Convert_From_Graph_To_StateMap( )
        {
            FlowDiagramControl flowDiagram = new FlowDiagramControl();

            var initialState = new InitialState();
            var dp = new DecisionPoint();
            var finalState = new FinalState();

            flowDiagram.AddShape( initialState );
            flowDiagram.AddShape( dp );
            flowDiagram.AddShape( finalState );

            initialState.Connection.To = dp;
            dp.True.To = finalState;
            dp.False.To = finalState;

            flowDiagram.Convert( false );
        }

        public void Serialize_StateMap( )
        {
        }
    }
}