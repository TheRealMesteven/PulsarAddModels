using System;
using System.IO;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Linq;
using PulsarExperiments.Features.PawnAppearance;
using System.Collections.Generic;

namespace PulsarExperiments
{
	public static class Prefabs // PulsarExperiments.Prefabs
	{
		public static GameObject SpaceEagle;
		public static GameObject BlackDEagle;

		public static GameObject SmugglersRifle;
		public static Material Leather;
        public static Material Generic_15;
        public static Material Generic_16_Dark;
        public static Material RazorCrystal_Green;

        public static GameObject Katana;
		public static GameObject FireAxe;
		public static GameObject Knife;

		public static GameObject EngTablet;

		public static GameObject ScaryyyyUfo;
		public static GameObject UfoWithInterior;

		public static TextAsset NavForUFO;

		internal static AssetBundle bundle;

		public static void LoadPrefabs()
		{
			if (bundle != null)
				bundle.Unload(true);

			var assetbundlePath = PulsarModLoader.ModManager.GetModsDir() + "\\experiments.bundle";
			bundle = AssetBundle.LoadFromFile(assetbundlePath);

			SpaceEagle = bundle.LoadAsset<GameObject>("SpaceEaglePrefab");
			if (SpaceEagle == null)
				throw new Exception("Cant load SpaceEagle!");

            BlackDEagle = bundle.LoadAsset<GameObject>("Black Desert Eagle Prefab");
            if (BlackDEagle == null)
                throw new Exception("Cant load Black Desert Eagle!");

            SmugglersRifle = bundle.LoadAsset<GameObject>("Smugglers Rifle Prefab");
			if (SmugglersRifle == null)
				throw new Exception("Cant load SmugglersRifle!");
			/*
            Leather = bundle.LoadAsset<Material>("Generic_16_Leather 1");
			if (Leather == null)
			{
                SmugglersTommyGun.GetComponent<Renderer>().materials[0] = Leather;
                throw new Exception("Cant load material Leather!");
			}

            Generic_15 = bundle.LoadAsset<Material>("Generic_15");
			if (Generic_15 == null)
			{
                SmugglersTommyGun.GetComponent<Renderer>().materials[2] = Generic_15;
                throw new Exception("Cant load material Generic_15!");
			}

            Generic_16_Dark = bundle.LoadAsset<Material>("Generic_16_Dark");
			if (Generic_16_Dark == null)
			{
                SmugglersTommyGun.GetComponent<Renderer>().materials[1] = Generic_16_Dark;
                throw new Exception("Cant load material Generic_16_Dark!");
			}

            RazorCrystal_Green = bundle.LoadAsset<Material>("RazorCrystal_Green");
			if (RazorCrystal_Green == null)
			{
                SmugglersTommyGun.GetComponent<Renderer>().materials[3] = RazorCrystal_Green;
                throw new Exception("Cant load material RazorCrystal_Green!");
			}
			*/
			Katana = bundle.LoadAsset<GameObject>("KatanaPrefab");
			if (Katana == null)
				throw new Exception("Cant load Katana!");

			FireAxe = bundle.LoadAsset<GameObject>("FireAxePrefab");
			if (FireAxe == null)
				throw new Exception("Cant load FireAxe!");

			Knife = bundle.LoadAsset<GameObject>("knifePrefab");
			if (Knife == null)
				throw new Exception("Cant load Knife!");

			EngTablet = bundle.LoadAsset<GameObject>("EngTabletPrefab");
			if (EngTablet == null)
				throw new Exception("Cant load EngTablet!");
			Features.Items.EngTabletMod.EngTablet.FixPrefabShader(EngTablet);

			ScaryyyyUfo = bundle.LoadAsset<GameObject>("ufoPrefab");
			if (Katana == null)
				throw new Exception("Cant load UFO!");

			NavForUFO = bundle.LoadAsset<TextAsset>("NavForUFO");

			UfoWithInterior = bundle.LoadAsset<GameObject>("ufoIntPrefab");

			Features.Ships.Utils.FixShields(ScaryyyyUfo.transform.Find("Exterior").Find("ShieldBubble").GetComponent<MeshRenderer>());
			Features.Ships.Utils.FixShields(UfoWithInterior.transform.Find("Exterior").Find("ShieldBubble").GetComponent<MeshRenderer>());

			//foreach (var i in bundle.LoadAllAssets<Mesh>())
			//	PulsarModLoader.Utilities.Logger.Info($"{i.name}");
			
			Features.PawnAppearance.Patch.AddRobotFaces.Add(bundle.LoadAsset<Mesh>("Pyro"));
			Features.PawnAppearance.Patch.AddRobotFaces.Add(bundle.LoadAsset<Mesh>("sphere"));

			Mesh MaleOutfit = bundle.LoadAsset<Mesh>("WD_Uniform_01_Male_2");
			if (MaleOutfit == null)
				throw new Exception("Cant load Male Outfit!");
			Features.PawnAppearance.Patch.AddMaleUniforms.Add(MaleOutfit);

            Mesh RobotOutfit = bundle.LoadAsset<Mesh>("HumanoidDrone_01");
            if (RobotOutfit == null)
                throw new Exception("Cant load Robot Outfit!");
            Features.PawnAppearance.Patch.AddMaleUniforms.Add(RobotOutfit);

            PhotonNetwork.PrefabCache.Add("NetworkPrefabs/UFO", ScaryyyyUfo);
			PhotonNetwork.PrefabCache.Add("NetworkPrefabs/UFOWithInterior", UfoWithInterior);

            GameObject Mohawk = bundle.LoadAsset<GameObject>("HumanFemale_Hair_03_0");
            if (Mohawk == null)
                throw new Exception("Cant load Mohawk!");
            Features.PawnAppearance.PatchHairTransform.HairTransform.Add(Mohawk.name, new SizeChanges
			(
                new Vector3(0f, -0.102f, 0.015f),
                Quaternion.identity,
                new Vector3(0.14f, 0.17f, 0.132f)
            ));
			Features.PawnAppearance.Patch.AddMaleHair.Add(Mohawk);

            GameObject Scruffy = bundle.LoadAsset<GameObject>("HumanFemale_Hair_05_0");
            if (Scruffy == null)
                throw new Exception("Cant load Scruffy!");
            Features.PawnAppearance.PatchHairTransform.HairTransform.Add(Scruffy.name, new SizeChanges
            (
                new Vector3(0f, -0.04f, 0.005f),
                Quaternion.identity,
                new Vector3(0.126f, 0.132f, 0.155f)
            ));
            Features.PawnAppearance.Patch.AddMaleHair.Add(Scruffy);
        }
	}

	[HarmonyPatch]
	internal static class FixPrefabCache
	{
		public static MethodBase TargetMethod()
		{
			var type = AccessTools.TypeByName("NetworkingPeer");
			return AccessTools.Method(type, "DoInstantiate");
		}

		public static void Prefix(ExitGames.Client.Photon.Hashtable evData, PhotonPlayer photonPlayer, ref GameObject resourceGameObject)
		{
			if (resourceGameObject == null)
			{
				string text = (string)evData[0];
				PhotonNetwork.PrefabCache.TryGetValue(text, out resourceGameObject);
			}
		}
	}
}
