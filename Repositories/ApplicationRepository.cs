using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithAuth.Models;
using WebApplicationWithAuth.Data;

namespace WebApplicationWithAuth
{
    public class ApplicationRepository : IApplicationRepository
    {


        private LKADbContext db;

        public ApplicationRepository(LKADbContext db)
        {
            this.db = db;
        }

        public async Task<Application> RetrieveApplicationAsync(int id, string UserId)
        {
            return await Task.Run(() =>
            {
                Application a = new Application();
                Enrollee c;
                if (id == 0)
                {
                    c = db.Enrollees.First(enrollee => enrollee.UserID == UserId);
                }
                else
                {
                    c = db.Enrollees.First(enrollee => enrollee.Id == id);
                }

                if (c == null) { return null; } //first, an user has to fill "profile". Enrolles will be created in ProfileRepository

                a.enrollee = c;
                a.enrolleesstatus = db.EnrolleesStatuses.First(s => s.EnrolleeId == c.Id);

                var leftOuterJoin = from d in db.Competitions
                                    join _ec in db.EnrollesCompetitions on new { f1 = d.Id, f2 = c.Id } equals new { f1 = _ec.CompetitionId, f2 = _ec.EnrolleeID } into temp
                                    from ec in temp.DefaultIfEmpty()
                                    select new Competition()
                                    {
                                        Id = d.Id,
                                        Code = d.Code,
                                        Speciality = d.Speciality,
                                        Budget = d.Budget,
                                        Form = d.Form,
                                        Description = d.Description,
                                        Department = d.Department,
                                        Selected = (ec.Id > 0)
                                    };
                a.competitions = leftOuterJoin.ToArray();

                var leftOuterJoin2 = from ae in db.ApplicationsExams
                                     join subject in db.Subjects on ae.SubjectId equals subject.Id into temp1
                                     from s1 in temp1.DefaultIfEmpty()
                                     join subject in db.Subjects on ae.SubjectsSubstitutorId equals subject.Id into temp2
                                     from s2 in temp2.DefaultIfEmpty()
                                     join exam in db.CompetitionsExams on ae.CompetitionsExamId equals exam.Id into temp3
                                     from s3 in temp3.DefaultIfEmpty()
                                     where ae.EnrolleeId == c.Id
                                     select new ApplicationsExam()
                                     {
                                         Id = ae.Id,
                                         CompetitionsExamId = ae.CompetitionsExamId,
                                         EnrolleeId = ae.EnrolleeId,
                                         Type = ae.Type,
                                         SubjectId = ae.SubjectId,
                                         SubjectsSubstitutorId = ae.SubjectsSubstitutorId,
                                         Score = ae.Score,
                                         EnrolleesDocId = ae.EnrolleesDocId,
                                         SubjectName = s1.Name,
                                         SubjectsSubstitutorName = s2.Name,
                                         egeIsFeasible = s3.egeIsFeasible,
                                         examIsFeasible = s3.examIsFeasible,
                                         achievementIsFeasible = s3.achievementIsFeasible,
                                         MinScore = s3.MinScore,
                                         MaxScore = s3.MaxScore,
                                         Name = s3.Name
                                     };
                a.applicationsexam = leftOuterJoin2.ToArray();

                var leftOuterJoin3 = from ss in db.SubjectSubstitutors
                                     join subject in db.Subjects on ss.SubjectId equals subject.Id into temp1
                                     from s1 in temp1.DefaultIfEmpty()
                                     join subject in db.Subjects on ss.SubjectsSubstitutorId equals subject.Id into temp2
                                     from s2 in temp2.DefaultIfEmpty()
                                     select new SubjectSubstitutor()
                                     {
                                         SubjectId = ss.SubjectId,
                                         SubjectsSubstitutorId = ss.SubjectsSubstitutorId,
                                         SubjectName = s1.Name,
                                         SubjectsSubstitutorName = s2.Name
                                     };
                a.subjectsubstitutors = leftOuterJoin3.ToArray();

                var leftOuterJoin4 = from d in db.EnrollesDocs
                                     join doctype in db.DocTypes on d.DocTypeId equals doctype.Id into doctypes
                                     from dt in doctypes.DefaultIfEmpty()
                                     join file in db.EnrollesFiles on d.Id equals file.EnrolleesDocID into files
                                     from f in files.DefaultIfEmpty()
                                     where d.EnrolleeID == c.Id
                                     select new EnrolleesDoc()
                                     {
                                         Id = d.Id,
                                         EnrolleeID = d.EnrolleeID,
                                         Type = dt.Name,
                                         DocTypeId = dt.Id,
                                         CanBeDeleted = dt.CanBeDeleted,
                                         Series = d.Series,
                                         Number = d.Number,
                                         IssueDate = d.IssueDate,
                                         IssuedBy = d.IssuedBy,
                                         Authority = d.Authority,
                                         FileId = f.Id,
                                         FileName = f.UserName
                                     };
                a.docs = leftOuterJoin4.ToArray();

                //это можно было сделать в leftOuterJoin2, но визуально код запроса выглядит сложно
                foreach (ApplicationsExam exam in a.applicationsexam)
                {
                    if (exam.EnrolleesDocId == 0) { continue; }
                    EnrolleesDoc doc = Array.Find(a.docs, _doc => _doc.Id == exam.EnrolleesDocId);
                    if (doc != null)
                    {
                        exam.EnrolleesDocName = doc.Type + " " + doc.Series + doc.Number;
                    }
                }

                var leftOuterJoin5 = from ac in db.ApplicationsContracts
                                     join doc in db.EnrollesDocs on ac.EnrolleesDocId equals doc.Id into temp2
                                     from dc in temp2.DefaultIfEmpty()
                                     where ac.EnrolleeId == c.Id
                                     select new ApplicationsContract()
                                     {
                                         Id = ac.Id,
                                         EnrolleeId = ac.EnrolleeId,
                                         Client = ac.Client,
                                         EnrolleesDocId = dc.Id
                                     };
                a.applicationscontract = leftOuterJoin5.ToArray();

                //это можно было сделать в leftOuterJoin5, но визуально код запроса выглядит сложно
                foreach (ApplicationsContract ac in a.applicationscontract)
                {
                    if (ac.EnrolleesDocId == 0) { continue; }
                    EnrolleesDoc doc = Array.Find(a.docs, _doc => _doc.Id == ac.EnrolleesDocId);
                    if (doc != null)
                    {
                        ac.EnrolleesDocName = doc.Type + " " + doc.Series + doc.Number;
                    }
                }

                var leftOuterJoin6 = from aa in db.ApplicationsAchievements
                                     join doc in db.EnrollesDocs on aa.EnrolleesDocId equals doc.Id into temp2
                                     from dc in temp2.DefaultIfEmpty()
                                     where aa.EnrolleeId == c.Id
                                     select new ApplicationsAchievement()
                                     {
                                         Id = aa.Id,
                                         EnrolleeId = aa.EnrolleeId,
                                         Name = aa.Name,
                                         Year = aa.Year,
                                         EnrolleesDocId = dc.Id
                                     };
                a.applicationsachievement = leftOuterJoin6.ToArray();

                //это можно было сделать в leftOuterJoin6, но визуально код запроса выглядит сложно
                foreach (ApplicationsAchievement aa in a.applicationsachievement)
                {
                    if (aa.EnrolleesDocId == 0) { continue; }
                    EnrolleesDoc doc = Array.Find(a.docs, _doc => _doc.Id == aa.EnrolleesDocId);
                    if (doc != null)
                    {
                        aa.EnrolleesDocName = doc.Type + " " + doc.Series + doc.Number;
                    }
                }

                return a;
            });
        }

