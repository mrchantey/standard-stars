//
//
//	THS FILE IS AUTO GENERATED
//	DO NOT EDIT DIRECTLY
//



/*AUTO SCRIPT*/using UnityEngine;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/namespace Ahoy
/*AUTO SCRIPT*/{
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    public class UlongArrayInvocableMono : InvocableMono
/*AUTO SCRIPT*/    {
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public ulong[] value;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public UlongArrayUnityEvent onInvoke;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public override void Invoke()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            onInvoke.Invoke(value);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/}