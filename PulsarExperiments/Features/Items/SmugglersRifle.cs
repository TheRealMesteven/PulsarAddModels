using System;
using PulsarExperiments.ScaryThings;
using PulsarModLoader.Content.Items;
using UnityEngine;

namespace PulsarExperiments.Features.Items
{
    internal class SmugglersRifleMod : ItemMod
    {
        public override string Name => "SmugglersRifle";
        public override PLPawnItem PLPawnItem => new SmugglersRifle();

        public class SmugglersRifle : PLPawnItem_PhasePistol
        {
            public SmugglersRifle() : base()
            {
                m_MarketPrice = 2000;
                MinAutoFireDelay = 0.05f;
                AIEffectiveRange = 25f;
                Desc = "Upgraded version of the smugglers pistol";
                Name = "Smugglers Rifle";
                UsesAmmo = true;
                AmmoMax = 30;
                AmmoCurrent = 30;
            }

            protected override GameObject GetGunPrefab() => Prefabs.SmugglersRifle;

            protected override float CalcDamageDone() => 38f + 8f * (float)base.Level;

            public override string GetItemName(bool skipLocalization = false) => "Smugglers Rifle";

            public override void FireShot(Vector3 aimAtPoint, Vector3 destNormal, int newBoltID, Collider hitCollider)
            {
                if (MySetupPawn != null)
                {
                    base.FireShot(aimAtPoint, destNormal, newBoltID, hitCollider);
                    Heat += 0.05f;
                    MySetupPawn.MyIK.ShotFeedbackAmt += 1f;
                    MySetupPawn.CurrentAccuracyRating += 1f;
                }
            }
            /*
            public override void OnUpdate()
            {
                base.OnUpdate();
                MySetupPawn.MyIK.rightHandEffector_OriginalLocalPos = MyGunInstance.transform.Find("Right Hand").position;
                //MySetupPawn.MyIK.Gun_diffPos_RightToLeftHand = MyGunInstance.transform.Find("Left Hand").position - MySetupPawn.MyIK.rightHandEffector_OriginalLocalPos;
            }

            public static Vector3 DefaultRightHandPos = new Vector3(0.09f, -0.2f, 0.2f);

            public override void OnInActive()
            {
                base.OnInActive();
                MySetupPawn.MyIK.rightHandEffector_OriginalLocalPos = DefaultRightHandPos; // reset
            }

            public override void UnSetup()
            {
                base.UnSetup();
                MySetupPawn.MyIK.rightHandEffector_OriginalLocalPos = DefaultRightHandPos; // reset
            }
            */
        }
    }
}
