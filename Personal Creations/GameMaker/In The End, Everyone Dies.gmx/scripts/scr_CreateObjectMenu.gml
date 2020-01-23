///scr_CreateObjectMenu(objects, number of objects, xpos, ypos)

var ObjectsToCreate = argument0;
var NumObjects = argument1;
var XPosition = argument2;
var YStart = argument3;
var ExtraSpace = 0;
var MoreSpace = 0;
TotalExtraSpace = 0;
TotalMoreSpace = 0;

for (var i = 0; i < NumObjects; i++)
{
    var GUIInstance = instance_create(0, 0, ObjectsToCreate[i]);
    if (ObjectsToCreate[i] == obj_BuildMenuBed) or (ObjectsToCreate[i] == obj_BuildMenuTable) or (ObjectsToCreate[i] == obj_BuildMenuGenerator) or (ObjectsToCreate[i] == obj_BuildMenuSniperNest)
    {
        ExtraSpace += 32;
        TotalExtraSpace += 32;
    }
    else if (ExtraSpace > 32)
    {
        MoreSpace = 64;
        TotalMoreSpace = 64;
    }
    else if (ExtraSpace > 0)
    {
        MoreSpace = 32;
        TotalMoreSpace = 32;
    }
    with (GUIInstance)
    {
        Width = 64;
        Height = 64;
        YPos = YStart - ExtraSpace - MoreSpace - (i * 32) - ((i + 1) * 75);
        Main = false;
        XPos = XPosition + 32;
    }
}

if (Text == 'DECORATIONS')
{
    TotalExtraSpace += 32;
}
