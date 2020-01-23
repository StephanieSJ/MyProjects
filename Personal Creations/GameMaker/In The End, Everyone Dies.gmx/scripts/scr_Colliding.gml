///scr_Colliding(size)
var Radius = argument0;

var PerimeterWallCollision = collision_circle(x, y, Radius, obj_PerimeterWall, false, true);
var IronGateCollision = collision_circle(x, y, Radius, obj_IronGate, false, true);
var PerimeterFenceCollision = collision_circle(x, y, Radius, obj_PerimeterFence, false, true);
var FenceGateCollision = collision_circle(x, y, Radius, obj_FenceGate, false, true);
var WoodWallCollision = collision_circle(x, y, Radius, obj_WoodWall, false, true);
var BrickWallCollision = collision_circle(x, y, Radius, obj_BrickWall, false, true);
var WoodDoorCollision = collision_circle(x, y, Radius, obj_WoodDoor, false, true);
var IronDoorCollision = collision_circle(x, y, Radius, obj_IronDoor, false, true);
var WindowCollision = collision_circle(x, y, Radius, obj_Window, false, true);

if (PerimeterWallCollision) or (IronGateCollision) or (PerimeterFenceCollision) or (FenceGateCollision) or (WoodWallCollision) or (BrickWallCollision) or (WoodDoorCollision) or (IronDoorCollision) or (WindowCollision)
{
    return true;
}
else
{
    return false;
}