        public async Task<Application> UpdateAsync(int id, Application a)
        {
            return await Task.Run(() =>
            {
                EnrolleesStatus appStatus = db.EnrolleesStatuses.First(s => s.EnrolleeId == id);
                if (appStatus == null)
                {
                    appStatus = new EnrolleesStatus() { EnrolleeId = id, ProfileStatus = 0, ApplicationStatus = 0 };
                    db.EnrolleesStatuses.Add(appStatus);
                }

                // update in database
                db.Enrollees.Update(a.enrollee);

                EnrolleesCompetition[] _ec = db.EnrollesCompetitions.Where(r => r.EnrolleeID == id).ToArray();
                db.EnrollesCompetitions.RemoveRange(_ec);

                int[] competitionsSelected = new int[] { };

                foreach (Competition c in a.competitions)
                {
                    if (!c.Selected) { continue; }
                    EnrolleesCompetition ec = new EnrolleesCompetition() { EnrolleeID = id, CompetitionId = c.Id };
                    db.EnrollesCompetitions.Add(ec);

                    competitionsSelected = competitionsSelected.Append(c.Id).ToArray();
                }

                //ApplicationsExam[] _exams = db.ApplicationsExams.Where(r => r.EnrolleeId == id).ToArray();
                /*ApplicationsExam first_exam = Array.Find(_exams, r => r.Id == ae.Id);
                first_exam.Type = ae.Type;
                first_exam.Score = ae.Score;
                first_exam.SubjectsSubstitutorId = ae.SubjectsSubstitutorId;
                first_exam.EnrolleesDocId = ae.EnrolleesDocId;*/

                CompetitionsExam[] competitionsExams = db.CompetitionsExams.Where(r => competitionsSelected.Contains(r.CompetitionId)).ToArray();
                foreach (ApplicationsExam ae in a.applicationsexam)
                {
                    CompetitionsExam first_competition_exam = Array.Find(competitionsExams, r => r.SubjectId == ae.SubjectId);
                    if (first_competition_exam == null)
                    {
                        db.ApplicationsExams.Remove(ae);
                    }
                    else
                    {
                        db.ApplicationsExams.Update(ae);
                    }
                }

                //update contracts
                ApplicationsContract[] applicationscontract_db = db.ApplicationsContracts.Where(r => r.EnrolleeId == id).ToArray();
                foreach (ApplicationsContract ac in a.applicationscontract)
                {
                    ApplicationsContract first_applications_contracts = Array.Find(applicationscontract_db, r => r.Id == ac.Id);
                    if (first_applications_contracts == null)
                    {
                        ac.Id = 0;
                        db.ApplicationsContracts.Add(ac);
                    }
                    else
                    {
                        first_applications_contracts.Client = ac.Client;
                        first_applications_contracts.EnrolleesDocId = ac.EnrolleesDocId;
                        db.ApplicationsContracts.Update(first_applications_contracts);
                    }
                }

                foreach (ApplicationsContract r_db in applicationscontract_db)
                {
                    ApplicationsContract first_contract = Array.Find(a.applicationscontract, r => r_db.Id == r.Id);
                    if (first_contract == null)
                    {
                        db.ApplicationsContracts.Remove(r_db);
                    }
                }

                //update achievements
                ApplicationsAchievement[] applicationsachievement_db = db.ApplicationsAchievements.Where(r => r.EnrolleeId == id).ToArray();
                foreach (ApplicationsAchievement aa in a.applicationsachievement)
                {
                    ApplicationsAchievement first_applications_achievement = Array.Find(applicationsachievement_db, r => r.Id == aa.Id);
                    if (first_applications_achievement == null)
                    {
                        aa.Id = 0;
                        db.ApplicationsAchievements.Add(aa);
                    }
                    else
                    {
                        first_applications_achievement.Name = aa.Name;
                        first_applications_achievement.Year = aa.Year;
                        first_applications_achievement.EnrolleesDocId = aa.EnrolleesDocId;
                        db.ApplicationsAchievements.Update(first_applications_achievement);
                    }
                }

                foreach (ApplicationsAchievement r_db in applicationsachievement_db)
                {
                    ApplicationsAchievement first_achievement = Array.Find(a.applicationsachievement, r => r_db.Id == r.Id);
                    if (first_achievement == null)
                    {
                        db.ApplicationsAchievements.Remove(r_db);
                    }
                }

                //update aditional exams list according to competiotions selected
                int _type = 1;
                foreach (CompetitionsExam ce in competitionsExams)
                {
                    ApplicationsExam first_application_exam = Array.Find(a.applicationsexam, r => r.SubjectId == ce.SubjectId);
                    if (first_application_exam == null)
                    {
                        _type = 1;
                        if (!ce.egeIsFeasible && ce.examIsFeasible)
                        {
                            _type = 2;
                        }
                        else if (!ce.egeIsFeasible && ce.achievementIsFeasible)
                        {
                            _type = 3;
                        }

                        first_application_exam = new ApplicationsExam()
                        {
                            SubjectId = ce.SubjectId,
                            EnrolleeId = a.enrollee.Id,
                            CompetitionsExamId = ce.Id,
                            Type = _type,
                            SubjectsSubstitutorId = 0,
                            EnrolleesDocId = 0
                        };
                        db.ApplicationsExams.Add(first_application_exam);
                        a.applicationsexam = a.applicationsexam.Append(first_application_exam).ToArray();
                    }
                }
                //end of update aditional exams list according to competiotions selected


                int affected = db.SaveChanges();

                if (affected > 0)
                {
                    return Task.Run(() => a);
                }
                return null;
            });
        }

    }
}