///scr_GoBackToAssignedState
switch (Assignment)
{
    case 'GARDEN':
    {
        State = scr_Gardening;
        break;
    }
    
    case 'HEALING STATION':
    {
        State = scr_Doctoring;
        break;
    }
    
    case 'CLEANING STATION':
    {
        State = scr_Cleaning;
        break;
    }
    
    case 'SNIPER NEST':
    {
        State = scr_Sniping;
        break;
    }
    
    case 'COOKER':
    {
        State  = scr_Cooking;
        break;
    }
    
    case 'WASHING MACHINE':
    {
        State = scr_Washing;
        break;
    }
    
    default:
    {
        State = scr_SIdle;
        break;
    }
}
