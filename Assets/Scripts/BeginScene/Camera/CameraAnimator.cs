using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;
    private Action overCallBack;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TurnLeft(Action callBack)
    {
        animator.SetTrigger("TurnLeft");
        overCallBack = callBack;
    }
    public void TurnRight(Action callBack)
    {
        animator.SetTrigger("TurnRight");
        overCallBack = callBack;
    }
    public void PlayOver()
    {
        overCallBack?.Invoke();
        overCallBack = null;
    }
}
