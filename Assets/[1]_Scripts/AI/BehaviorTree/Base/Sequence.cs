
using System.Collections.Generic;

namespace SA.AI.BT
{
    public class Sequence : Node
    {
        protected List<Node> nodes = new List<Node>();


        public Sequence(List<Node> nodes )
        {
            this.nodes = nodes;
        }
        
        
        public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;

            foreach(var node in nodes)
            {
                switch(node.Evaluate())
                {
                    case NodeState.RUNNING:
                        isAnyNodeRunning = true;
                    break;

                    case NodeState.SUCCESS:
                    break;

                    case NodeState.FAILURE:
                        nodeState = NodeState.FAILURE;
                        return nodeState;
                    default:
                    break;
                }
            }

            nodeState = (isAnyNodeRunning) ? NodeState.RUNNING : NodeState.SUCCESS;

            return nodeState;
        }
    }
}