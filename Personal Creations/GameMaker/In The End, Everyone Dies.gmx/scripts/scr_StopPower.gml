///scr_StopPower
for (var i = 0; i < obj_Electronics.NumberElectronics; i++)
{
    with (obj_Electronics.Electronics[i])
    {
        if (Name != 'Generator')
        {
            Times = '000000000000000000000000';
            Active = false;
        }
    }
}

for (var i = 0; i < global.Survivors; i++)
{
    with (global.SurvivorInstances[i])
    {
        if (Assignment == 'obj_Cooker') or (Assignment == 'obj_WashingMachine')
        {
            Assignment = 'Nothing';
            AssignedObject = noone;
        }
        if (State == scr_Cooking) or (State == scr_Washing)
        {
            State = scr_SIdle;
        }
    }
}
