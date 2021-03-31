$("#Delete").click(function ()
{
    if (confirm("Are you sure you want to delete this?"))
    {
        return true;
    }
    else
    {
        return false;
    }
});