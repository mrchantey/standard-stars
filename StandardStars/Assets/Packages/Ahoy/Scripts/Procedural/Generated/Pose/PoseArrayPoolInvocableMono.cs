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
/*AUTO SCRIPT*/    public class PoseArrayPoolInvocableMono : InvocableMono
/*AUTO SCRIPT*/    {
/*AUTO SCRIPT*/        [Range(0, 12400)]
/*AUTO SCRIPT*/        public int poolSize = 512;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public PoseArrayPoolUnityEvent onInvoke;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        PoseArrayPool array;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        void OnEnable()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            array = new PoseArrayPool(poolSize);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void Push(Pose val)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            array.Push(val);
/*AUTO SCRIPT*/            Invoke();
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public Pose Pop()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            var val = array.Pop();
/*AUTO SCRIPT*/            Invoke();
/*AUTO SCRIPT*/            return val;
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public override void Invoke()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            onInvoke.Invoke(array);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    }
/*AUTO SCRIPT*/}