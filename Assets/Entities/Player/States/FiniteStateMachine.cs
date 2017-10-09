/*
  FiniteStateMachine.cs
  Author: Samuel Vargas
*/

using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player.States {
  public abstract class FiniteStateMonoBehaviour : MonoBehaviour {
    public abstract void Enter();
    public abstract void Exit();
  }

  public class FiniteStateMachine : MonoBehaviour {
    private Dictionary<string, FiniteStateMonoBehaviour> _stateTable;
    private FiniteStateMonoBehaviour _activeState;

    private void Start() {
      var stateEmpty = new GameObject {name = typeof(FiniteStateMachine).Name};
      stateEmpty.transform.SetParent(transform, false);

      // Setup states, use the classname reflection idiom to refer to a specific state
      _stateTable = new Dictionary<string, FiniteStateMonoBehaviour>();
      _stateTable[typeof(Idling).Name] = CreateAndAddDisabledChildState<Idling>(ref stateEmpty);
      _stateTable[typeof(Walking).Name] = CreateAndAddDisabledChildState<Walking>(ref stateEmpty);

      // Set & enable default state 
      _activeState = _stateTable[typeof(Idling).Name];
      _activeState.enabled = true;
    }

    public void ChangeState(string state) {
      Debug.Assert(_stateTable.ContainsKey(state));
      _activeState.Exit();
      _activeState.enabled = false;
      _activeState = _stateTable[state];
      _activeState.enabled = true;
      _activeState.Enter();
    }

    public bool IsActive(FiniteStateMonoBehaviour state) {
      return _activeState == state;
    }

    private static T CreateAndAddDisabledChildState<T>(ref GameObject g)
      where T : FiniteStateMonoBehaviour {
      var empty = new GameObject {name = typeof(T).Name};
      empty.transform.SetParent(g.transform, false);
      var c = empty.AddComponent<T>();
      c.enabled = false;
      return c;
    }
  }
}