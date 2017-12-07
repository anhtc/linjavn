
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
                //Quyền
                public const string GetAllRolesController = @"admin_GetAllRolesController";
                public const string GetMotaQuyenByAction = @"admin_GetMotaQuyenByAction @Controller={0},@Action={1}";
                public const string UpdateMotaQuyen = @"admin_UpdateMotaQuyen @Controller={0},@Action={1},@Description={2},@Area={3}";

                public const string AddController = @"admin_AddController @Controller={0},@Action={1},@Area={2},@Description={3},@IsDelete={4}";
                public const string CheckControllerandAction = @"admin_CheckControllerandAction  @Controller={0},@Action={1}";
                //quyền
                public const string GetAllRoles = @"admin_GetAllRoles";
                public const string GetRoleById = @"admin_GetRoleById @Id={0}";
                public const string CreateRoleId = @"admin_CreateRoleId @RoleName={0}";
                public const string UpdateRoles = @"admin_UpdateRoles @Id={0},@RoleName={1}";
                public const string DeleteRoleById = @"admin_DeleteRoleById @Id={0}";
                //USER                
                public const string GetAllControllerByRole = @"admin_GetAllControllerByRole";
                public const string GetAllActionByController = @"admin_GetAllActionByController @userId={0}, @Controller={1}";
                public const string UpdatePhanquyenUserById = @"admin_UpdatePhanquyenUserById @RoleId={0}, @Controller={1}, @Action={2}, @Description={3}";
                public const string DeletePhanquyenUserById = @"admin_DeletePhanquyenUserById @RoleId={0}, @Controller={1}, @Action={2}, @Description={3}";
                //Người dùng
                public const string GetAllUser = @"admin_GetAllUser";
                public const string InsertUser = @"admin_InsertUser @Email={0}, @PasswordHash={1}, @Phone={2}, @UserName={3}, @Avatar={4}, @Active={5}, @RoleId={6}, @Hoten={7}";
                public const string UpdateUser = @"admin_UpdateUser @Id={0}, @Email={1}, @PasswordHash={2}, @Phone={3}, @UserName={4}, @Avatar={5}, @Active={6}, @RoleId={7}, @Hoten={8}";
                public const string DeleteUserById = @"admin_DeleteUserById @Id={0}";
            }
        }
    }
}