using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SCGF.DefRelated
{

    public class GasDef_DamageDealing : GasDef
    {

        private void ConductDamageDeterimination()
        {

            this.pawnsInGas = this.GetPawnsInGas();
            foreach (GasDef_DamageDealingInfo gasDamageInfo in this.definitionData.gasDamageInfos)
            {
                if (gasDamageInfo.damagesPawns || gasDamageInfo.damageCarriedThings)
                {
                    this.AttemptDealDamageToPawns(gasDamageInfo);
                }
                if (gasDamageInfo.damageOtherThings)
                {
                    this.AttemptDealDamageToNonPawns(gasDamageInfo);
                }
            }
        }

        private void AttemptDealDamageToNonPawns(GasDef_DamageDealingInfo gasDamageInfo)
        {
            DamageInfo dinfo = new DamageInfo(gasDamageInfo.damageDef, gasDamageInfo.damageAmountRange.Average, gasDamageInfo.damagePenetration.Average, -1f, this.orginalInstigator, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true, QualityCategory.Normal, true);
            foreach (Thing thing in this.nonPawnsInGas)
            {
                if (thing != null && thing.Spawned)
                {
                    thing.TakeDamage(dinfo);
                }
            }
        }

        private void AttemptDealDamageToPawns(GasDef_DamageDealingInfo gasDamageInfo)
        {
            if (gasDamageInfo == null)
            {
                Log.Warning("Null gas damage info detected");
                return;
            }
            foreach (Pawn pawn in this.pawnsInGas)
            {
                if ((pawn.Spawned || pawn != null) && Rand.Chance(gasDamageInfo.damageChance))
                {
                    if (gasDamageInfo.damagesPawns && this.IsPawnDamageable(pawn, gasDamageInfo))
                    {
                        this.DamagePawn(pawn, gasDamageInfo);
                    }
                    if (gasDamageInfo.damageCarriedThings)
                    {
                        this.DamageCarriedThings(pawn, gasDamageInfo);
                    }
                }
            }
        }

        private void DamageCarriedThings(Pawn pawn, GasDef_DamageDealingInfo gasDamageInfo)
        {
            DamageInfo dinfo = new DamageInfo(gasDamageInfo.damageDef, gasDamageInfo.damageAmountRange.Average, gasDamageInfo.damagePenetration.Average, -1f, this.orginalInstigator, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true, QualityCategory.Normal, true);
            foreach (Thing thing in pawn.EquippedWornOrInventoryThings)
            {
                if (thing != null && thing.Spawned && thing.HitPoints > 0)
                {
                    thing.TakeDamage(dinfo);
                }
            }
        }

        private void DamageThings(Thing thing, GasDef_DamageDealingInfo gasDamageInfo)
        {
        }

        private void DamagePawn(Pawn pawn, GasDef_DamageDealingInfo gasDamageInfo)
        {
            List<BodyPartRecord> list = new List<BodyPartRecord>();
            List<Apparel> list2 = new List<Apparel>();
            if (!pawn.RaceProps.Animal)
            {
                list2 = pawn.apparel.WornApparel;
            }
            foreach (BodyPartRecord bodyPartRecord in pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Outside, null, null))
            {
                if (list2.Count > 0 && gasDamageInfo.onlyDamageExposedParts)
                {
                    bool flag = false;
                    for (int i = 0; i < list2.Count; i++)
                    {
                        if (list2[i].def.apparel.CoversBodyPart(bodyPartRecord))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        list.Add(bodyPartRecord);
                    }
                }
                else
                {
                    list.Add(bodyPartRecord);
                }
            }
            if (list.Count == 0)
            {
                return;
            }
            List<BodyPartRecord> list3 = new List<BodyPartRecord>();
            if (gasDamageInfo.maxHits <= 0)
            {
                list3 = list;
            }
            else
            {
                int maxHits = gasDamageInfo.maxHits;
                if (maxHits >= list.Count)
                {
                    list3 = list;
                }
                else
                {
                    do
                    {
                        BodyPartRecord item = list.RandomElement<BodyPartRecord>();
                        list3.Add(item);
                    }
                    while (list3.Count < maxHits);
                }
            }
            float randomInRange = gasDamageInfo.damageAmountRange.RandomInRange;
            float randomInRange2 = gasDamageInfo.damagePenetration.RandomInRange;
            DamageInfo dinfo = new DamageInfo(gasDamageInfo.damageDef, randomInRange, randomInRange2, -1f, this.orginalInstigator, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null, true, true, QualityCategory.Normal, true);
            foreach (BodyPartRecord bodyPartRecord2 in list3)
            {
                if (bodyPartRecord2.def.GetHitChanceFactorFor(dinfo.Def) > 0f)
                {
                    dinfo.SetHitPart(bodyPartRecord2);
                    dinfo.SetAmount(gasDamageInfo.damageAmountRange.RandomInRange);
                    pawn.TakeDamage(dinfo);
                }
            }
        }

        internal void SetInstigator(Thing instigator)
        {
            this.orginalInstigator = instigator;
            this.instigationFaction = instigator.Faction;
        }

        private bool IsPawnDamageable(Pawn pawn, GasDef_DamageDealingInfo gasDamageInfo)
        {
            bool flag = false;
            bool flag2 = true;
            if (pawn.Dead && gasDamageInfo.damageDeadPawns)
            {
                flag = true;
            }
            else if (pawn.Downed && gasDamageInfo.damageDownPawns)
            {
                flag = true;
            }
            else if (!pawn.Dead && !pawn.Downed)
            {
                flag = true;
            }
            if (gasDamageInfo.ignoreFriendlies)
            {
                flag2 = (this.instigationFaction == null || pawn.HostileTo(this.instigationFaction));
            }
            return flag && flag2;
        }

        internal void SetInstigatorFaction(Faction faction)
        {
            this.instigationFaction = faction;
        }

        private List<Pawn> GetPawnsInGas()
        {
            List<Pawn> result = this.pawnsInGas = new List<Pawn>();
            foreach (Thing thing in this.thingsInGas)
            {
                if (thing.Spawned && !thing.Destroyed)
                {
                    Pawn pawn = thing as Pawn;
                    if (pawn != null)
                    {
                        this.pawnsInGas.Add(pawn);
                    }
                    else
                    {
                        this.nonPawnsInGas.Add(thing);
                    }
                }
            }
            return result;
        }

        private List<Pawn> pawnsInGas;

        private List<Thing> thingsInGas;

        private List<Thing> nonPawnsInGas;

        private GasDef_DamageDealing definitionData;

        private Faction instigationFaction;

        private Thing orginalInstigator;

        public List<GasDef_DamageDealingInfo> gasDamageInfos;

    }

}
