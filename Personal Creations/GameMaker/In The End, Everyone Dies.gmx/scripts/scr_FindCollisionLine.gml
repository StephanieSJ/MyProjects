///scr_FindCollisionLine(X1, Y1, X2, Y2)
var XStart = argument0;
var YStart = argument1;
var XEnd = argument2;
var YEnd = argument3;

var PerimeterWallCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_PerimeterWall, false, true);
var IronGateCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_IronGate, false, true);
var PerimeterFenceCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_PerimeterFence, false, true);
var FenceGateCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_FenceGate, false, true);
var WoodWallCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_WoodWall, false, true);
var BrickWallCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_BrickWall, false, true);
var WoodDoorCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_WoodDoor, false, true);
var IronDoorCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_IronDoor, false, true);
var WindowCollision = collision_line(XStart, YStart, XEnd, YEnd, obj_Window, false, true);
    
if (PerimeterWallCollision) or (IronGateCollision) or (PerimeterFenceCollision) or (FenceGateCollision) or (WoodWallCollision) or (BrickWallCollision) or (WoodDoorCollision) or (IronDoorCollision) or (WindowCollision)
{
    return false;
}
else
{
    return true;
}
