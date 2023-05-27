using System;
using System.IO;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

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

			var assetbundlePath = Path.Combine(new FileInfo(typeof(Prefabs).Assembly.Location).Directory.FullName, "experiments.bundle");
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
			Mesh MaleOutfit = bundle.LoadAsset<Mesh>("WD Admiral Uniform");
			if (MaleOutfit == null)
				throw new Exception("Cant load Male Outfit!");
			else 
			{ 
				Features.PawnAppearance.Patch.AddMaleUniforms.Add(MaleOutfit);
			}

            PhotonNetwork.PrefabCache.Add("NetworkPrefabs/UFO", ScaryyyyUfo);
			PhotonNetwork.PrefabCache.Add("NetworkPrefabs/UFOWithInterior", UfoWithInterior);
			
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
