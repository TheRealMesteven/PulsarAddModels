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
					SkinnedMeshCopier(uniformMeshes.FirstOrDefault(), ref AddMaleUniforms);
                    uniformMeshes.AddRange(AddMaleUniforms);
				}
				else
				{
                    SkinnedMeshCopier(uniformMeshes.FirstOrDefault(), ref AddFemaleUniforms);
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
		static void SkinnedMeshCopier(Mesh sourceMesh, ref List<Mesh> Meshes)
		{
			foreach (Mesh targetMesh in Meshes)
			{
                if (sourceMesh == null || targetMesh == null)
                {
                    PulsarModLoader.Utilities.Logger.Info("[SMC] Source or target Mesh is not assigned!");
                    return;
                }

                // Ensure sourceMesh and targetMesh have the same vertex count
                if (sourceMesh.vertexCount != targetMesh.vertexCount)
                {
                    PulsarModLoader.Utilities.Logger.Info("[SMC] Source and target meshes have different vertex counts!");
                    return;
                }

                // Copy vertex data
                targetMesh.vertices = sourceMesh.vertices;

                // Copy triangle data
                targetMesh.triangles = sourceMesh.triangles;

                // Copy normals
                targetMesh.normals = sourceMesh.normals;

                // Copy colors
                targetMesh.colors = sourceMesh.colors;

                // Copy UVs
                targetMesh.uv = sourceMesh.uv;
                targetMesh.uv2 = sourceMesh.uv2;
                targetMesh.uv3 = sourceMesh.uv3;
                targetMesh.uv4 = sourceMesh.uv4;

                // Copy tangents
                targetMesh.tangents = sourceMesh.tangents;

                // Copy bind poses
                targetMesh.bindposes = sourceMesh.bindposes;

                // Copy bone weights
                targetMesh.boneWeights = sourceMesh.boneWeights;

                // Copy blend shape count
                for (int i = 0; i < sourceMesh.blendShapeCount; i++)
                {
                    string blendShapeName = sourceMesh.GetBlendShapeName(i);
                    int frameCount = sourceMesh.GetBlendShapeFrameCount(i);
                    for (int j = 0; j < frameCount; j++)
                    {
                        float weight = sourceMesh.GetBlendShapeFrameWeight(i, j);
                        Vector3[] deltaVertices = new Vector3[sourceMesh.vertexCount];
                        Vector3[] deltaNormals = new Vector3[sourceMesh.vertexCount];
                        Vector3[] deltaTangents = new Vector3[sourceMesh.vertexCount];
                        sourceMesh.GetBlendShapeFrameVertices(i, j, deltaVertices, deltaNormals, deltaTangents);

                        // Check if the target mesh supports adding blend shape frames
                        if (targetMesh.blendShapeCount <= i)
                        {
                            PulsarModLoader.Utilities.Logger.Info($"[SMC] Target mesh does not support blend shape {blendShapeName}!");
                            continue;
                        }

                        // Add blend shape frame
                        targetMesh.AddBlendShapeFrame(blendShapeName, weight, deltaVertices, deltaNormals, deltaTangents);
                    }
                }

                // Copy bounds
                targetMesh.bounds = sourceMesh.bounds;

                // Copy other properties
                targetMesh.indexFormat = sourceMesh.indexFormat;
                targetMesh.subMeshCount = sourceMesh.subMeshCount;
                targetMesh.UploadMeshData(false);
                targetMesh.Clear(false);
            }
        }


        private Patch() { }
		private static Patch Instance = new Patch();
	}
}
