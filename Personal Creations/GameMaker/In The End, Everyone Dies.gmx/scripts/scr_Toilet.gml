///scr_Toilet
if !(Distracted)
{
    if (place_meeting(x, y, Toilet))
    {
        Counter++;
        if (Counter == 375)
        {
            Counter = 0;
            Bathroom = 0;
            Toilet.Used = false;
            Toilet = noone;
            scr_GoBackToAssignedState();
            exit;
        }
    }
}

if (global.UpdateSurvivorGrid)
{
    PathCreated = false;
    StopMoving = true;
    if (path_exists(Path))
    {
        path_end();
        path_delete(Path);
    }
    alarm[1] = 2;
}

if (PathCreated)
{
    if (path_position == 1)
    {
        PathCreated = false;
        StopMoving = true;
        alarm[1] = (irandom(4) + 1) * 60;
        path_delete(Path);
    }
}
else if !(StopMoving)
{
    Path = path_add();
    while !(PathCreated)
    {
        if (Toilet == noone)
        {
            Shower = scr_FindToilet();
        }
        if (Toilet == noone)
        {
            scr_GoBackToAssignedState();
        }
        else
        {
            with (Toilet)
            {
                Used = true;
            }
            StartX = Toilet.x;
            StartY = Toilet.y;
        }
        var RandomX = 0;
        var RandomY = 0;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX + RandomX, StartY + RandomY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}
