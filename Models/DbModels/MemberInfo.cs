using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class MemberInfo
    {
        //PK 具有唯一性與識別性的資料
        [Key]
        //PK 是引用 memberid 所以要加上，代表主鍵由自己輸入，不是由資料庫產生
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberID { get; set; }

        //大頭貼網址
        [StringLength(100)]
        public string HeadPhoto { get; set; }

        //綽號
        [StringLength(20)]
        public string NickName { get; set; }

        //工作
        [StringLength(20)]
        public string Job { get; set; }

        //狀態
        [StringLength(50)]
        public string State { get; set; }

        //自我介紹
        [StringLength(100)]
        public string Introduce { get; set; }

        public Member Member { get; set; }

    }
}
