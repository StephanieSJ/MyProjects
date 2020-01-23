///scr_DrawState(x, y, Survivor)
var XPos = argument0;
var YPos = argument1;
var SurvivorInstance = argument2;

switch (SurvivorInstance.State)
{
    case scr_GettingEaten:
    case scr_Fighting:
    {
        draw_text(XPos, YPos, 'FIGHTING');
        break;
    }
    case scr_Showering:
    {
        draw_text(XPos, YPos, 'SHOWERING');
        break;
    }
    case scr_Sleeping:
    {
        draw_text(XPos, YPos, 'SLEEPING');
        break;
    }
    case scr_Hungry:
    {
        draw_text(XPos, YPos, 'EATING');
        break;
    }
    case scr_Gardening:
    {
        draw_text(XPos, YPos, 'GARDENING');
        break;
    }
    case scr_Washing:
    {
        draw_text(XPos, YPos, 'WASHING');
        break;
    }
    case scr_Sniping:
    {
        draw_text(XPos, YPos, 'SNIPING');
        break;
    }
    case scr_Cooking:
    {
        draw_text(XPos, YPos, 'COOKING');
        break;
    }
    case scr_Doctoring:
    {
        draw_text(XPos, YPos, 'HEALING');
        break;
    }
    case scr_Cleaning:
    {
        draw_text(XPos, YPos, 'CLEANING');
        break;
    }
    case scr_Healing:
    {
        draw_text(XPos, YPos, 'GETTING HEALED');
        break;
    }
    case scr_SIdle:
    {
        draw_text(XPos, YPos, 'IDLING');
        break;
    }
    case scr_Scavenge:
    {
        draw_text(XPos, YPos, 'SCAVENGING');
        break;
    }
}
