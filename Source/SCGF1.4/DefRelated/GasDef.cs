﻿using SCGF.DefRelated;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SCGF
{
    /// <summary>
    /// This defines the various properties a GasDef should have, as well as their default values.
    /// </summary>
    public class GasDef : Def
    {
        public Material material;

        public Color color;

        public int dissipationSpeed = 4;

        public bool canEqualizeThroughBuildings = true;

        public int diffusionThreshold = 51;

        public bool isToxic = false;

        public bool targetHumans = true;

        public bool targetAnimals = true;

        public bool targetMechs = false;

        public bool targetInsects = true;

        public List<GasDef_AddsHediff> addsHediffs = null;

        public List<GasDef_DamageDealing> dealsDamage = null;

        public GasDef_Immunities immunities = null;

        public float diffusionLossModifier = 1.0f;

        public float diffusionGainModifier = 1.0f;

        public GasType? realGasType;

        public Type workerClass = typeof(GasWorker);

        [Unsaved(false)]
        private GasWorker workerInt;

        [Unsaved(false)]
        public int customIndex;

        public GasWorker Worker
        {
            get
            {
                if (workerInt == null)
                {
                    workerInt = (GasWorker)Activator.CreateInstance(workerClass);
                    workerInt.gasDef = this;
                    workerInt.Initialize();
                }

                return workerInt;
            }
        }
    }
}
