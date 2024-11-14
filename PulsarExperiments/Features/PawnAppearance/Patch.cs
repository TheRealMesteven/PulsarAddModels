using System.Collections.Generic;
using System.Collections;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using static PulsarModLoader.Patches.HarmonyHelpers;
using static HarmonyLib.AccessTools;
using System.Reflection.Emit;

namespace PulsarExperiments.Features.PawnAppearance
{
    #region Adding options to list
    [HarmonyPatch(typeof(PLCustomPawn))]
	public class Patch
	{
		public static List<Mesh> AddRobotFaces = new List<Mesh>();
        public static List<GameObject> AddMaleHair = new List<GameObject>();
        public static List<GameObject> AddFemaleHair = new List<GameObject>();
        public static List<Mesh> AddMaleUniforms = new List<Mesh>();
        public static List<Mesh> AddFemaleUniforms = new List<Mesh>();
        public static List<Mesh> AddRobotUniforms = new List<Mesh>();

        [HarmonyPostfix, HarmonyPatch("Start")]
		static void Start(PLCustomPawn __instance)
		{
			__instance.StartCoroutine(Instance.SetFaceMeshes(__instance));
            __instance.StartCoroutine(Instance.SetHairPrefabs(__instance));
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

        IEnumerator SetHairPrefabs(PLCustomPawn __instance)
        {
            while (__instance.MyPawn == null || __instance.MyPawn.MyPlayer == null)
                yield return null;

            var HairGameObjects = __instance.HairPrefabs.ToList();
            if (__instance.MyPawn.m_MyPlayer.RaceID == 0) // for humans
            {
                if (__instance.MyPawn.m_MyPlayer.Gender_IsMale)
                {
                    HairGameObjects.AddRange(AddMaleHair);
                }
                else
                {
                    HairGameObjects.AddRange(AddFemaleHair);
                }
            }
            __instance.HairPrefabs = HairGameObjects.ToArray();

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
    #endregion
    #region Patching scaling changes
    public struct SizeChanges
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public SizeChanges(Vector3 _position, Quaternion _rotation, Vector3 _scale)
        {
            Position = _position;
            Rotation = _rotation;
            Scale = _scale;
        }
    }

    [HarmonyPatch(typeof(PLCustomPawn), "BeforeUpdate")]
    class PatchHairTransform
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> target = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, Field(typeof(PLCustomPawn), "CurrentHairObj")),
                new CodeInstruction(OpCodes.Callvirt),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(UnityEngine.Transform), "set_localScale")),
            }; // this.CurrentHairObj.transform.localScale = Vector3.one;

            int LabelIndex = FindSequence(instructions, target, CheckMode.NONNULL);

            List<CodeInstruction> patch = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_0), // this
                instructions.ToList()[LabelIndex - 4], // CurrentHairObj
                /*new CodeInstruction(OpCodes.Ldarg_0), // this
                instructions.ToList()[LabelIndex - 25], // HairID*/
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(PatchHairTransform), "Patch"))
            };

            return PatchBySequence(instructions, target, patch, PatchMode.AFTER, CheckMode.NONNULL, showDebugOutput: false);
        }
        public static Dictionary<string, SizeChanges> HairTransform = new Dictionary<string, SizeChanges>();
        public static void Patch(GameObject CurrentHairObj)
        {
            string trimmedName = CurrentHairObj.name.Replace("(Clone)", "");
            if (HairTransform.ContainsKey(trimmedName))
            {
                CurrentHairObj.transform.localPosition = HairTransform[trimmedName].Position;
                CurrentHairObj.transform.localRotation = HairTransform[trimmedName].Rotation;
                CurrentHairObj.transform.localScale = HairTransform[trimmedName].Scale;
            }
        }
    }
    #endregion
}
