using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class CSSequenceManager : CSEvent
{
    int step;
    public CSEvent[] sequence;

    public int Step
    {
        get { return step; }

        set
        {
            if (value >= sequence.Length)
                return;

            step = value;
            ExecuteStep(step);
        }
    }

    void Awake()
    {
        step = -1;
        //sequence = GetComponentsInChildren<CSEvent>().Where(x => x.gameObject != gameObject).ToArray();
    }

    void ExecuteStep(int st)
    {
        if (st >= sequence.Length)
            return;

        sequence[st].NextStep = CompleteStep;
        sequence[st].OnEventAction();
    }

    public void CompleteStep()
    {
        sequence[Step].NextStep = null;
        Step++;
    }

    public override void OnEventAction()
    {
        if(sequence.Length > 0)
            Step = 0;
    }


}
