using System;
using UnityEngine;

[Serializable]
public class DialogueData
{
    public string Speaker;
    [Multiline] public string Content;
    public bool AutoNext;
    public bool NeedTyping;
    public bool CanQuickShow;
}

[CreateAssetMenu(fileName = "Node_",menuName ="Event/Message/Show Dialogue")]
public class EN_ShowDialogue : EventNodeBase
{
    public DialogueData[] datas;
    public int boxStyle = 0;
    private int _index;

    public override void Execute()
    {
        base.Execute();
        _index = 0;

        UIManager.OpenDialogueBox(ShowNextDialogue, boxStyle);
    }

    private void ShowNextDialogue(bool forceDisplayDirectly)
    {
        if (_index < datas.Length)
        {
            DialogueData data = new DialogueData() //为了添加forceDisplayDirectly新开个DialogueData
            {
                Speaker = datas[_index].Speaker,
                Content = datas[_index].Content,
                CanQuickShow = datas[_index].CanQuickShow,
                AutoNext = datas[_index].AutoNext,
                NeedTyping = !forceDisplayDirectly && datas[_index].NeedTyping //如果有forceDisplayDirectly就不能NeedTyping
            };
            UIManager.PrintDialogue(data);
            _index++;
        }
        else
        {
            state = NodeState.Finished;
            OnFinished(true);
        }
    }
}
