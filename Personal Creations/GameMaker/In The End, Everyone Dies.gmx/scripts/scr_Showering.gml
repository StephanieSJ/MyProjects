///scr_Showering
if !(Distracted)
{
    if (place_meeting(x, y, Shower))
    {
        Counter++;
        if (Counter == 375)
        {
            Counter = 0;
            Cleanliness += 50;
            if (Cleanliness >= 100)
            {
                Cleanliness = 100;
                Shower.Used = false;
                Shower = noone;
                scr_GoBackToAssignedState();
                exit;
            }
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
        if (Shower == noone)
        {
            Shower = scr_FindShower();
        }
        if (Shower == noone)
        {
            scr_GoBackToAssignedState();
        }
        else
        {
            with (Shower)
            {
                Used = true;
            }
            StartX = Shower.x;
            StartY = Shower.y;
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
