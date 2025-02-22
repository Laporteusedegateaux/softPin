@icon ("res://addons/soft_pin/softPin Icon.svg")
@tool
extends Node

## Variables 
@export_enum("MeshInstance3D","Skeleton3D") var pin_type :
	set(value):
		pin_type = value
		notify_property_list_changed()
@export var softbody: SoftBody3D :
	set(value):
		softbody = value
		notify_property_list_changed()
@export var bone_attachment: BoneAttachment3D :
	set(value) :
		bone_attachment = value
		notify_property_list_changed()
@export_color_no_alpha var pinned_vertex_color
@export_tool_button("Pin vertices") var pinning = assign_pins


var mesh : Mesh
var vert_amount

var meshdatatool = MeshDataTool.new()
var arraymesh = ArrayMesh.new()
var arrays = []


func _validate_property(property):
	if property["name"] == "pinning" :
		if softbody == null :
			property.usage |= PROPERTY_USAGE_READ_ONLY
		elif pin_type == 1 and !bone_attachment :
			property.usage |= PROPERTY_USAGE_READ_ONLY
		else :
			property.usage |= PROPERTY_USAGE_EDITOR


func assign_pins():
	## Set the Mesh resource, Godot Collections Array, Array Mesh , and Mesh Data Tool
	mesh = softbody.mesh
	arrays = mesh.surface_get_arrays(0)
	arraymesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, arrays)
	meshdatatool.create_from_surface(arraymesh,0)
	print("SOFTPIN: Pinning...")
	
	vert_amount = meshdatatool.get_vertex_count()
	
	if (pin_type == 0):
		for i in range(vert_amount):
			var vertexcolor = meshdatatool.get_vertex_color(i)
			if (vertexcolor != pinned_vertex_color):
				## Color detected, don't pin.
				softbody.set_point_pinned(i,false)
			else:
				## No color detected, set pinned point.
				softbody.set_point_pinned(i,true) 
	if (pin_type == 1):
		var bonepath = softbody.get_path_to(bone_attachment)
		for i in range(vert_amount):
			var vertexcolor = meshdatatool.get_vertex_color(i)
			if (vertexcolor != pinned_vertex_color):
				## Color detected, don't pin.
				softbody.set_point_pinned(i,false)
			else:
				## No color detected, set pinned point. 
				softbody.set_point_pinned(i,true,bonepath) 
		if(softbody.skeleton != null):
			softbody.skeleton = ""
	
	print("SOFTPIN: Pinning completed without errors.")
	arraymesh.clear_surfaces()
	meshdatatool.clear()
	softbody = null
	bone_attachment = null
