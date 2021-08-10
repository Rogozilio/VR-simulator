using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = System.Object;

public enum Operator
{
    Less = 0,
    LessEquals,
    Equally,
    MoreEquals,
    More
}

[Serializable]
public struct ValueCondition
{
    public string NameProperty;
    public Operator Opertor;
    public float Value;
}

[Serializable]
public class Condition
{
    private Predicate<float> _predicate;
    public ValueCondition Value;

    public string Text
    {
        get => Value.NameProperty + GetMarkOperator() + Value.Value;
    }

    private string GetMarkOperator()
    {
        string mark = "";
        switch (Value.Opertor)
        {
            case Operator.Less:
                mark += " < ";
                break;
            case Operator.LessEquals:
                mark += " <= ";
                break;
            case Operator.Equally:
                mark += " = ";
                break;
            case Operator.MoreEquals:
                mark += " >= ";
                break;
            case Operator.More:
                mark += " > ";
                break;
        }

        return mark;
    }

    public Condition(string nameProperty, Operator op, float result = 1)
    {
        Value.NameProperty = nameProperty;
        Value.Opertor = op;
        Value.Value = result;
    }

    public bool GetResult(float value)
    {
        if (_predicate == null)
        {
            switch (Value.Opertor)
            {
                case Operator.Less:
                    _predicate = delegate(float result) { return result < Value.Value; };
                    break;
                case Operator.LessEquals:
                    _predicate = delegate(float result) { return result <= Value.Value; };
                    break;
                case Operator.Equally:
                    _predicate = delegate(float result) { return result == Value.Value; };
                    break;
                case Operator.MoreEquals:
                    _predicate = delegate(float result) { return result >= Value.Value; };
                    break;
                case Operator.More:
                    _predicate = delegate(float result) { return result > Value.Value; };
                    break;
            }
        }
        return _predicate(value);
    }
}