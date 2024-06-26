﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback lets you control the stereo pan of a target AudioSource over time.
    /// </summary>
    [AddComponentMenu("")]
    [FeedbackPath("Audio/AudioSource Stereo Pan")]
    [FeedbackHelp("This feedback lets you control the stereo pan of a target AudioSource over time.")]
    public class MMFeedbackAudioSourceStereoPan : MMFeedback
    {
        /// sets the inspector color for this feedback
        #if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.SoundsColor; } }
        #endif
        /// returns the duration of the feedback
        public override float FeedbackDuration { get { return Duration; } }

        [Header("StereoPan Feedback")]
        /// the channel to emit on
        public int Channel = 0;
        /// the duration of the shake, in seconds
        public float Duration = 2f;
        /// whether or not to reset shaker values after shake
        public bool ResetShakerValuesAfterShake = true;
        /// whether or not to reset the target's values after shake
        public bool ResetTargetValuesAfterShake = true;

        [Header("StereoPan")]
        /// whether or not to add to the initial value
        public bool RelativeStereoPan = false;
        /// the curve used to animate the intensity value on
        public AnimationCurve ShakeStereoPan = new AnimationCurve(new Keyframe(0, 0f), new Keyframe(0.3f, 1f), new Keyframe(0.6f, -1f), new Keyframe(1, 0f));
        /// the value to remap the curve's 0 to
        [Range(-1f, 1f)]
        public float RemapStereoPanZero = 0f;
        /// the value to remap the curve's 1 to
        [Range(-1f, 1f)]
        public float RemapStereoPanOne = 1f;


        /// <summary>
        /// Triggers the corresponding coroutine
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attenuation"></param>
        protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1.0f)
        {
            if (Active)
            {
                MMAudioSourceStereoPanShakeEvent.Trigger(ShakeStereoPan, Duration, RemapStereoPanZero, RemapStereoPanOne, RelativeStereoPan,
                    attenuation, Channel, ResetShakerValuesAfterShake, ResetTargetValuesAfterShake);
            }
        }
    }
}
