﻿using AutoMapper;
using Prayug.Module.Core.Models;
using Prayug.Module.Core.ViewModels.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Module.Core.MappingProfile
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<tbl_course_vm, CourseVm>();
            CreateMap<tbl_group_vm, GroupVm>();
            CreateMap<tbl_subject_vm, SubjectVm>();
            CreateMap<tbl_user_semester, UserSemesterVm>();
            CreateMap<tbl_subject_overview, SubjectOverviewVm>();
            CreateMap<tbl_subject_curriculum, SubjectCurriculumVm>();
            CreateMap<tbl_unit_lession, UnitLessionVm>();
            CreateMap<tbl_lession_item, LessionItemsVm>();
            CreateMap<tbl_unit_lession, QuizDetailVm>();
            CreateMap<tbl_question_mcq, QuestionVm>();
            CreateMap<tbl_answer_option, OptionVm>();
            CreateMap<fnd_user, UserVm>();
            CreateMap<tbl_course_vm, CourseListVm>();
            CreateMap<import_course, ImportCourseList>();
            CreateMap<import_response, ImportResponseVm>();
            CreateMap<tbl_subject_vm, SubjectListVm>();
            CreateMap<tbl_unit_lession, LessionVm>();
            CreateMap<tbl_category_vm, CategoryVm>();
            CreateMap<lession_item_list, LessionItemListVm>();
            CreateMap<tbl_lession_item, LessionItemListVm>();
        }
    }
}
