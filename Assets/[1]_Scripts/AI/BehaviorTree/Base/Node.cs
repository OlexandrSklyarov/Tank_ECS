
namespace SA.AI.BT
{   
    public enum NodeState
    {
        RUNNING, SUCCESS, FAILURE
    }

    [System.Serializable]
    public abstract class Node
    {
        public NodeState NodeState => nodeState;

        protected NodeState nodeState;

        public abstract NodeState Evaluate();
    }
}