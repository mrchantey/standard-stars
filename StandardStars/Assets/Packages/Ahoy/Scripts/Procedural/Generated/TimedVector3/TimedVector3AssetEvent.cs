//
//
//	THS FILE IS AUTO GENERATED
//	DO NOT EDIT DIRECTLY
//



/*AUTO SCRIPT*/using UnityEngine;
/*AUTO SCRIPT*/using UnityEngine.Events;
/*AUTO SCRIPT*/using System.Collections.Generic;
/*AUTO SCRIPT*/using System;
/*AUTO SCRIPT*/using System.Linq;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/namespace Ahoy
/*AUTO SCRIPT*/{
/*AUTO SCRIPT*/    [CreateAssetMenu(fileName = "TimedVector3AssetEvent", menuName = "Ahoy/Asset Events/TimedVector3", order = 0)]
/*AUTO SCRIPT*/    public class TimedVector3AssetEvent : InvocableSO
/*AUTO SCRIPT*/    {
/*AUTO SCRIPT*/        public bool debug;
/*AUTO SCRIPT*/        public TimedVector3 constantValue;
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        // public List<TimedVector3UnityEvent> listeners = new List<TimedVector3UnityEvent>();
/*AUTO SCRIPT*/        public List<TimedVector3UnityEvent> unityEventListeners;
/*AUTO SCRIPT*/        public List<TimedVector3AssetEvent> assetEventListeners;
/*AUTO SCRIPT*/        AhoyEvent<TimedVector3> ahoyEvent = new AhoyEvent<TimedVector3>();
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void AddListener(Action<TimedVector3> action)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            ahoyEvent.AddListener(action);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void RemoveListener(Action<TimedVector3> action)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            ahoyEvent.RemoveListener(action);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void AddListener(TimedVector3UnityEvent listener)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            unityEventListeners.TryAdd(listener);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/        public void RemoveListener(TimedVector3UnityEvent listener)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            unityEventListeners.TryRemove(listener);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/        public void AddListener(TimedVector3AssetEvent listener)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            assetEventListeners.TryAdd(listener);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/        public void RemoveListener(TimedVector3AssetEvent listener)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            assetEventListeners.TryRemove(listener);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public void Invoke(TimedVector3 val)
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            if (debug)
/*AUTO SCRIPT*/                Debug.Log($"{name} event invoked: {val}");
/*AUTO SCRIPT*/            ahoyEvent.Invoke(val);
/*AUTO SCRIPT*/            //important to do this incase a listener removes its self from the list
/*AUTO SCRIPT*/            unityEventListeners.ToArray().ForEach(l => l.Invoke(val));
/*AUTO SCRIPT*/            assetEventListeners.ToArray().ForEach(l => l.Invoke(val));
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/        public override void Invoke()
/*AUTO SCRIPT*/        {
/*AUTO SCRIPT*/            Invoke(constantValue);
/*AUTO SCRIPT*/        }
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/
/*AUTO SCRIPT*/    }
/*AUTO SCRIPT*/}