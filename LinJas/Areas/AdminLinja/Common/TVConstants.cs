
namespace LinJas.Areas.AdminLinja.Common
{
    public class TVConstants
    {
      
        public class StoredProcedure
        {
            public class Admin
            {
                //
                public const string CarMakerSelect = @"Admin_CarMaker_Select";
                //
                public const string ModelSelectByHangId = @"Admin_Model_SelectByHang @CarMakerId={0}";
                public const string ModelSelectById = @"Admin_Model_SelectById @Id={0}";
                public const string ModelInsert = @"Admin_Model_Insert @CarMarkerId={0}, @Name={1}";
                public const string ModelUpdate = @"Admin_Model_Update @Id ={0}, @CarMarkerId={1}, @Name={2}";
                public const string ModelDelete = @"Admin_Model_Delete @Id={0}";
                
            }
            public class AdminRole
            {
                public const string GetAllRolesController = @"admin_GetAllRolesController";
                public const string AddController = @"admin_AddController @Controller={0},@Action={1},@Area={2},@Description={3},@IsDelete={4}";
                public const string CheckControllerandAction = @"admin_CheckControllerandAction  @Controller={0},@Action={1}";
                //quyền
                public const string GetAllRoles = @"admin_GetAllRoles";
                public const string GetRoleById = @"admin_GetRoleById @Id={0}";
                public const string CreateRoleId = @"admin_CreateRoleId @RoleName={0}";
                public const string UpdateRoles = @"admin_UpdateRoles @Id={0},@RoleName={1}";
                public const string DeleteRoleById = @"admin_DeleteRoleById @Id={0}";
            }
        }
    }
}