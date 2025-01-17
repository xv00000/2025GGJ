using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public int _student_id;
    public Sprite _currentSprite;
    public List<string> Courses;
    public int _x;
    public int _y;
    public int _z;
    public StudentState _currentState;

    public enum StudentState
    {
        Idle,
        Wake,
        Study
    }

    // 构造函数
    public Student(int id, int x, int y, int z, Sprite sprite)
    {
        _student_id = id;
        Courses = new List<string>();
        _x = x;
        _y = y;
        _z = z;
        _currentSprite = sprite;
        _currentState = StudentState.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    public void ChangeState(StudentState newState, Sprite sprite)
    {
        _currentState = newState;
        _currentSprite = sprite;
    }
    // Update is called once per frame
    void Update()
    {
        // 可以在这里添加每帧更新的逻辑
    }






  
}
