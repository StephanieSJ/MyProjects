///scr_GetBlocker
var PerimeterWallCollision = collision_circle(x, y, 6, obj_PerimeterWall, false, true);
var IronGateCollision = collision_circle(x, y, 6, obj_IronGate, false, true);
var PerimeterFenceCollision = collision_circle(x, y, 6, obj_PerimeterFence, false, true);
var FenceGateCollision = collision_circle(x, y, 6, obj_FenceGate, false, true);
var WoodWallCollision = collision_circle(x, y, 6, obj_WoodWall, false, true);
var BrickWallCollision = collision_circle(x, y, 6, obj_BrickWall, false, true);
var WoodDoorCollision = collision_circle(x, y, 6, obj_WoodDoor, false, true);
var IronDoorCollision = collision_circle(x, y, 6, obj_IronDoor, false, true);
var WindowCollision = collision_circle(x, y, 6, obj_Window, false, true);

if (PerimeterWallCollision)
{
    return obj_PerimeterWall;
}
else if (IronGateCollision)
{
    return obj_IronGate;
}
else if (PerimeterFenceCollision)
{
    return obj_PerimeterFence;
}
else if (FenceGateCollision)
{
    return obj_FenceGate;
}
else if (WoodWallCollision)
{
    return obj_WoodWall;
}
else if (BrickWallCollision)
{
    return obj_BrickWall;
}
else if (WoodDoorCollision)
{
    return obj_WoodDoor;
}
else if (IronDoorCollision)
{
    return obj_IronDoor;
}
else if (WindowCollision)
{
    return obj_Window;
}
else
{
    return noone;
}
