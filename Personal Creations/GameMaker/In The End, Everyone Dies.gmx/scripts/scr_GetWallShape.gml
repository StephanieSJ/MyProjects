///scr_GetWallShape(object name)
var NameObject = argument0;
if (NameObject == 'WoodWall') or (NameObject == 'BrickWall') or (NameObject == 'PerimeterWall')
{
    var RightCollide = collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_WoodWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_BrickWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_Window, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_PerimeterWall, false, true);
    var LeftCollide = collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_WoodWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_BrickWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_Window, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_PerimeterWall, false, true);
    var TopCollide = collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_WoodWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_BrickWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_Window, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_PerimeterWall, false, true);
    var BottomCollide = collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_WoodWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_BrickWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_Window, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_PerimeterWall, false, true);

    if (RightCollide) and (LeftCollide) and (TopCollide) and (BottomCollide)
    {
        return 10;
    }
    else if (RightCollide) and (LeftCollide) and (TopCollide)
    {
        return 9;
    }
    else if ((RightCollide) or (LeftCollide)) and (TopCollide) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 8;
    }
    else if (RightCollide) and (LeftCollide) and (BottomCollide)
    {
        return 7;
    }
    else if (RightCollide) and (LeftCollide)
    {
        return 6;
    }
    else if (TopCollide) and (BottomCollide)
    {
        return 5;
    }
    else if ((LeftCollide) or (RightCollide)) and (TopCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 4;
    }
    else if ((LeftCollide) or (RightCollide)) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 3;
    }
    else if (BottomCollide)
    {
        return 2;
    }
    else if (LeftCollide) or (RightCollide)
    {
        if (LeftCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 1;
    }
    else if (TopCollide)
    {
        return 0;
    }
    else
    {
        return 11;
    }
}
else if (NameObject == 'Garden')
{
    var RightCollide = collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_Garden, false, true);
    var LeftCollide = collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_Garden, false, true);
    var TopCollide = collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_Garden, false, true);
    var BottomCollide = collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_Garden, false, true);

    if (RightCollide) and (LeftCollide) and (TopCollide) and (BottomCollide)
    {
        return 10;
    }
    else if (RightCollide) and (LeftCollide) and (TopCollide)
    {
        return 9;
    }
    else if ((RightCollide) or (LeftCollide)) and (TopCollide) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 8;
    }
    else if (RightCollide) and (LeftCollide) and (BottomCollide)
    {
        return 7;
    }
    else if (RightCollide) and (LeftCollide)
    {
        return 6;
    }
    else if (TopCollide) and (BottomCollide)
    {
        return 5;
    }
    else if ((LeftCollide) or (RightCollide)) and (TopCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 4;
    }
    else if ((LeftCollide) or (RightCollide)) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 3;
    }
    else if (BottomCollide)
    {
        return 2;
    }
    else if (LeftCollide) or (RightCollide)
    {
        if (LeftCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 1;
    }
    else if (TopCollide)
    {
        return 0;
    }
    else
    {
        return 11;
    }
}
else if (NameObject == 'PerimeterFence')
{
    var RightCollide = collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_PerimeterFence, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_FenceGate, false, true);
    var LeftCollide = collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_PerimeterFence, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_FenceGate, false, true);
    var TopCollide = collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_PerimeterFence, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_FenceGate, false, true);
    var BottomCollide = collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_PerimeterFence, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_FenceGate, false, true);
    
    if (RightCollide) and (LeftCollide) and (TopCollide) and (BottomCollide)
    {
        return 10;
    }
    else if (RightCollide) and (LeftCollide) and (TopCollide)
    {
        return 9;
    }
    else if ((RightCollide) or (LeftCollide)) and (TopCollide) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 8;
    }
    else if (RightCollide) and (LeftCollide) and (BottomCollide)
    {
        return 7;
    }
    else if (RightCollide) and (LeftCollide)
    {
        return 6;
    }
    else if (TopCollide) and (BottomCollide)
    {
        return 5;
    }
    else if ((LeftCollide) or (RightCollide)) and (TopCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 4;
    }
    else if ((LeftCollide) or (RightCollide)) and (BottomCollide)
    {
        if (RightCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 3;
    }
    else if (BottomCollide)
    {
        return 2;
    }
    else if (LeftCollide) or (RightCollide)
    {
        if (LeftCollide)
        {
            XScale = -1;
        }
        else
        {
            XScale = 1;
        }
        return 1;
    }
    else if (TopCollide)
    {
        return 0;
    }
    else
    {
        return 11;
    }
}
else if (NameObject == 'IronDoor') or (NameObject == 'WoodDoor') or (NameObject == 'Window') or (NameObject == 'IronGate')
{
    var RightCollide = collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_WoodWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_BrickWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_Window, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_PerimeterWall, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_IronGate, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_IronDoor, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_WoodDoor, false, true);
    var LeftCollide = collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_WoodWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_BrickWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_Window, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_PerimeterWall, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_IronGate, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_IronDoor, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_WoodDoor, false, true);
    var TopCollide = collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_WoodWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_BrickWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_Window, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_PerimeterWall, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_IronGate, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_IronDoor, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_WoodDoor, false, true);
    var BottomCollide = collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_WoodWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_BrickWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_Window, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_PerimeterWall, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_IronGate, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_IronDoor, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_WoodDoor, false, true);

    if (RightCollide) or (LeftCollide)
    {
        return 1;
    }
    else if (TopCollide) or (BottomCollide)
    {
        return 0;
    }
}
else if (NameObject == 'FenceGate')
{
    var RightCollide = collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_PerimeterFence, false, true) or collision_rectangle(x + 9, y - 7, x + 23, y + 7, obj_FenceGate, false, true);
    var LeftCollide = collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_PerimeterFence, false, true) or collision_rectangle(x - 23, y - 7, x - 9, y + 7, obj_FenceGate, false, true);
    var TopCollide = collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_PerimeterFence, false, true) or collision_rectangle(x - 7, y - 23, x + 7, y - 9, obj_FenceGate, false, true);
    var BottomCollide = collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_PerimeterFence, false, true) or collision_rectangle(x - 7, y + 9, x + 7, y + 23, obj_FenceGate, false, true);
    
    if (RightCollide) or (LeftCollide)
    {
        return 1;
    }
    else if (TopCollide) or (BottomCollide)
    {
        return 0;
    }
}
return 0;
