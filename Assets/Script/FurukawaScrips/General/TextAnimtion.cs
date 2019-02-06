using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class ListPooling<T>
{
    //public static List<T> Get();
    //public static void Release(List<T> toRelease);

    private static readonly ObjectPooling<List<T>> s_ListPool = new ObjectPooling<List<T>>(null, l => l.Clear());

    public static List<T> Get()
    {
        return s_ListPool.Get();
    }

    public static void Release(List<T> toRelease)
    {
        s_ListPool.Release(toRelease);
    }
}

public class ObjectPooling<T> where T : new()
{
    private readonly Stack<T> m_Stack = new Stack<T>();
    private readonly UnityAction<T> m_ActionOnGet;
    private readonly UnityAction<T> m_ActionOnRelease;

    public int countAll { get; private set; }
    public int countActive { get { return countAll - countInactive; } }
    public int countInactive { get { return m_Stack.Count; } }

    public ObjectPooling(UnityAction<T> actionOnGet,UnityAction<T> actionOnRelease)
    {
        m_ActionOnGet = actionOnGet;
        m_ActionOnRelease = actionOnRelease;
    }

    public T Get()
    {
        T element;
        if(m_Stack.Count==0)
        {
            element = new T();
            countAll++;
        }
        else
        {
            element = m_Stack.Pop();
        }
        if (m_ActionOnGet != null) { m_ActionOnGet(element); }

        return element;
    }

    public void Release(T element)
    {
        if(m_Stack.Count>0&&ReferenceEquals(m_Stack.Peek(),element))
        {
            Debug.Log("Internal error. Trying to destroy object that is already released to pool.");
        }
        if(m_ActionOnRelease!=null)
        {
            m_ActionOnRelease(element);
        }
        m_Stack.Push(element);
    }
}


public class TextAnimtion : BaseMeshEffect{

    private const int OneSpriteVertex = 6;
    private bool _isEnd = false;
    private float _alpha = 0;
    private int _charaCount = 0;
    private Text _text;
    public Text Text { get { return _text ?? (_text = GetComponent<Text>()); } }

    //文字表示終了判定
    public bool IsEnd(){ return _isEnd; }

    //文字数カウント初期化
    public void Initialize()
    {
        _charaCount = 0;
        _isEnd = false;
    }

    public override void ModifyMesh(VertexHelper verts)
    {
        var input = new List<UIVertex>();
        var output = new List<UIVertex>();

        var text = Text;

        verts.GetUIVertexStream(input);
        var vertexTop = _charaCount * OneSpriteVertex;

        if(vertexTop>=input.Count)
        {
            _isEnd = true;
            return;
        }

        for(int i=0;i<vertexTop;i++)
        {
            output.Add(input[i]);
        }

        for(int i=vertexTop;i<vertexTop+OneSpriteVertex;i++)
        {
            var uiVertex = input[i];
            uiVertex.color.a = (byte)(255f * _alpha);
            output.Add(uiVertex);
        }

        _alpha += 0.75f;
        if(_alpha>=1f)
        {
            _charaCount++;
            _alpha = 0;
        }


        //var stream = ListPooling<UIVertex>.Get();
        //verts.GetUIVertexStream(stream);

        //modify(ref stream,45);

        verts.Clear();
        verts.AddUIVertexTriangleStream(output);

        //ListPooling<UIVertex>.Release(output);
    }

    private void modify(ref List<UIVertex> stream,float angle)
    {
    }
}
