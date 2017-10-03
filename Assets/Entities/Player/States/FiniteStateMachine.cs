using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player.States {
  public class FiniteStateMachine : MonoBehaviour {
    
    /*
    States:
      * Idling
      * Start Walking
      * Stop Walking
      * Walking
      * Start Running
      * Stop Running
      * Running
  */

    private Dictionary<string, Component> _stateTable;
    private Component _activeState;
    private GameObject _stateEmpty;
    
    private void Awake() {
      _stateEmpty = new GameObject {name = typeof(FiniteStateMachine).Name};
      _stateEmpty.transform.SetParent(transform, false);
      
      // The Reflection idiom here allows child classes to call ChangeState with the
      // name of the state they intend to switch to.
      _stateTable = new Dictionary<string, Component>();
      _stateTable[typeof(Idling).Name] = CreateAndAddChildState<Idling>(ref _stateEmpty);
      _stateTable[typeof(Walking).Name] = CreateAndAddChildState<Walking>(ref _stateEmpty);
      
      // 'Idling' is always the default state.
      _activeState = _stateTable[typeof(Idling).Name];
    }
    
    public void ChangeState(string state) {
      Debug.Assert(_stateTable.ContainsKey(state));
      _activeState = _stateTable[state];
    }

    public bool IsActive(Component state) {
      return _activeState == state;
    }
  
    private static T CreateAndAddChildState<T>(ref GameObject g)
      where T : Component {
      var empty = new GameObject {name = typeof(T).Name};
      empty.transform.SetParent(g.transform, false);
      return empty.AddComponent<T>();
    }
  }
}