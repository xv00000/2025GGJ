using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.U2D;
public enum StudentState
    {
        Idle,
        Distract,
        Amaze
    }
public class Student : MonoBehaviour
{
    public StudentScript studentScript;
    public int _student_id;
    public SpriteRenderer spriteRenderer;
    public List<string> Courses;
    public StudentState _currentState;
    // ¹¹Ôìº¯Êý
    public void Init(StudentScript studentScript)
    {
        this.studentScript = studentScript;
        spriteRenderer = GetComponent<SpriteRenderer>();    
        spriteRenderer.sprite = studentScript.IdleSprite;
        _currentState = StudentState.Idle;
    }

    public void ChangeState(StudentState newState)
    {
        _currentState = newState;
        switch (newState) { 
            case StudentState.Idle: spriteRenderer.sprite = studentScript.IdleSprite; break;
            case StudentState.Distract: spriteRenderer.sprite = studentScript.DistractSprite; break;
            case StudentState.Amaze: spriteRenderer.sprite = studentScript.AmazeSprite; break;
        }
    }





  
}
