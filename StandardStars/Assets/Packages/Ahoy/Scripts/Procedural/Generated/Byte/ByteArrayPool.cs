//
//
//	THS FILE IS AUTO GENERATED
//	DO NOT EDIT DIRECTLY
//



/*AUTO SCRIPT*/using UnityEngine;
/*AUTO SCRIPT*/using System;
/*AUTO SCRIPT*/using System.Collections;
/*AUTO SCRIPT*/using System.Collections.Generic;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/namespace Ahoy
/*AUTO SCRIPT*/{
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    public struct ByteArrayPool : IEnumerable<byte>
/*AUTO SCRIPT*/    {
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public byte[] m_array;
/*AUTO SCRIPT*/        int index;
/*AUTO SCRIPT*/        // public int Length { get { return m_array.Length; } }
/*AUTO SCRIPT*/        public int Length { get; private set; }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public ByteArrayPool(int length)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            m_array = new byte[length];
/*AUTO SCRIPT*/            index = -1;
/*AUTO SCRIPT*/            Length = 0;
/*AUTO SCRIPT*/            // Length = 0;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void Push(byte val)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            index = (index + 1) % m_array.Length;
/*AUTO SCRIPT*/            Length = Mathf.Clamp(Length + 1, 0, m_array.Length - 1);
/*AUTO SCRIPT*/            m_array[index] = val;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public byte Pop()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            index = index - 1;
/*AUTO SCRIPT*/            if (index < 0) index = m_array.Length - 1;
/*AUTO SCRIPT*/            Length = Mathf.Clamp(Length - 1, 0, m_array.Length - 1);
/*AUTO SCRIPT*/            return m_array[index];
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public byte this[int key]
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            get => m_array[(index + key) % Length];
/*AUTO SCRIPT*/            // get
/*AUTO SCRIPT*/            // {
/*AUTO SCRIPT*/            //     var i = (index + key) % Length;
/*AUTO SCRIPT*/            //     Debug.Log($"getting element at {i}");
/*AUTO SCRIPT*/            //     return m_array[i];
/*AUTO SCRIPT*/            // }
/*AUTO SCRIPT*/            // set => m_array[(index + key) % m_array.Length] = value;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        IEnumerator IEnumerable.GetEnumerator()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            return GetEnumerator();
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        IEnumerator<byte> IEnumerable<byte>.GetEnumerator()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            return (IEnumerator<byte>)GetEnumerator();
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public ByteArrayPoolEnum GetEnumerator()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            return new ByteArrayPoolEnum(this);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/    }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    public class ByteArrayPoolEnum : IEnumerator<byte>
/*AUTO SCRIPT*/    {
/*AUTO SCRIPT*/        public ByteArrayPool arrayPool;
/*AUTO SCRIPT*/        int position = -1;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public ByteArrayPoolEnum(ByteArrayPool arrayPool)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            this.arrayPool = arrayPool;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public bool MoveNext()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            position++;
/*AUTO SCRIPT*/            return (position < arrayPool.Length);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void Reset()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            position = -1;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/        object IEnumerator.Current { get { return Current; } }
/*AUTO SCRIPT*/        public byte Current { get { try { return arrayPool[position]; } catch (IndexOutOfRangeException) { throw new InvalidOperationException(); } } }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void Dispose() { }
/*AUTO SCRIPT*/    }
/*AUTO SCRIPT*/}