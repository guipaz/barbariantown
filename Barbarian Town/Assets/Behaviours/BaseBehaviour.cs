using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public virtual void Perform() { }

    public virtual void Perform(KeyCode code) { }
}
