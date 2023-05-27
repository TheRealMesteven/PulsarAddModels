using System;
using PulsarModLoader.Content.Items;
using UnityEngine;

namespace PulsarExperiments.Features.Items
{
	internal class BlackDesertEagleMod : ItemMod
	{
		public override string Name => "DesertEagle";
		public override PLPawnItem PLPawnItem => new BlackDesertEagle();

		public class BlackDesertEagle : PLPawnItem_PierceLaserPistol
		{
			public BlackDesertEagle() : base()
			{
				m_MarketPrice = 1000;
				MinAutoFireDelay = 0.5f;
				AIEffectiveRange = 25f;
				Desc = "Unpopular AOG Weapon";
				Name = "DesertEagle";
				UsesAmmo = true;
				AmmoMax = 7;
				AmmoCurrent= 7;
			}
            public override GameObject CreateBoltGO()
            {
                GameObject Bolt = base.CreateBoltGO();
                Bolt.GetComponent<PLBolt>().AddData(new BoltColourData(this.MyGunInstance.MuzzleFlash.main.startColor.color));
                return Bolt;
            }

            public override GameObject GetGunPrefab() => Prefabs.BlackDEagle;

            public override float CalcDamageDone() => 38f + 8f * (float)base.Level;

			public override string GetItemName(bool skipLocalization = false) => "Desert Eagle";

			public override void FireShot(Vector3 aimAtPoint, Vector3 destNormal, int newBoltID, Collider hitCollider)
			{
				if (MySetupPawn != null)
				{
					base.FireShot(aimAtPoint, destNormal, newBoltID, hitCollider);
					Heat += 0.3f;
					MySetupPawn.MyIK.ShotFeedbackAmt += 4f;
					MySetupPawn.CurrentAccuracyRating += 3f;
				}
			}
		}
	}
}
