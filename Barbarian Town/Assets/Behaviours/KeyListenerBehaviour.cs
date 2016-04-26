using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KeyListenerBehaviour : MonoBehaviour
{
    public List<KeyBind> bindings;
}

[Serializable]
public class KeyBind
{
    public KeyCode code;
    public BaseBehaviour listener;
}