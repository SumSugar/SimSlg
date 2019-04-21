using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInfoUI : MonoBehaviour {
    /// <summary>
    /// an enum for easily keeping track of UI animation
    /// </summary>
    public enum AnimationState
    {
        /// <summary>
        /// The UI is completely hidden
        /// </summary>
        Hidden,

        /// <summary>
        /// The UI is animation to be shown
        /// </summary>
        Showing,

        /// <summary>
        /// the UI is completely shown
        /// </summary>
        Shown,

        /// <summary>
        /// The UI is animating 
        /// </summary>
        Hiding
    }

    /// <summary>
    /// The name of the clip that shows the UI
    /// </summary>
    public string showClipName = "Show";

    /// <summary>
    /// The name of the clip that hides the UI
    /// </summary>
    public string hideClipName = "Hide";


}
