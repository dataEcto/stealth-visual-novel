using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.TopDownEngine
{
    
    //A hacked version that combines the FOV script I coded with the AI Decision 
    //Hopefully this makes it rotate
    
    //[RequireComponent(typeof(FieldOfView))]
    public class AIDecisonFOV : AIDecision
    {
        protected FieldOfView fovScript;
        
        
        /// <summary>
        /// On Init we grab our FOV Script
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
            fovScript = GameObject.FindWithTag("FOV").GetComponent<FieldOfView>();
        }

        /// <summary>
        /// On Decide we look for a target
        /// </summary>
        /// <returns></returns>
        public override bool Decide()
        {
            return DetectTarget();
        }

        /// <summary>
        /// If the ViewVisualization has at least one target, it becomes our new brain target and this decision is true, otherwise it's false.
        /// </summary>
        /// <returns></returns>
        protected virtual bool DetectTarget()
        {
            if (fovScript.visibleTargets.Count == 0)
            {
                _brain.Target = null;
                return false;
            }
            else
            {
                _brain.Target = fovScript.visibleTargets[0];
                return true;
            }
        }
        
        
    }
}



