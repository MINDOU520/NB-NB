//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Match
    {
        public int MID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string MName { get; set; }
        public string MAdder { get; set; }
        public Nullable<System.DateTime> MTime { get; set; }
        public Nullable<int> MNum { get; set; }
        public Nullable<System.DateTime> MAddtime { get; set; }
        public Nullable<int> EID { get; set; }
    
        public virtual Entry Entry { get; set; }
        public virtual Users Users { get; set; }
    }
}
