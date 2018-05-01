﻿/*
The MIT License (MIT)

Copyright (c) 2014 Banbury & Play-Em

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif
using System.Collections;

[CustomEditor(typeof(Skin2D))]
public class Skin2DEditor : Editor {
    private Skin2D skin;
	private SkinnedMeshRenderer skinnedMeshRenderer;
	private Mesh skinnedMesh;

    private float baseSelectDistance = 0.1f;
    private float changedBaseSelectDistance = 0.1f;
	private int selectedIndex = -1;
	private Color handleColor = Color.green;

    void OnEnable() {
        skin = (Skin2D)target;
		skinnedMeshRenderer = skin.GetComponent<SkinnedMeshRenderer>();
		skinnedMesh = skinnedMeshRenderer.sharedMesh;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUILayout.Separator();

        if (GUILayout.Button("Toggle Mesh Outline")) {
            Skin2D.showMeshOutline = !Skin2D.showMeshOutline;
        }

        EditorGUILayout.Separator();

        if (skin.skinnedMeshRenderer.sharedMesh != null && GUILayout.Button("Save as Prefab")) {
            skin.SaveAsPrefab();
        }

		EditorGUILayout.Separator();

        if (skin.skinnedMeshRenderer.sharedMesh != null && GUILayout.Button("Recalculate Bone Weights")) {
            skin.RecalculateBoneWeights();
        }

		EditorGUILayout.Separator();
		handleColor = EditorGUILayout.ColorField("Handle Color", handleColor);
		changedBaseSelectDistance = EditorGUILayout.Slider("Handle Size", baseSelectDistance, 0, 1);
        if(baseSelectDistance != changedBaseSelectDistance) {
            baseSelectDistance = changedBaseSelectDistance;
            EditorUtility.SetDirty(this);
            SceneView.RepaintAll();
        }

        if (skin.skinnedMeshRenderer.sharedMesh != null && GUILayout.Button("Create Control Points")) {
            skin.CreateControlPoints(skin.skinnedMeshRenderer);
        }

        if (skin.skinnedMeshRenderer.sharedMesh != null && GUILayout.Button("Reset Control Points")) {
            skin.ResetControlPointPositions();
        }

        if (skin.points != null && skin.controlPoints != null && skin.controlPoints.Length > 0 
		&& selectedIndex != -1 && GUILayout.Button("Reset Selected Control Point")) {
            skin.controlPoints[selectedIndex].ResetPosition();
			skin.points.SetPoint(skin.controlPoints[selectedIndex]);
        }

        if (GUILayout.Button("Remove Control Points")) {
            skin.RemoveControlPoints();
        }

		EditorGUILayout.Separator();

        if (skin.skinnedMeshRenderer.sharedMesh != null && GUILayout.Button("Generate Mesh Asset")) {
            #if UNITY_EDITOR
			// Check if the Meshes directory exists, if not, create it.
			if(!Directory.Exists("Assets/Meshes")) {
				AssetDatabase.CreateFolder("Assets", "Meshes");
				AssetDatabase.Refresh();
			}
			Mesh mesh = new Mesh();
			mesh.name = skin.skinnedMeshRenderer.sharedMesh.name.Replace(".SkinnedMesh", ".Mesh");;
			mesh.vertices = skin.skinnedMeshRenderer.sharedMesh.vertices;
			mesh.triangles = skin.skinnedMeshRenderer.sharedMesh.triangles;
			mesh.normals = skin.skinnedMeshRenderer.sharedMesh.normals;
			mesh.uv = skin.skinnedMeshRenderer.sharedMesh.uv;
			mesh.uv2 = skin.skinnedMeshRenderer.sharedMesh.uv2;
			mesh.bounds = skin.skinnedMeshRenderer.sharedMesh.bounds;
			ScriptableObjectUtility.CreateAsset(mesh, "Meshes/" + skin.gameObject.name + ".Mesh");
			#endif
        }

        if (skin.skinnedMeshRenderer.sharedMaterial != null && GUILayout.Button("Generate Material Asset")) {
            #if UNITY_EDITOR
			Material material = new Material(skin.skinnedMeshRenderer.sharedMaterial);
			material.CopyPropertiesFromMaterial(skin.skinnedMeshRenderer.sharedMaterial);
			skin.skinnedMeshRenderer.sharedMaterial = material;
			if(!Directory.Exists("Assets/Materials")) {
				AssetDatabase.CreateFolder("Assets", "Materials");
				AssetDatabase.Refresh();
			}
			AssetDatabase.CreateAsset(material, "Assets/Materials/" + material.mainTexture.name + ".mat");
			Debug.Log("Created material " + material.mainTexture.name + " for " + skin.gameObject.name);
			#endif
        }
    }

	// This is for editing control points
	void OnSceneGUI() {
		// If we have a skin and control points then update them
		if (skin != null && skinnedMeshRenderer != null && skinnedMesh != null 
		&& skin.controlPoints != null && skin.controlPoints.Length > 0 && skin.points != null) {
			Event e = Event.current;

			Handles.matrix = skin.transform.localToWorldMatrix;
			EditorGUI.BeginChangeCheck();
			Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
			Vector2 mousePos = r.origin;

			float selectDistance = HandleUtility.GetHandleSize(mousePos) * baseSelectDistance;
			if (e.type == EventType.MouseDrag) {
				skin.editingPoints = true;
			}
			else if (e.type == EventType.MouseUp) {
				skin.editingPoints = false;
			}

			#region Draw vertex handles
			Handles.color = handleColor;
			Mesh mesh = new Mesh();
			skinnedMeshRenderer.BakeMesh(mesh);
			Vector3[] vertices = new Vector3[mesh.vertexCount];

			for (int i = 0; i < mesh.vertexCount; i++) {
				vertices[i] = mesh.vertices[i];
			}

			Vector3[] newVertices = new Vector3[skinnedMeshRenderer.sharedMesh.vertexCount];
			newVertices = skinnedMeshRenderer.sharedMesh.vertices;

			for(int i = 0; i < skin.controlPoints.Length; i++) {
				if (Handles.Button(vertices[i], Quaternion.identity, selectDistance, selectDistance, Handles.CircleHandleCap)) {
					selectedIndex = i;
				}

				if (selectedIndex == i) {
					EditorGUI.BeginChangeCheck();
					Vector3 offset = vertices[i] - skin.transform.TransformPoint(skin.points.GetPoint(skin.controlPoints[i]));

					skin.controlPoints[i].position = skin.transform.InverseTransformPoint(Handles.DoPositionHandle(skin.transform.TransformPoint(skin.points.GetPoint(skin.controlPoints[i])) + offset, Quaternion.identity) - offset);

					if (EditorGUI.EndChangeCheck()) {
						skin.points.SetPoint(skin.controlPoints[i]);
						Undo.RecordObject(skin, "Changed Control Point");
						Undo.RecordObject(skin.points, "Changed Control Point");
						EditorUtility.SetDirty(this);
					}
				}

				if (skin.editingPoints) {
					newVertices[i] = skin.points.GetPoint(skin.controlPoints[i]);
				}
			}

			if (skin.editingPoints) {
				skinnedMeshRenderer.sharedMesh.vertices = newVertices;
			}
			#endregion
		}
	}
}
