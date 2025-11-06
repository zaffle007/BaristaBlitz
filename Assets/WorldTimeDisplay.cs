//all code is taken from https://www.youtube.com/watch?v=0nq1ZFxuEJY
using System;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

namespace WorldTime
{
    [RequireComponent(typeof(TMP_Text))]
    public class WorldTimeDisplay : MonoBehaviour
    {
        [SerializeField]
        private WorldTime worldTime;
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            worldTime.WorldTimeChanged += OnWorldTimeChanged;

        }

        private void OnDestroy()
        {
            worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            text.SetText(sourceText: newTime.ToString(format: @"hh\:mm"));
        }
    }
}
