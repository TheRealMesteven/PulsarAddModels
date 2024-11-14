using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Bindings;
using System.Diagnostics;

namespace PulsarExperiments.Features.PawnAppearance
{
	[HarmonyPatch(typeof(PLCustomPawn))]
	public class Patch
	{
		public static List<Mesh> AddRobotFaces = new List<Mesh>();
		public static List<Mesh> AddMaleUniforms = new List<Mesh>();
        public static List<Mesh> AddFemaleUniforms = new List<Mesh>();
        public static List<Mesh> AddRobotUniforms = new List<Mesh>();

        [HarmonyPostfix, HarmonyPatch("Start")]
		static void Start(PLCustomPawn __instance)
		{
			__instance.StartCoroutine(Instance.SetFaceMeshes(__instance));
            __instance.StartCoroutine(Instance.SetUniformMeshes(__instance));
        }

		IEnumerator SetFaceMeshes(PLCustomPawn __instance)
		{
			while (__instance.MyPawn == null || __instance.MyPawn.MyPlayer == null)
				yield return null;

			var faceMeshes = __instance.FaceMeshes.ToList();
			if (__instance.MyPawn.m_MyPlayer.RaceID == 2) // for robots
			{
				faceMeshes.AddRange(AddRobotFaces);
				__instance.CurrentBakedFaceID = -1;
			}
			__instance.FaceMeshes = faceMeshes.ToArray();

			yield break;
		}
        IEnumerator SetUniformMeshes(PLCustomPawn __instance)
        {
            while (__instance.MyPawn == null || __instance.MyPawn.MyPlayer == null)
                yield return null;

            var uniformMeshes = __instance.UniformMeshes.ToList();
            if (__instance.MyPawn.m_MyPlayer.RaceID == 0) // for humans
            {
				if (__instance.MyPawn.m_MyPlayer.Gender_IsMale)
				{
                    uniformMeshes.AddRange(AddMaleUniforms);
				}
				else
				{
                    uniformMeshes.AddRange(AddFemaleUniforms);
                }
            }
			// sylvassi setup differently, hardcoded outfits.
			else if (__instance.MyPawn.m_MyPlayer.RaceID == 2) // for robots
			{
                uniformMeshes.AddRange(AddRobotUniforms);
            }
            __instance.UniformMeshes = uniformMeshes.ToArray();

            yield break;
        }


        private Patch() { }
		private static Patch Instance = new Patch();
	}
}
