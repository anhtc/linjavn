
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
                public const string AddSanPham = @"admin_AddSanPham @Name={0},@TieuDe={1},@MoTa={2},@TuKhoa={3},@GiaCu={4},@GiaMoi={5},@ChietKhau={6},@KhuyenMaiId={7},@NgayBatDau={8},@NgayKetThuc={9},@DanhMucId={10},@SapSep={11},@TrangThai={12},@NoiDung={13},@Active={14},@HinhAnh={15},@TuKhoaTimKiem={16}";
                public const string UpdateSanPham = @"admin_UpdateSanPham @Id={0},@Name={1},@TieuDe={2},@MoTa={3},@TuKhoa={4},@GiaCu={5},@GiaMoi={6},@ChietKhau={7},@KhuyenMaiId={8},@NgayBatDau={9},@NgayKetThuc={10},@DanhMucId={11},@SapSep={12},@TrangThai={13},@NoiDung={14},@Active={15},@HinhAnh={16},@IsChangeImage={17},@TuKhoaTimKiem={18}";
                public const string DeleteSanPham = @"admin_DeleteSanPham @Id={0}";
            }
            public class AdminDanhMuc
            {
                public const string GetdanhMucBySelect = @"admin_GetdanhMucBySelect @TinhId={0}, @ParentId={1}";
                public const string GetdanhMucByAll = @"admin_GetdanhMucByAll @TinhId={0},@Id={1}";
                public const string GetdanhMucById = @"admin_GetdanhMucById @Id={0}";
                public const string addddanhMuc = @"admin_addddanhMuc @ParentId={0}, @Ma={1}, @Ten={2}, @SapSep={3}, @TinhId={4}";
                public const string UpdatedanhMuc = @"admin_UpdatedanhMuc @Id={0},@ParentId={1}, @Ma={2}, @Ten={3}, @SapSep={4}, @TinhId={5}";
                public const string DeleteDanhMuc = @"admin_DeleteDanhMuc @Id={0}";
            }
            public class AdminAnhSanPham
            {
                public const string GetAnhSanPhamId = @"admin_GetAnhSanPhamId @SanPhamId ={0}";
                public const string GetAnhSanPhamById = @"admin_GetAnhSanPhamById @Id ={0}";
                public const string AddAnhSanPham = @"admin_AddAnhSanPham @Name={0},@SanPhamId={1},@HinhAnh={2} ,@SapXep={3}";
                public const string UpdateAnhSanPham = @"admin_UpdateAnhSanPham @Id={0}, @Name={1},@SanPhamId={2},@HinhAnh={3} ,@SapXep={4},@IsChangeImage={5}";
                public const string DeleteAnhSanPham = @"admin_DeleteAnhSanPham @Id={0}";
            }
            public class AdminBlog
            {
                public const string GetBlogByAll = @"admin_GetBlogByAll";
                public const string GetBlogById = @"admin_GetBlogById @id={0}";
                public const string AddBlog = @"admin_AddBlog @DanhMucId={0} ,@Name={1} ,@NoiDung={2} ,@TieuDe={3} ,@MoTa={4} ,@TuKhoa={5} ,@SapXep={6} ,@NgayDang={7} ,@HinhAnh={8} ,@Active={9}";
                public const string UpdateBlog = @"admin_UpdateBlog  @id={0},@DanhMucId={1} ,@Name={2} ,@NoiDung={3} ,@TieuDe={4} ,@MoTa={5} ,@TuKhoa={6} ,@SapXep={7} ,@NgayDang={8} ,@HinhAnh={9} ,@Active={10}";
                public const string DeleteBlog = @"admin_DeleteBlog @id={0}";
                public const string BlogTags_CapNhat= @"aadmin_BlogTags_CapNhat @BlogId ={0}, @TagIds={1}, @UnTagIds={2},@SortOrder={3}";
            }
            public class AdminTag
            {
                public const string TagBlogSelectAll = @"[admin_Tag_select] @Search={0}, @Loai={1}";
                public const string TagBlogSelectById = @"[admin_Tag_select_ById] @Id={0}";
                public const string TagBlogInsert = @"[admin_Tag_Insert] @Name={0}, @SortOrder={1}, @Loai={2}";
                public const string TagBlogDelete = @"[admin_Tag_Delete] @Id={0}";
                public const string TagBlogUpdate = @"[admin_Tag_Update] @Id={0}, @Name={1}, @SortOrder={2}, @Loai={3}";//
                public const string BlogTags_SelectByBlogId = @"admin_BlogTags_SelectByBlogId @Search={0}, @BlogId={1}";

            }
        }
    }
}