using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Block", menuName = "ScriptableObjects/Dialog Block")]
    public class DialogBlock : ScriptableObject
    {
        public string blockText;
        public Actor actor;
        public List<Answer> answers;
    }
    
    [Serializable]
    public class Answer
    {
        public string text;
        public DialogBlock nextDialogBlock;
    }
}