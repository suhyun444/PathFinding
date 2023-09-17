using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHistory : MonoBehaviour
{
    public delegate void Task();
    public Stack<Task> undoStack;
    
    public TaskHistory()
    {
        undoStack = new Stack<Task>();
    }
    public void AddUndoTask(Task task)
    {
        undoStack.Push(task);
    }
    public void Undo()
    {
        if(undoStack.Count == 0)return;
        Debug.Log("A");
        undoStack.Peek().Invoke();
        undoStack.Pop();
    }

}

