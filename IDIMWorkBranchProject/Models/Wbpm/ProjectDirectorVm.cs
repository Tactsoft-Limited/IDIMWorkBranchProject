using System.ComponentModel;
using System;
using System.Web;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ProjectDirectorVmValidator))]
    public class ProjectDirectorVm
    {

        public int ProjectDirectorId { get; set; }

        [DisplayName("প্রকল্পের নাম")]
        public int ADPProjectId { get; set; }

        [DisplayName("প্রকল্পের শিরোনাম")]
        public string ProjectTitle { get; set; }

        [DisplayName("প্রকল্প পরিচালক")]
        public string ProjectDirectorName { get; set; }

        [DisplayName("পদবী")]
        public string Designation { get; set; }

        [DisplayName("যোগদান তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }

        [DisplayName("বদলীর তারিখ")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransferDate { get; set; }

        [DisplayName("ডকুমেন্ট")]
        public string PDDocument { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        [DisplayName("আপলোড ডকুমেন্ট")]
        public HttpPostedFileBase PDDocumentFile { get; set; }


    }
}