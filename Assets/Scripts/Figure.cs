using UnityEngine;
using System.Collections;

public class Figure : MonoBehaviour
{
    private Rigidbody body;
    private int type = -1;

    private  void OnMouseDown()
    {
        GetHit();
        Debug.Log("Hit");
    }

    private void ResetFalling()
    {
        body.velocity = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
    }

    private void GetHit()
    {
        ResetFalling();
        GameManager.Instance.CountHit(type, this);
    }

    private void ResetFigure()
    {
        ResetFalling();
        GameManager.Instance.JustAddFigure(type, this);
    }

    public void InitComponent(int _type)
    {
        body = GetComponent<Rigidbody>();
        type = _type;
    }

    private void OnBecameInvisible()
    {
        if(transform.parent == null)
        {
            ResetFigure();
        }
    }

}
