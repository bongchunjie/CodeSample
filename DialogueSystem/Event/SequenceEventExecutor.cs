using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Executor_",menuName ="Event/Sequence Executor")]
public class SequenceEventExecutor : ScriptableObject
{
    public Action<bool> OnFinished; // bool参数代表执行器执行是否成功

    private int _index;
    public EventNodeBase[] nodes;

    public void Init(Action<bool> onFinishedEvent)
    {
        _index = 0;

        foreach (EventNodeBase item in nodes)
        {
            if (null != item)
            {
                item.Init(OnNodeFinished);
            }
        }

        OnFinished = onFinishedEvent;
    }

    private void OnNodeFinished(bool success)
    {
        if (success)
        {
            ExecuteNextNode();
        }
        else
        {
            OnFinished(false);
        }
    }

    private void ExecuteNextNode()
    {
        if (_index < nodes.Length)
        {
            if (nodes[_index].state == NodeState.Waiting)
            {
                nodes[_index].Execute();
                _index++;
            }
        }
        else
        {
            OnFinished(true);
        }
    }

    public void Execute()
    {
        _index = 0;
        ExecuteNextNode();
    }
}
