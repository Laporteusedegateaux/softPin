using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;

enum PinState
{
    Off = 0,
    On = 1,
}

enum PinType
{
    MeshInstance3D = 0,
    Skeleton3D = 1,
}

[Icon("res://addons/soft_pin/softPin Icon.svg")]
[Tool]
public partial class SoftPin : Node
{

	[Export] SoftBody3D softBody;
    [Export] BoneAttachment3D boneAttachment;
	[Export] PinType pinType;
    [Export] PinState startPinning;
    [Export] string pinningStatus = "No softbody assigned";

	private int vertAmount;
    private Color _white = new Color(1, 1 ,1, 1);
	private Color _black = new Color(0, 0, 0, 1);
    private Mesh _mesh;
    private Skeleton3D _skeleton;
	
    //init mesh tool and Arrays
    private MeshDataTool _meshDataTool = new();
	private ArrayMesh _arrayMesh = new();
	private Godot.Collections.Array _arrays = new();


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (Engine.IsEditorHint())
		{
			switch (startPinning)
			{
				case PinState.Off:
					break;
				case PinState.On:
                    AssignPins(); startPinning = 0; ;
                    break;

			}

        }

	}

	void AssignPins()
	{
		if (softBody != null )
		{
			//Set the Mesh resource, Godot Collections Array, Array Mesh , and Mesh Data Tool
			_mesh = softBody.Mesh;
			_arrays = _mesh.SurfaceGetArrays(0);
			_arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, _arrays);
			_meshDataTool.CreateFromSurface(_arrayMesh, 0);
			pinningStatus = $"Pinning...";

            //Gets the number of vertices to check
            vertAmount = _meshDataTool.GetVertexCount();
           

            //Gets Vertex Color and Sets Softbody Pins depending on type selected
            switch (pinType)
			{
				case PinType.MeshInstance3D:

                    if (boneAttachment != null) { GD.PrintErr("PINTYPE ERROR! SOFTPIN ACTION REQUIRED: SWITCH PINTYPE TO SKELETON3D WHEN USING BONE ATTACHMENT"); pinningStatus = "PINTYPE ERROR! Switch to Skeleton3D "; return; }
                    
                    for (int i = 0; i < vertAmount; i++)
                    {
                        Color vertexColor = _meshDataTool.GetVertexColor(i);
                        if (vertexColor != _white && vertexColor != _black)
                        {
                            // COLOR DETECTED! NO PIN!
                            GD.Print($"VERTEX COLOR DETECTED: {vertexColor} in {i}");
                        }

                        else {  softBody.SetPointPinned(i, true); GD.Print($"NO COLOR DETECTED: {vertexColor} in {i}"); } //NO COLOR DETECTED SET PIN!
                    }

                    pinningStatus = $"{softBody.Name} Complete!";
                    break;
                
                case PinType.Skeleton3D:
                    
                    if (boneAttachment != null)
                    {
                        string bonePath = $"{softBody.GetPathTo(boneAttachment)}";

                        for (int i = 0; i < vertAmount; i++)
                        {
                            Color vertexColor = _meshDataTool.GetVertexColor(i);
                            //var boneList = _meshDataTool.GetVertexBones(i);

                            if (vertexColor != _white && vertexColor != _black)
                            {
                                // COLOR DETECTED! NO PIN!
                                GD.Print($"VERTEX COLOR DETECTED: {vertexColor} in {i}");
                            }

                            else
                            {
                                softBody.SetPointPinned(i, true, bonePath);
                                GD.Print($"NO COLOR DETECTED: {vertexColor} in {i} // BONE ATTACHED: {boneAttachment}");
                            }

                        }

                        if (softBody.Skeleton != null) { softBody.Skeleton = null; GD.Print("Skeleton Removed"); pinningStatus = $"{softBody.Name} Complete!"; } // Remove Skeleton from SoftBody. (Skeleton prevents the SB from activating.)
                    }

                    else { GD.PrintErr("NULL REF! SOFTPIN ACTION REQUIRE: ASSIGN BONEATTACHMENT3D"); pinningStatus = "NULL REF! Assign BoneAttachment "; return; }
                    break ;
			}
			



            //clear 
            GD.Print($"SOFTPIN: {softBody.Name} PINNING COMPLETE!");
            
            _arrayMesh.ClearSurfaces();
			_meshDataTool.Clear();
           
			 softBody = null;
             boneAttachment = null;
		}


		else { GD.PrintErr("NULL REF! SOFTPIN ACTION REQUIRE: ASSIGN SOFTBODY"); pinningStatus = "NULL REF! Assign Softbody"; }

		
	}
}
