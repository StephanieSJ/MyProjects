///scr_CheckHovering
if !(global.DragHover) and !(global.HealHover) and !(global.SelectionHover) and !(global.TimeHover) and !(global.ScrollBarHover) and !(global.WeaponHover) and !(global.BackHover)
{
    return true;
}
else
{
    return false;
}
