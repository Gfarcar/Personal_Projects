namespace authbackend.Entities;

public class MenuPermissionsTable{

    public string Id  =>  $"{RoleId}-{MenuItemId}";
    public int RoleId {get;set;}
    public int MenuItemId{get;set;}
    public bool CanRender{get;set;}

    public Role? Role { get; set; } // Foreign key to the Role table
    public MenuItem? MenuItem { get; set; } // Foreign key to the MenuItem table
}