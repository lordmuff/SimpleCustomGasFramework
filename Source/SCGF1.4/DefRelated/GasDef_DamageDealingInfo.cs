using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SCGF.DefRelated
{
    public class GasDef_DamageDealingInfo
    {

        // Token: 0x04000048 RID: 72
        public int ticksBetweenDamageAttempts = 120;

        // Token: 0x04000049 RID: 73
        public bool tickTimeRandomizer = true;

        // Token: 0x0400004B RID: 75
        public float damageChance = 1f;

        // Token: 0x0400004C RID: 76
        public DamageDef damageDef;

        // Token: 0x0400004D RID: 77
        public FloatRange damageAmountRange = new FloatRange(1f, 1f);

        // Token: 0x0400004E RID: 78
        public FloatRange damagePenetration = new FloatRange(1f, 1f);

        // Token: 0x0400004F RID: 79
        public bool damageStructures = true;

        // Token: 0x04000050 RID: 80
        public float buildingMultiplier = 1f;

        // Token: 0x04000051 RID: 81
        public bool damagesPawns = true;

        // Token: 0x04000052 RID: 82
        public bool ignoreFriendlies;

        // Token: 0x04000053 RID: 83
        public bool onlyDamageExposedParts = true;

        // Token: 0x04000054 RID: 84
        public bool damageCarriedThings = true;

        // Token: 0x04000055 RID: 85
        public bool damageDeadPawns = true;

        // Token: 0x04000056 RID: 86
        public bool damageDownPawns = true;

        // Token: 0x04000057 RID: 87
        public bool canDamageOtherThings = true;

        // Token: 0x04000058 RID: 88
        public bool toxicSensativityMatters;

        // Token: 0x04000059 RID: 89
        public int maxHits;

        // Token: 0x0400005A RID: 90
        public bool damageOtherThings = true;

    }
}
