///scr_GettingEaten
if (PathCreated)
{
    path_end();
    path_delete(Path);
    StopMoving = true;
    PathCreated = false;
}

Counter++;
Health--;

if (Counter >= 49)
{
    Counter = 0;
    with (instance_nearest(x, y, obj_Enemy))
    {
        alarm[1] = 30;
        Stunned = true;
    }
    State = scr_Fighting;
}
