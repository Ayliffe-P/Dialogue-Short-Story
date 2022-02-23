using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public virtual IEnumerator Dancing()
    {
        yield break;
    }

    public virtual IEnumerator Idle()
    {
        yield break;
    }

    public virtual IEnumerator Walk()
    {
        yield break;

    }
    public virtual IEnumerator Talking()
    {
        yield break;

    }
}
