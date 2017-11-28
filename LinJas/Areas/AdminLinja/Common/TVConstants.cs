
namespace LinJas.Manager.Common
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
        }
    }
}