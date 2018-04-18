#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    [RequireComponent(typeof(Collider))]
    public class Crossing : MonoBehaviour
    {
        #region Fields

        #region  Private Fields

        private HashSet<NavAgentBase> _currentAgents;
        private CrossingState _state;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void Awake()
        {
            _currentAgents = new HashSet<NavAgentBase>();
        }

        private void CheckAgents()
        {
            switch(_state)
            {
                case CrossingState.Vehicle:
                {
                    if(_currentAgents.OfType<VehicleAgent>().Any())
                        _state = CrossingState.Vehicle;
                    else if(_currentAgents.OfType<HumanoidAgent>().Any())
                        _state = CrossingState.Pedestrian;
                    else
                        _state = CrossingState.Default;
                    break;
                }
                default:
                {
                    if(_currentAgents.OfType<HumanoidAgent>().Any())
                        _state = CrossingState.Pedestrian;
                    else if(_currentAgents.OfType<VehicleAgent>().Any())
                        _state = CrossingState.Vehicle;
                    else
                        _state = CrossingState.Default;
                    break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckAgents();
            _currentAgents.Add(other.GetComponent<NavAgentBase>());
            PauseAgents();
        }

        private void OnTriggerExit(Collider other)
        {
            _currentAgents.Remove(other.GetComponent<NavAgentBase>());
            CheckAgents();
            UnpauseAgents();
        }

        private void PauseAgents()
        {
            switch(_state)
            {
                case CrossingState.Default:
                    break;
                case CrossingState.Pedestrian:
                {
                    _currentAgents.OfType<VehicleAgent>().InvokeAction(x => x.IsPaused = true);
                    break;
                }
                case CrossingState.Vehicle:
                {
                    _currentAgents.OfType<HumanoidAgent>().InvokeAction(x => x.IsPaused = true);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnpauseAgents()
        {
            switch(_state)
            {
                case CrossingState.Default:
                {
                    _currentAgents.InvokeAction(x => x.IsPaused = false);
                    break;
                }
                case CrossingState.Pedestrian:
                {
                    _currentAgents.OfType<HumanoidAgent>().InvokeAction(x => x.IsPaused = false);
                    break;
                }
                case CrossingState.Vehicle:
                {
                    _currentAgents.OfType<VehicleAgent>().InvokeAction(x => x.IsPaused = false);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #endregion

        #region  Nested type CrossingState

        private enum CrossingState
        {
            Default,
            Pedestrian,
            Vehicle
        }

        #endregion
    }
}