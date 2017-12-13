
namespace LinJas.Areas.AdminLinja.Common
{
    public class TVConstants
    {
      
        public class StoredProcedure
        {
            public class AdminTinh
            {
                public const string GetTinhByAll = @"admin_GetTinhByAll";
            }
            public class AdminQuanHuyen
            {
                public const string GetQuanHuyenByAll = @"admin_GetQuanHuyenByAll @TinhId={0}";
                public const string GetQuanHuyenById = @"admin_GetQuanHuyenById @Id={0}";
                public const string AddQuanHuyen = @"admin_AddQuanHuyen @TinhId={0},@Name={1},@SapXep={2}";
                public const string UpdateQuanHuyen = @"admin_UpdateQuanHuyen @Id={0},@TinhId={1},@Name={2},@SapXep={3}";
                public const string DeleteQuanHuyen = @"admin_DeleteQuanHuyen @Id={0}";
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
                public const string GetUpdateUserById = @"admin_GetUpdateUserById @Id={0}";
                public const string InsertUser = @"admin_InsertUser @Email={0}, @PasswordHash={1}, @Phone={2}, @UserName={3}, @Avatar={4}, @Active={5}, @RoleId={6}, @Hoten={7}";
                public const string UpdateUser = @"admin_UpdateUser @Id={0}, @Email={1}, @PasswordHash={2}, @Phone={3}, @UserName={4}, @Avatar={5}, @Active={6}, @RoleId={7}, @Hoten={8},@IsChangeImage={9}";
                public const string DeleteUserById = @"admin_DeleteUserById @Id={0}";
                //Quyền phân cho người dùng
                public const string GetQuyenByAll = @"admin_GetQuyenByAll";

            }
            public class AdminSanPham
            {
                public const string GetSanPhamByAll = @"admin_GetSanPhamByAll";
                public const string GetUpdateSanPhamById = @"admin_GetUpdateSanPhamById @Id={0}";
                public const string AddSanPham = @"admin_AddSanPham @Name={0},@TieuDe={1},@MoTa={2},@TuKhoa={3},@GiaCu={4},@GiaMoi={5},@ChietKhau={6},@KhuyenMaiId={7},@NgayBatDau={8},@NgayKetThuc={9},@DanhMucId={10},@SapSep={11},@TrangThai={12},@NoiDung={13},@Active={14},@HinhAnh={15}";
                public const string UpdateSanPham = @"admin_UpdateSanPham @Id={0},@Name={1},@TieuDe={2},@MoTa={3},@TuKhoa={4},@GiaCu={5},@GiaMoi={6},@ChietKhau={7},@KhuyenMaiId={8},@NgayBatDau={9},@NgayKetThuc={10},@DanhMucId={11},@SapSep={12},@TrangThai={13},@NoiDung={14},@Active={15},@HinhAnh={16}";
                public const string DeleteSanPham = @"admin_DeleteSanPham @Id={0}";
            }
            public class AdminDanhMuc
            {
                public const string GetdanhMucByAll = @"admin_GetdanhMucByAll";
                public const string GetdanhMucById = @"admin_GetdanhMucById @Id={0}";
                public const string addddanhMuc = @"admin_addddanhMuc @ParentId={0}, @Ma={1}, @Ten={2}, @SapSep={3}, @TinhId={4}";
                public const string UpdatedanhMuc = @"admin_UpdatedanhMuc @Id={0},@ParentId={1}, @Ma={2}, @Ten={3}, @SapSep={4}, @TinhId={5}";
                public const string DeleteDanhMuc = @"admin_DeleteDanhMuc @Id={0}";
            }
        }
    }
}