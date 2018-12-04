﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SimpleJSON
{
    public enum JSONNodeType
    {
        Array = 1,
        Object = 2,
        String = 3,
        Number = 4,
        NullValue = 5,
        Boolean = 6,
        None = 7,
        Custom = 0xFF,
    }
    public enum JSONTextMode
    {
        Compact,
        Indent
    }

    public abstract partial class JSONNode
    {
        #region Enumerators
        public struct Enumerator
        {
            private enum Type { None, Array, Object }
            private Type type;
            private Dictionary<string, JSONNode>.Enumerator m_Object;
            private List<JSONNode>.Enumerator m_Array;
            public bool IsValid { get { return type != Type.None; } }
            public Enumerator(List<JSONNode>.Enumerator aArrayEnum)
            {
                type = Type.Array;
                m_Object = default(Dictionary<string, JSONNode>.Enumerator);
                m_Array = aArrayEnum;
            }
            public Enumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum)
            {
                type = Type.Object;
                m_Object = aDictEnum;
                m_Array = default(List<JSONNode>.Enumerator);
            }
            public KeyValuePair<string, JSONNode> Current
            {
                get
                {
                    if (type == Type.Array)
                        return new KeyValuePair<string, JSONNode>(string.Empty, m_Array.Current);
                    else if (type == Type.Object)
                        return m_Object.Current;
                    return new KeyValuePair<string, JSONNode>(string.Empty, null);
                }
            }
            public bool MoveNext()
            {
                if (type == Type.Array)
                    return m_Array.MoveNext();
                else if (type == Type.Object)
                    return m_Object.MoveNext();
                return false;
            }
        }
        public struct ValueEnumerator
        {
            private Enumerator m_Enumerator;
            public ValueEnumerator(List<JSONNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public ValueEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public ValueEnumerator(Enumerator aEnumerator) { m_Enumerator = aEnumerator; }
            public JSONNode Current { get { return m_Enumerator.Current.Value; } }
            public bool MoveNext() { return m_Enumerator.MoveNext(); }
            public ValueEnumerator GetEnumerator() { return this; }
        }
        public struct KeyEnumerator
        {
            private Enumerator m_Enumerator;
            public KeyEnumerator(List<JSONNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public KeyEnumerator(Dictionary<string, JSONNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public KeyEnumerator(Enumerator aEnumerator) { m_Enumerator = aEnumerator; }
            public JSONNode Current { get { return m_Enumerator.Current.Key; } }
            public bool MoveNext() { return m_Enumerator.MoveNext(); }
            public KeyEnumerator GetEnumerator() { return this; }
        }

        public class LinqEnumerator : IEnumerator<KeyValuePair<string, JSONNode>>, IEnumerable<KeyValuePair<string, JSONNode>>
        {
            private JSONNode m_Node;
            private Enumerator m_Enumerator;
            internal LinqEnumerator(JSONNode aNode)
            {
                m_Node = aNode;
                if (m_Node != null)
                    m_Enumerator = m_Node.GetEnumerator();
            }
            public KeyValuePair<string, JSONNode> Current { get { return m_Enumerator.Current; } }
            object IEnumerator.Current { get { return m_Enumerator.Current; } }
            public bool MoveNext() { return m_Enumerator.MoveNext(); }

            public void Dispose()
            {
                m_Node = null;
                m_Enumerator = new Enumerator();
            }

            public IEnumerator<KeyValuePair<string, JSONNode>> GetEnumerator()
            {
                return new LinqEnumerator(m_Node);
            }

            public void Reset()
            {
                if (m_Node != null)
                    m_Enumerator = m_Node.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new LinqEnumerator(m_Node);
            }
        }

        #endregion Enumerators

        #region common interface

        public static bool forceASCII = false; // Use Unicode by default

        public abstract JSONNodeType Tag { get; }

        public virtual JSONNode this[int aIndex] { get { return null; } set { } }

        public virtual JSONNode this[string aKey] { get { return null; } set { } }

        public virtual string Value { get { return ""; } set { } }

        public virtual int Count { get { return 0; } }

        public virtual bool IsNumber { get { return false; } }
        public virtual bool IsString { get { return false; } }
        public virtual bool IsBoolean { get { return false; } }
        public virtual bool IsNull { get { return false; } }
        public virtual bool IsArray { get { return false; } }
        public virtual bool IsObject { get { return false; } }

        public virtual bool Inline { get { return false; } set { } }

        public virtual void Add(string aKey, JSONNode aItem)
        {
        }
        public virtual void Add(JSONNode aItem)
        {
            Add("", aItem);
        }

        public virtual JSONNode Remove(string aKey)
        {
            return null;
        }

        public virtual JSONNode Remove(int aIndex)
        {
            return null;
        }

        public virtual JSONNode Remove(JSONNode aNode)
        {
            return aNode;
        }

        public virtual IEnumerable<JSONNode> Children
        {
            get
            {
                yield break;
            }
        }

        public IEnumerable<JSONNode> DeepChildren
        {
            get
            {
                foreach (var C in Children)
                    foreach (var D in C.DeepChildren)
                        yield return D;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, 0, JSONTextMode.Compact);
            return sb.ToString();
        }

        public virtual string ToString(int aIndent)
        {
            StringBuilder sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, aIndent, JSONTextMode.Indent);
            return sb.ToString();
        }
        internal abstract void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode);

        public abstract Enumerator GetEnumerator();
        public IEnumerable<KeyValuePair<string, JSONNode>> Linq { get { return new LinqEnumerator(this); } }
        public KeyEnumerator Keys { get { return new KeyEnumerator(GetEnumerator()); } }
        public ValueEnumerator Values { get { return new ValueEnumerator(GetEnumerator()); } }

        #endregion common interface

        #region typecasting properties


        public virtual double AsDouble
        {
            get
            {
                double v = 0.0;
                if (double.TryParse(Value, out v))
                    return v;
                return 0.0;
            }
            set
            {
                Value = value.ToString();
            }
        }

        public virtual int AsInt
        {
            get { return (int)AsDouble; }
            set { AsDouble = value; }
        }

        public virtual float AsFloat
        {
            get { return (float)AsDouble; }
            set { AsDouble = value; }
        }

        public virtual bool AsBool
        {
            get
            {
                bool v = false;
                if (bool.TryParse(Value, out v))
                    return v;
                return !string.IsNullOrEmpty(Value);
            }
            set
            {
                Value = (value) ? "true" : "false";
            }
        }

        public virtual JSONArray AsArray
        {
            get
            {
                return this as JSONArray;
            }
        }

        public virtual JSONObject AsObject
        {
            get
            {
                return this as JSONObject;
            }
        }


        #endregion typecasting properties

        #region operators

        public static implicit operator JSONNode(string s)
        {
            return new JSONString(s);
        }
        public static implicit operator string(JSONNode d)
        {
            return (d == null) ? null : d.Value;
        }

        public static implicit operator JSONNode(double n)
        {
            return new JSONNumber(n);
        }
        public static implicit operator double(JSONNode d)
        {
            return (d == null) ? 0 : d.AsDouble;
        }

        public static implicit operator JSONNode(float n)
        {
            return new JSONNumber(n);
        }
        public static implicit operator float(JSONNode d)
        {
            return (d == null) ? 0 : d.AsFloat;
        }

        public static implicit operator JSONNode(int n)
        {
            return new JSONNumber(n);
        }
        public static implicit operator int(JSONNode d)
        {
            return (d == null) ? 0 : d.AsInt;
        }

        public static implicit operator JSONNode(bool b)
        {
            return new JSONBool(b);
        }
        public static implicit operator bool(JSONNode d)
        {
            return (d == null) ? false : d.AsBool;
        }

        public static implicit operator JSONNode(KeyValuePair<string, JSONNode> aKeyValue)
        {
            return aKeyValue.Value;
        }

        public static bool operator ==(JSONNode a, object b)
        {
            if (ReferenceEquals(a, b))
                return true;
            bool aIsNull = a is JSONNull || ReferenceEquals(a, null) || a is JSONLazyCreator;
            bool bIsNull = b is JSONNull || ReferenceEquals(b, null) || b is JSONLazyCreator;
            if (aIsNull && bIsNull)
                return true;
            return !aIsNull && a.Equals(b);
        }

        public static bool operator !=(JSONNode a, object b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion operators

        [ThreadStatic]
        private static StringBuilder m_EscapeBuilder;
        internal static StringBuilder EscapeBuilder
        {
            get
            {
                if (m_EscapeBuilder == null)
                    m_EscapeBuilder = new StringBuilder();
                return m_EscapeBuilder;
            }
        }
        internal static string Escape(string aText)
        {
            var sb = EscapeBuilder;
            sb.Length = 0;
            if (sb.Capacity < aText.Length + aText.Length / 10)
                sb.Capacity = aText.Length + aText.Length / 10;
            foreach (char c in aText)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    default:
                        if (c < ' ' || (forceASCII && c > 127))
                        {
                            ushort val = c;
                            sb.Append("\\u").Append(val.ToString("X4"));
                        }
                        else
                            sb.Append(c);
                        break;
                }
            }
            string result = sb.ToString();
            sb.Length = 0;
            return result;
        }

        static void ParseElement(JSONNode ctx, string token, string tokenName, bool quoted)
        {
            if (quoted)
            {
                ctx.Add(tokenName, token);
                return;
            }
            string tmp = token.ToLower();
            if (tmp == "false" || tmp == "true")
                ctx.Add(tokenName, tmp == "true");
            else if (tmp == "null")
                ctx.Add(tokenName, null);
            else
            {
                double val;
                if (double.TryParse(token, out val))
                    ctx.Add(tokenName, val);
                else
                    ctx.Add(tokenName, token);
            }
        }

        public static JSONNode Parse(string aJSON)
        {
            Stack<JSONNode> stack = new Stack<JSONNode>();
            JSONNode ctx = null;
            int i = 0;
            StringBuilder Token = new StringBuilder();
            string TokenName = "";
            bool QuoteMode = false;
            bool TokenIsQuoted = false;
            while (i < aJSON.Length)
            {
                switch (aJSON[i])
                {
                    case '{':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        stack.Push(new JSONObject());
                        if (ctx != null)
                        {
                            ctx.Add(TokenName, stack.Peek());
                        }
                        TokenName = "";
                        Token.Length = 0;
                        ctx = stack.Peek();
                        break;

                    case '[':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }

                        stack.Push(new JSONArray());
                        if (ctx != null)
                        {
                            ctx.Add(TokenName, stack.Peek());
                        }
                        TokenName = "";
                        Token.Length = 0;
                        ctx = stack.Peek();
                        break;

                    case '}':
                    case ']':
                        if (QuoteMode)
                        {

                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (stack.Count == 0)
                            throw new Exception("JSON Parse: Too many closing brackets");

                        stack.Pop();
                        if (Token.Length > 0 || TokenIsQuoted)
                        {
                            ParseElement(ctx, Token.ToString(), TokenName, TokenIsQuoted);
                            TokenIsQuoted = false;
                        }
                        TokenName = "";
                        Token.Length = 0;
                        if (stack.Count > 0)
                            ctx = stack.Peek();
                        break;

                    case ':':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        TokenName = Token.ToString();
                        Token.Length = 0;
                        TokenIsQuoted = false;
                        break;

                    case '"':
                        QuoteMode ^= true;
                        TokenIsQuoted |= QuoteMode;
                        break;

                    case ',':
                        if (QuoteMode)
                        {
                            Token.Append(aJSON[i]);
                            break;
                        }
                        if (Token.Length > 0 || TokenIsQuoted)
                        {
                            ParseElement(ctx, Token.ToString(), TokenName, TokenIsQuoted);
                            TokenIsQuoted = false;
                        }
                        TokenName = "";
                        Token.Length = 0;
                        TokenIsQuoted = false;
                        break;

                    case '\r':
                    case '\n':
                        break;

                    case ' ':
                    case '\t':
                        if (QuoteMode)
                            Token.Append(aJSON[i]);
                        break;

                    case '\\':
                        ++i;
                        if (QuoteMode)
                        {
                            char C = aJSON[i];
                            switch (C)
                            {
                                case 't':
                                    Token.Append('\t');
                                    break;
                                case 'r':
                                    Token.Append('\r');
                                    break;
                                case 'n':
                                    Token.Append('\n');
                                    break;
                                case 'b':
                                    Token.Append('\b');
                                    break;
                                case 'f':
                                    Token.Append('\f');
                                    break;
                                case 'u':
                                    {
                                        string s = aJSON.Substring(i + 1, 4);
                                        Token.Append((char)int.Parse(
                                            s,
                                            System.Globalization.NumberStyles.AllowHexSpecifier));
                                        i += 4;
                                        break;
                                    }
                                default:
                                    Token.Append(C);
                                    break;
                            }
                        }
                        break;

                    default:
                        Token.Append(aJSON[i]);
                        break;
                }
                ++i;
            }
            if (QuoteMode)
            {
                throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
            }
            return ctx;
        }

    }
    // End of JSONNode

    public partial class JSONArray : JSONNode
    {
        private List<JSONNode> m_List = new List<JSONNode>();
        private bool inline = false;
        public override bool Inline
        {
            get { return inline; }
            set { inline = value; }
        }

        public override JSONNodeType Tag { get { return JSONNodeType.Array; } }
        public override bool IsArray { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(m_List.GetEnumerator()); }

        public override JSONNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= m_List.Count)
                    return new JSONLazyCreator(this);
                return m_List[aIndex];
            }
            set
            {
                if (value == null)
                    value = JSONNull.CreateOrGet();
                if (aIndex < 0 || aIndex >= m_List.Count)
                    m_List.Add(value);
                else
                    m_List[aIndex] = value;
            }
        }

        public override JSONNode this[string aKey]
        {
            get { return new JSONLazyCreator(this); }
            set
            {
                if (value == null)
                    value = JSONNull.CreateOrGet();
                m_List.Add(value);
            }
        }

        public override int Count
        {
            get { return m_List.Count; }
        }

        public override void Add(string aKey, JSONNode aItem)
        {
            if (aItem == null)
                aItem = JSONNull.CreateOrGet();
            m_List.Add(aItem);
        }

        public override JSONNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= m_List.Count)
                return null;
            JSONNode tmp = m_List[aIndex];
            m_List.RemoveAt(aIndex);
            return tmp;
        }

        public override JSONNode Remove(JSONNode aNode)
        {
            m_List.Remove(aNode);
            return aNode;
        }

        public override IEnumerable<JSONNode> Children
        {
            get
            {
                foreach (JSONNode N in m_List)
                    yield return N;
            }
        }


        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append('[');
            int count = m_List.Count;
            if (inline)
                aMode = JSONTextMode.Compact;
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    aSB.Append(',');
                if (aMode == JSONTextMode.Indent)
                    aSB.AppendLine();

                if (aMode == JSONTextMode.Indent)
                    aSB.Append(' ', aIndent + aIndentInc);
                m_List[i].WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
            }
            if (aMode == JSONTextMode.Indent)
                aSB.AppendLine().Append(' ', aIndent);
            aSB.Append(']');
        }
    }
    // End of JSONArray

    public partial class JSONObject : JSONNode
    {
        private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();

        private bool inline = false;
        public override bool Inline
        {
            get { return inline; }
            set { inline = value; }
        }

        public override JSONNodeType Tag { get { return JSONNodeType.Object; } }
        public override bool IsObject { get { return true; } }

        public override Enumerator GetEnumerator() { return new Enumerator(m_Dict.GetEnumerator()); }


        public override JSONNode this[string aKey]
        {
            get
            {
                if (m_Dict.ContainsKey(aKey))
                    return m_Dict[aKey];
                else
                    return new JSONLazyCreator(this, aKey);
            }
            set
            {
                if (value == null)
                    value = JSONNull.CreateOrGet();
                if (m_Dict.ContainsKey(aKey))
                    m_Dict[aKey] = value;
                else
                    m_Dict.Add(aKey, value);
            }
        }

        public override JSONNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= m_Dict.Count)
                    return null;
                return m_Dict.ElementAt(aIndex).Value;
            }
            set
            {
                if (value == null)
                    value = JSONNull.CreateOrGet();
                if (aIndex < 0 || aIndex >= m_Dict.Count)
                    return;
                string key = m_Dict.ElementAt(aIndex).Key;
                m_Dict[key] = value;
            }
        }

        public override int Count
        {
            get { return m_Dict.Count; }
        }

        public override void Add(string aKey, JSONNode aItem)
        {
            if (aItem == null)
                aItem = JSONNull.CreateOrGet();

            if (!string.IsNullOrEmpty(aKey))
            {
                if (m_Dict.ContainsKey(aKey))
                    m_Dict[aKey] = aItem;
                else
                    m_Dict.Add(aKey, aItem);
            }
            else
                m_Dict.Add(Guid.NewGuid().ToString(), aItem);
        }

        public override JSONNode Remove(string aKey)
        {
            if (!m_Dict.ContainsKey(aKey))
                return null;
            JSONNode tmp = m_Dict[aKey];
            m_Dict.Remove(aKey);
            return tmp;
        }

        public override JSONNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= m_Dict.Count)
                return null;
            var item = m_Dict.ElementAt(aIndex);
            m_Dict.Remove(item.Key);
            return item.Value;
        }

        public override JSONNode Remove(JSONNode aNode)
        {
            try
            {
                var item = m_Dict.Where(k => k.Value == aNode).First();
                m_Dict.Remove(item.Key);
                return aNode;
            }
            catch
            {
                return null;
            }
        }

        public override IEnumerable<JSONNode> Children
        {
            get
            {
                foreach (KeyValuePair<string, JSONNode> N in m_Dict)
                    yield return N.Value;
            }
        }

        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append('{');
            bool first = true;
            if (inline)
                aMode = JSONTextMode.Compact;
            foreach (var k in m_Dict)
            {
                if (!first)
                    aSB.Append(',');
                first = false;
                if (aMode == JSONTextMode.Indent)
                    aSB.AppendLine();
                if (aMode == JSONTextMode.Indent)
                    aSB.Append(' ', aIndent + aIndentInc);
                aSB.Append('\"').Append(Escape(k.Key)).Append('\"');
                if (aMode == JSONTextMode.Compact)
                    aSB.Append(':');
                else
                    aSB.Append(" : ");
                k.Value.WriteToStringBuilder(aSB, aIndent + aIndentInc, aIndentInc, aMode);
            }
            if (aMode == JSONTextMode.Indent)
                aSB.AppendLine().Append(' ', aIndent);
            aSB.Append('}');
        }

    }
    // End of JSONObject

    public partial class JSONString : JSONNode
    {
        private string m_Data;

        public override JSONNodeType Tag { get { return JSONNodeType.String; } }
        public override bool IsString { get { return true; } }

        public override Enumerator GetEnumerator() { return new Enumerator(); }


        public override string Value
        {
            get { return m_Data; }
            set
            {
                m_Data = value;
            }
        }

        public JSONString(string aData)
        {
            m_Data = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append('\"').Append(Escape(m_Data)).Append('\"');
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;
            string s = obj as string;
            if (s != null)
                return m_Data == s;
            JSONString s2 = obj as JSONString;
            if (s2 != null)
                return m_Data == s2.m_Data;
            return false;
        }
        public override int GetHashCode()
        {
            return m_Data.GetHashCode();
        }
    }
    // End of JSONString

    public partial class JSONNumber : JSONNode
    {
        private double m_Data;

        public override JSONNodeType Tag { get { return JSONNodeType.Number; } }
        public override bool IsNumber { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get { return m_Data.ToString(); }
            set
            {
                double v;
                if (double.TryParse(value, out v))
                    m_Data = v;
            }
        }

        public override double AsDouble
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public JSONNumber(double aData)
        {
            m_Data = aData;
        }

        public JSONNumber(string aData)
        {
            Value = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append(m_Data);
        }
        private static bool IsNumeric(object value)
        {
            return value is int || value is uint
                || value is float || value is double
                || value is decimal
                || value is long || value is ulong
                || value is short || value is ushort
                || value is sbyte || value is byte;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (base.Equals(obj))
                return true;
            JSONNumber s2 = obj as JSONNumber;
            if (s2 != null)
                return m_Data == s2.m_Data;
            if (IsNumeric(obj))
                return Convert.ToDouble(obj) == m_Data;
            return false;
        }
        public override int GetHashCode()
        {
            return m_Data.GetHashCode();
        }
    }
    // End of JSONNumber

    public partial class JSONBool : JSONNode
    {
        private bool m_Data;

        public override JSONNodeType Tag { get { return JSONNodeType.Boolean; } }
        public override bool IsBoolean { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get { return m_Data.ToString(); }
            set
            {
                bool v;
                if (bool.TryParse(value, out v))
                    m_Data = v;
            }
        }
        public override bool AsBool
        {
            get { return m_Data; }
            set { m_Data = value; }
        }

        public JSONBool(bool aData)
        {
            m_Data = aData;
        }

        public JSONBool(string aData)
        {
            Value = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append((m_Data) ? "true" : "false");
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool)
                return m_Data == (bool)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return m_Data.GetHashCode();
        }
    }
    // End of JSONBool

    public partial class JSONNull : JSONNode
    {
        static JSONNull m_StaticInstance = new JSONNull();
        public static bool reuseSameInstance = true;
        public static JSONNull CreateOrGet()
        {
            if (reuseSameInstance)
                return m_StaticInstance;
            return new JSONNull();
        }
        private JSONNull() { }

        public override JSONNodeType Tag { get { return JSONNodeType.NullValue; } }
        public override bool IsNull { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get { return "null"; }
            set { }
        }
        public override bool AsBool
        {
            get { return false; }
            set { }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            return (obj is JSONNull);
        }
        public override int GetHashCode()
        {
            return 0;
        }

        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append("null");
        }
    }
    // End of JSONNull

    internal partial class JSONLazyCreator : JSONNode
    {
        private JSONNode m_Node = null;
        private string m_Key = null;
        public override JSONNodeType Tag { get { return JSONNodeType.None; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public JSONLazyCreator(JSONNode aNode)
        {
            m_Node = aNode;
            m_Key = null;
        }

        public JSONLazyCreator(JSONNode aNode, string aKey)
        {
            m_Node = aNode;
            m_Key = aKey;
        }

        private void Set(JSONNode aVal)
        {
            if (m_Key == null)
            {
                m_Node.Add(aVal);
            }
            else
            {
                m_Node.Add(m_Key, aVal);
            }
            m_Node = null; // Be GC friendly.
        }

        public override JSONNode this[int aIndex]
        {
            get
            {
                return new JSONLazyCreator(this);
            }
            set
            {
                var tmp = new JSONArray();
                tmp.Add(value);
                Set(tmp);
            }
        }

        public override JSONNode this[string aKey]
        {
            get
            {
                return new JSONLazyCreator(this, aKey);
            }
            set
            {
                var tmp = new JSONObject();
                tmp.Add(aKey, value);
                Set(tmp);
            }
        }

        public override void Add(JSONNode aItem)
        {
            var tmp = new JSONArray();
            tmp.Add(aItem);
            Set(tmp);
        }

        public override void Add(string aKey, JSONNode aItem)
        {
            var tmp = new JSONObject();
            tmp.Add(aKey, aItem);
            Set(tmp);
        }

        public static bool operator ==(JSONLazyCreator a, object b)
        {
            if (b == null)
                return true;
            return System.Object.ReferenceEquals(a, b);
        }

        public static bool operator !=(JSONLazyCreator a, object b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return true;
            return System.Object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override int AsInt
        {
            get
            {
                JSONNumber tmp = new JSONNumber(0);
                Set(tmp);
                return 0;
            }
            set
            {
                JSONNumber tmp = new JSONNumber(value);
                Set(tmp);
            }
        }

        public override float AsFloat
        {
            get
            {
                JSONNumber tmp = new JSONNumber(0.0f);
                Set(tmp);
                return 0.0f;
            }
            set
            {
                JSONNumber tmp = new JSONNumber(value);
                Set(tmp);
            }
        }

        public override double AsDouble
        {
            get
            {
                JSONNumber tmp = new JSONNumber(0.0);
                Set(tmp);
                return 0.0;
            }
            set
            {
                JSONNumber tmp = new JSONNumber(value);
                Set(tmp);
            }
        }

        public override bool AsBool
        {
            get
            {
                JSONBool tmp = new JSONBool(false);
                Set(tmp);
                return false;
            }
            set
            {
                JSONBool tmp = new JSONBool(value);
                Set(tmp);
            }
        }

        public override JSONArray AsArray
        {
            get
            {
                JSONArray tmp = new JSONArray();
                Set(tmp);
                return tmp;
            }
        }

        public override JSONObject AsObject
        {
            get
            {
                JSONObject tmp = new JSONObject();
                Set(tmp);
                return tmp;
            }
        }
        internal override void WriteToStringBuilder(StringBuilder aSB, int aIndent, int aIndentInc, JSONTextMode aMode)
        {
            aSB.Append("null");
        }
    }
    // End of JSONLazyCreator

    public static class JSON
    {
        public static JSONNode Parse(string aJSON)
        {
            return JSONNode.Parse(aJSON);
        }
    }
    public enum JSONContainerType { Array, Object }
    public partial class JSONNode
    {
        public static JSONContainerType VectorContainerType = JSONContainerType.Array;
        public static JSONContainerType QuaternionContainerType = JSONContainerType.Array;
        public static JSONContainerType RectContainerType = JSONContainerType.Array;
        private static JSONNode GetContainer(JSONContainerType aType)
        {
            if (aType == JSONContainerType.Array)
                return new JSONArray();
            return new JSONObject();
        }

        #region implicit conversion operators
        public static implicit operator JSONNode(Vector2 aVec)
        {
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector2(aVec);
            return n;
        }
        public static implicit operator JSONNode(Vector3 aVec)
        {
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector3(aVec);
            return n;
        }
        public static implicit operator JSONNode(Vector4 aVec)
        {
            JSONNode n = GetContainer(VectorContainerType);
            n.WriteVector4(aVec);
            return n;
        }
        public static implicit operator JSONNode(Quaternion aRot)
        {
            JSONNode n = GetContainer(QuaternionContainerType);
            n.WriteQuaternion(aRot);
            return n;
        }
        public static implicit operator JSONNode(Rect aRect)
        {
            JSONNode n = GetContainer(RectContainerType);
            n.WriteRect(aRect);
            return n;
        }
        public static implicit operator JSONNode(RectOffset aRect)
        {
            JSONNode n = GetContainer(RectContainerType);
            n.WriteRectOffset(aRect);
            return n;
        }

        public static implicit operator Vector2(JSONNode aNode)
        {
            return aNode.ReadVector2();
        }
        public static implicit operator Vector3(JSONNode aNode)
        {
            return aNode.ReadVector3();
        }
        public static implicit operator Vector4(JSONNode aNode)
        {
            return aNode.ReadVector4();
        }
        public static implicit operator Quaternion(JSONNode aNode)
        {
            return aNode.ReadQuaternion();
        }
        public static implicit operator Rect(JSONNode aNode)
        {
            return aNode.ReadRect();
        }
        public static implicit operator RectOffset(JSONNode aNode)
        {
            return aNode.ReadRectOffset();
        }
        #endregion implicit conversion operators

        #region Vector2
        public Vector2 ReadVector2(Vector2 aDefault)
        {
            if (IsObject)
                return new Vector2(this["x"].AsFloat, this["y"].AsFloat);
            if (IsArray)
                return new Vector2(this[0].AsFloat, this[1].AsFloat);
            return aDefault;
        }
        public Vector2 ReadVector2(string aXName, string aYName)
        {
            if (IsObject)
            {
                return new Vector2(this[aXName].AsFloat, this[aYName].AsFloat);
            }
            return Vector2.zero;
        }

        public Vector2 ReadVector2()
        {
            return ReadVector2(Vector2.zero);
        }
        public JSONNode WriteVector2(Vector2 aVec, string aXName = "x", string aYName = "y")
        {
            if (IsObject)
            {
                Inline = true;
                this[aXName].AsFloat = aVec.x;
                this[aYName].AsFloat = aVec.y;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
            }
            return this;
        }
        #endregion Vector2

        #region Vector3
        public Vector3 ReadVector3(Vector3 aDefault)
        {
            if (IsObject)
                return new Vector3(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat);
            if (IsArray)
                return new Vector3(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat);
            return aDefault;
        }
        public Vector3 ReadVector3(string aXName, string aYName, string aZName)
        {
            if (IsObject)
                return new Vector3(this[aXName].AsFloat, this[aYName].AsFloat, this[aZName].AsFloat);
            return Vector3.zero;
        }
        public Vector3 ReadVector3()
        {
            return ReadVector3(Vector3.zero);
        }
        public JSONNode WriteVector3(Vector3 aVec, string aXName = "x", string aYName = "y", string aZName = "z")
        {
            if (IsObject)
            {
                Inline = true;
                this[aXName].AsFloat = aVec.x;
                this[aYName].AsFloat = aVec.y;
                this[aZName].AsFloat = aVec.z;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
                this[2].AsFloat = aVec.z;
            }
            return this;
        }
        #endregion Vector3

        #region Vector4
        public Vector4 ReadVector4(Vector4 aDefault)
        {
            if (IsObject)
                return new Vector4(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat, this["w"].AsFloat);
            if (IsArray)
                return new Vector4(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Vector4 ReadVector4()
        {
            return ReadVector4(Vector4.zero);
        }
        public JSONNode WriteVector4(Vector4 aVec)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aVec.x;
                this["y"].AsFloat = aVec.y;
                this["z"].AsFloat = aVec.z;
                this["w"].AsFloat = aVec.w;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aVec.x;
                this[1].AsFloat = aVec.y;
                this[2].AsFloat = aVec.z;
                this[3].AsFloat = aVec.w;
            }
            return this;
        }
        #endregion Vector4

        #region Quaternion
        public Quaternion ReadQuaternion(Quaternion aDefault)
        {
            if (IsObject)
                return new Quaternion(this["x"].AsFloat, this["y"].AsFloat, this["z"].AsFloat, this["w"].AsFloat);
            if (IsArray)
                return new Quaternion(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Quaternion ReadQuaternion()
        {
            return ReadQuaternion(Quaternion.identity);
        }
        public JSONNode WriteQuaternion(Quaternion aRot)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aRot.x;
                this["y"].AsFloat = aRot.y;
                this["z"].AsFloat = aRot.z;
                this["w"].AsFloat = aRot.w;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aRot.x;
                this[1].AsFloat = aRot.y;
                this[2].AsFloat = aRot.z;
                this[3].AsFloat = aRot.w;
            }
            return this;
        }
        #endregion Quaternion

        #region Rect
        public Rect ReadRect(Rect aDefault)
        {
            if (IsObject)
                return new Rect(this["x"].AsFloat, this["y"].AsFloat, this["width"].AsFloat, this["height"].AsFloat);
            if (IsArray)
                return new Rect(this[0].AsFloat, this[1].AsFloat, this[2].AsFloat, this[3].AsFloat);
            return aDefault;
        }
        public Rect ReadRect()
        {
            return ReadRect(new Rect());
        }
        public JSONNode WriteRect(Rect aRect)
        {
            if (IsObject)
            {
                Inline = true;
                this["x"].AsFloat = aRect.x;
                this["y"].AsFloat = aRect.y;
                this["width"].AsFloat = aRect.width;
                this["height"].AsFloat = aRect.height;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsFloat = aRect.x;
                this[1].AsFloat = aRect.y;
                this[2].AsFloat = aRect.width;
                this[3].AsFloat = aRect.height;
            }
            return this;
        }
        #endregion Rect

        #region RectOffset
        public RectOffset ReadRectOffset(RectOffset aDefault)
        {
            if (this is JSONObject)
                return new RectOffset(this["left"].AsInt, this["right"].AsInt, this["top"].AsInt, this["bottom"].AsInt);
            if (this is JSONArray)
                return new RectOffset(this[0].AsInt, this[1].AsInt, this[2].AsInt, this[3].AsInt);
            return aDefault;
        }
        public RectOffset ReadRectOffset()
        {
            return ReadRectOffset(new RectOffset());
        }
        public JSONNode WriteRectOffset(RectOffset aRect)
        {
            if (IsObject)
            {
                Inline = true;
                this["left"].AsInt = aRect.left;
                this["right"].AsInt = aRect.right;
                this["top"].AsInt = aRect.top;
                this["bottom"].AsInt = aRect.bottom;
            }
            else if (IsArray)
            {
                Inline = true;
                this[0].AsInt = aRect.left;
                this[1].AsInt = aRect.right;
                this[2].AsInt = aRect.top;
                this[3].AsInt = aRect.bottom;
            }
            return this;
        }
        #endregion RectOffset

        #region Matrix4x4
        public Matrix4x4 ReadMatrix()
        {
            Matrix4x4 result = Matrix4x4.identity;
            if (IsArray)
            {
                for (int i = 0; i < 16; i++)
                {
                    result[i] = this[i].AsFloat;
                }
            }
            return result;
        }
        public JSONNode WriteMatrix(Matrix4x4 aMatrix)
        {
            if (IsArray)
            {
                Inline = true;
                for (int i = 0; i < 16; i++)
                {
                    this[i].AsFloat = aMatrix[i];
                }
            }
            return this;
        }
        #endregion Matrix4x4
    }
}