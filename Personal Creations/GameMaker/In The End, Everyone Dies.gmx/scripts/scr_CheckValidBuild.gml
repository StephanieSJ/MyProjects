///scr_CheckValidBuild
var Counter  = 0;
var Top = false;
var Left = false;
var Bottom = false;
var Right = false;
var Invalid = false;

if collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_PerimeterWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_WoodWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_BrickWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_Window, false, true)
{
    Counter++;
    Right = true;
}
if collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_PerimeterWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_WoodWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_BrickWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_Window, false, true)
{
    Counter++;
    Left = true;
}
if collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_PerimeterWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_WoodWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_BrickWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_Window, false, true)
{
    Counter++;
    Top = true;
}
if collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_PerimeterWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_WoodWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_BrickWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_Window, false, true)
{
    Counter++;
    Bottom = true;
}

if ((Left) and (Top)) or ((Left) and (Bottom)) or ((Right) and (Top)) or ((Right) and (Bottom))
{
    Invalid = true;
}

if (Counter > 2) or (Invalid)
{
    return false;
}
else
{
    return true;
}
