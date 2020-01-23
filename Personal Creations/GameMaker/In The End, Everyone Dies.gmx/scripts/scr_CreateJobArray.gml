///scr_CreateJobArray(Job Name)
var JobName = argument0;
var JobInstances = 0;
var TotalInstances = 0;

switch (JobName)
{
    case 'COOKING':
    {
        for (var i = 0; i < instance_number(obj_Cooker); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_Cooker, i);
            TotalInstances++;
        }
        break;
    }
    
    case 'WASHING':
    {
        for (var i = 0; i < instance_number(obj_WashingMachine); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_WashingMachine, i);
            TotalInstances++;
        }
        break;
    }
    
    case 'SNIPING':
    {
        for (var i = 0; i < instance_number(obj_SniperNest); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_SniperNest, i);
            TotalInstances++;
        }
        break;
    }
    
    case 'CLEANING':
    {
        for (var i = 0; i < instance_number(obj_CleaningStation); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_CleaningStation, i);
            TotalInstances++;
        }
        break;
    }
    
    case 'GARDENING':
    {
        for (var i = 0; i < ceil(instance_number(obj_Garden) / 10); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_Garden, i);
            TotalInstances++;
        }
        break;
    }
    
    case 'HEALING':
    {
        for (var i = 0; i < instance_number(obj_HealingStation); i++)
        {
            JobInstances[TotalInstances] = instance_find(obj_HealingStation, i);
            TotalInstances++;
        }
        break;
    }
}

return JobInstances;
