//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HASAKI_ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEU_XUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEU_XUAT()
        {
            this.PHIEU_XUAT_CT = new HashSet<PHIEU_XUAT_CT>();
        }
    
        public string MaXuat { get; set; }
        public System.DateTime NgayXuat { get; set; }
        public decimal TongGiaTriXuat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEU_XUAT_CT> PHIEU_XUAT_CT { get; set; }
    }
}
