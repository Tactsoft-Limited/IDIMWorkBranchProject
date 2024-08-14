using BGB.Data.Entities.Inv;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Item")]
    public partial class WorkBranchStoreItem
    {
        public WorkBranchStoreItem()
        {
            ComplainDetails = new HashSet<ComplainDetail>();
            ComplainIssueDetails = new HashSet<ComplainIssueDetail>();
            ComplainReceives = new HashSet<ComplainReceive>();
            DirectReceivedDetail1 = new HashSet<WorkBranchStoreDirectReceivedDetail>();
            ExpenseDetail2 = new HashSet<WorkBranchStoreExpenseDetail>();
            IssueDetail2 = new HashSet<WorkBranchStoreIssueDetail>();
            PurchaseOrderDetail4 = new HashSet<WorkBranchStorePurchaseOrderDetail>();
            PurchaseOrderReceivedDetail1 = new HashSet<WorkBranchStorePurchaseOrderReceivedDetail>();
            RequisitionDetail1 = new HashSet<WorkBranchStoreRequisitionDetail>();
        }

        [Key]
        public int ItemId { get; set; }

        public int ProductCategoryId { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductBrandId { get; set; }

        public int ProductUnitId { get; set; }

        public int? ProductGroupId { get; set; }

        public int? ProductWarrantyId { get; set; }

        [StringLength(50)]
        public string ItemBarCode { get; set; }

        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(3)]
        public string ItemStatus { get; set; }

        [StringLength(20)]
        public string ItemType { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual ProductUnit ProductUnit { get; set; }

        public virtual ProductWarranty ProductWarranty { get; set; }

        public virtual ICollection<ComplainDetail> ComplainDetails { get; set; }

        public virtual ICollection<ComplainIssueDetail> ComplainIssueDetails { get; set; }

        public virtual ICollection<ComplainReceive> ComplainReceives { get; set; }

        public virtual ICollection<WorkBranchStoreDirectReceivedDetail> DirectReceivedDetail1 { get; set; }

        public virtual ICollection<WorkBranchStoreExpenseDetail> ExpenseDetail2 { get; set; }

        public virtual ICollection<WorkBranchStoreIssueDetail> IssueDetail2 { get; set; }

        //public virtual WorkBranchStoreItem Item21 { get; set; }

        //public virtual WorkBranchStoreItem Item22 { get; set; }

        public virtual ICollection<WorkBranchStorePurchaseOrderDetail> PurchaseOrderDetail4 { get; set; }

        public virtual ICollection<WorkBranchStorePurchaseOrderReceivedDetail> PurchaseOrderReceivedDetail1
        {
            get;
            set;
        }

        public virtual ICollection<WorkBranchStoreRequisitionDetail> RequisitionDetail1 { get; set; }
    }
}
