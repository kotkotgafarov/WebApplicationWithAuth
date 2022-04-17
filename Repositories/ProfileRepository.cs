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
    public class ProfileRepository : IProfileRepository
    {


        private LKADbContext db;

        public ProfileRepository(LKADbContext db)
        {
            this.db = db;
        }

        public async Task<Profile> RetrieveProfileAsync(int id, string UserId)
        {
            return await Task.Run(() =>
            {
                Profile p = new Profile();
                Enrollee c;
                int count;
                if (id == 0)
                {
                    count = db.Enrollees.Where(enrollee => enrollee.UserID == UserId).Count();
                    if (count == 0)
                    {
                        c = new Enrollee { UserID = UserId };
                        db.Enrollees.Add(c);

                        int affected = db.SaveChanges();
                        if (affected == 0) { return null; }

                        EnrolleesStatus enrolleesstatus = new EnrolleesStatus { ApplicationStatus = 0, ProfileStatus = 0, EnrolleeId = c.Id };
                        db.EnrolleesStatuses.Add(enrolleesstatus);
                        affected = db.SaveChanges();
                        if (affected == 0) { return null; }
                    }
                    else
                    {
                        c = db.Enrollees.First(enrollee => enrollee.UserID == UserId);
                    }
                }
                else
                {
                    count = db.Enrollees.Where(enrollee => enrollee.Id == id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        c = db.Enrollees.First(enrollee => enrollee.Id == id);
                    }
                }

                p.enrollee = c;

                p.enrolleesstatus = db.EnrolleesStatuses.First(s => s.EnrolleeId == c.Id);
                p.relatives = db.Relatives.Where(r => r.EnrolleeID == c.Id).ToArray();
                p.doctypes = db.DocTypes.ToArray();
                p.educationlevels = db.EducationLevels.ToArray();

                var leftOuterJoin = from d in db.EnrollesDocs
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
                p.docs = leftOuterJoin.ToArray();

                int max_id = 0;
                // update the list of docs by adding docs, that can not be deleted
                if (p.docs.Length > 0)
                {
                    max_id = p.docs.Max(x => x.Id) + 1;
                }

                foreach (DocType d in p.doctypes)
                {
                    if (!d.CanBeDeleted)
                    {
                        EnrolleesDoc first_doc = Array.Find(p.docs, doc => doc.DocTypeId == d.Id);
                        if (first_doc == null)
                        {
                            first_doc = new EnrolleesDoc() { Type = d.Name, DocTypeId = d.Id, Changed = true, Id = max_id };
                            max_id++;
                            p.docs = p.docs.Append(first_doc).ToArray();
                        }
                    }
                }

                var leftOuterJoin2 = from d in db.EnrolleesEducations
                                     join level in db.EducationLevels on d.EducationLevelId equals level.Id into temp1
                                     from l in temp1.DefaultIfEmpty()
                                     join doc in db.EnrollesDocs on d.EnrolleesDocId equals doc.Id into temp2
                                     from dc in temp2.DefaultIfEmpty()
                                     where d.EnrolleeID == c.Id
                                     select new EnrolleesEducation()
                                     {
                                         Id = d.Id,
                                         EnrolleeID = d.EnrolleeID,
                                         NameOfSchool = d.NameOfSchool,
                                         Year = d.Year,
                                         EducationLevelName = l.Name,
                                         EducationLevelId = l.Id,
                                         EnrolleesDocId = dc.Id
                                     };
                p.education = leftOuterJoin2.ToArray();

                //это можно было сделать в leftOuterJoin2, но визуально код запроса выглядит сложно
                foreach (EnrolleesEducation edu in p.education)
                {
                    if (edu.EnrolleesDocId == 0) { continue; }
                    EnrolleesDoc doc = Array.Find(p.docs, _doc => _doc.Id == edu.EnrolleesDocId);
                    if (doc != null)
                    {
                        edu.EnrolleesDocName = doc.Type + " " + doc.Series + doc.Number;
                    }
                }
                return p;
            });
        }

        public async Task<Profile> UpdateAsync(int id, Profile p)
        {
            return await Task.Run(() =>
            {
                // update in database
                db.Enrollees.Update(p.enrollee);

                EnrolleesStatus appStatus = db.EnrolleesStatuses.First(s => s.EnrolleeId == id);
                if (appStatus == null)
                {
                    appStatus = new EnrolleesStatus() { EnrolleeId = id, ProfileStatus = 0, ApplicationStatus = 0 };
                    db.EnrolleesStatuses.Add(appStatus);
                }

                //db.EnrolleesEducations.RemoveRange(enrolleseducation_db);
                //db.EnrolleesEducations.AddRange(p.education);
                EnrolleesEducation[] enrolleseducation_db = db.EnrolleesEducations.Where(r => r.EnrolleeID == id).ToArray();
                foreach (EnrolleesEducation ee in p.education)
                {
                    EnrolleesEducation first_enrollees_educations = Array.Find(enrolleseducation_db, r => r.Id == ee.Id);
                    if (first_enrollees_educations == null)
                    {
                        ee.Id = 0;
                        db.EnrolleesEducations.Add(ee);
                    }
                    else
                    {
                        first_enrollees_educations.NameOfSchool = ee.NameOfSchool;
                        first_enrollees_educations.EducationLevelId = ee.EducationLevelId;
                        first_enrollees_educations.Year = ee.Year;
                        first_enrollees_educations.EnrolleesDocId = ee.EnrolleesDocId;
                        db.EnrolleesEducations.Update(first_enrollees_educations);
                    }
                }

                foreach (EnrolleesEducation r_db in enrolleseducation_db)
                {
                    EnrolleesEducation first_edu = Array.Find(p.education, r => r_db.Id == r.Id);
                    if (first_edu == null)
                    {
                        db.EnrolleesEducations.Remove(r_db);
                    }
                }


                Relative[] relatives_db = db.Relatives.Where(r => r.EnrolleeID == id).ToArray();


                foreach (Relative r in p.relatives)
                {
                    if (!r.Changed) continue;

                    Relative first_relative = Array.Find(relatives_db, r_db => r_db.Id == r.Id);
                    if (first_relative == null)
                    {
                        r.EnrolleeID = id;
                        r.Id = 0;
                        db.Relatives.Add(r);
                    }
                    else
                    {
                        first_relative.Name = r.Name;
                        first_relative.Phone = r.Phone;
                        first_relative.Type = r.Type;
                        first_relative.BirthDate = r.BirthDate;
                        db.Relatives.Update(first_relative);
                    }
                }

                foreach (Relative r_db in relatives_db)
                {
                    Relative first_relative = Array.Find(p.relatives, r => r_db.Id == r.Id);
                    if (first_relative == null)
                    {
                        db.Relatives.Remove(r_db);
                    }
                }

                EnrolleesDoc[] docs_db = db.EnrollesDocs.Where(d => d.EnrolleeID == id).ToArray();

                foreach (EnrolleesDoc d in p.docs)
                {
                    if (!d.Changed) continue;

                    EnrolleesDoc first_doc = Array.Find(docs_db, d_db => d_db.Id == d.Id);
                    if (first_doc == null)
                    {
                        d.EnrolleeID = id;
                        d.Id = 0;
                        db.EnrollesDocs.Add(d);
                    }
                    else
                    {
                        first_doc.Type = d.Type;
                        first_doc.IssueDate = d.IssueDate;
                        first_doc.IssuedBy = d.IssuedBy;
                        first_doc.Number = d.Number;
                        first_doc.Series = d.Series;
                        first_doc.Authority = d.Authority;
                        db.EnrollesDocs.Update(first_doc);
                    }
                }

                foreach (EnrolleesDoc d_db in docs_db)
                {
                    EnrolleesDoc first_doc = Array.Find(p.docs, d => d_db.Id == d.Id);
                    if (first_doc == null)
                    {
                        db.EnrollesDocs.Remove(d_db);
                    }
                }

                int affected = db.SaveChanges();

                if (affected > 0)
                {
                    return Task.Run(() => p);
                }
                return null;
            });
        }

    }
}