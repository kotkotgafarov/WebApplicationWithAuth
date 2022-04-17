using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using WebApplicationWithAuth.Models;
using WebApplicationWithAuth.Data;
using System.Linq.Expressions;
using WebApplicationWithAuth.Common;

namespace WebApplicationWithAuth
{
    public class EnrolleeRepository : IEnrolleeRepository
    {


        private LKADbContext db;

        public EnrolleeRepository(LKADbContext db)
        {
            this.db = db;
        }

        public async Task<Enrollee> CreateAsync(Enrollee c)
        {

            // add to database using EF Core
            EntityEntry<Enrollee> added = await db.Enrollees.AddAsync(c);

            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {

                return c;
            }
            else
            {
                return null;
            }
        }

        public async Task<PageResult<Enrollee>> RetrieveAllAsync(GetEnrolleesQueryObject request)
        {

            return await Task.Run<PageResult<Enrollee>>(() =>
            {
                var result = new PageResult<Enrollee>();
                IQueryable<Enrollee> iq;

                if (request.PageSize == 0)
                {
                    result.Count = db.Enrollees.Count();
                    result.Items = db.Enrollees.ToList();
                    return result;
                }
                else
                {
                    /*iq = db.Enrollees;
                    result.Items = iq.Skip(request.PageNumber * request.PageSize).Take(request.PageSize).ToList();*/

                    var leftOuterJoin = from enr in db.Enrollees
                                        join status in db.EnrolleesStatuses on enr.Id equals status.EnrolleeId into temp2
                                        from dstatus in temp2.DefaultIfEmpty()
                                        select new Enrollee()
                                        {
                                            Id = enr.Id,
                                            Address = enr.Address,
                                            Name = enr.Name,
                                            Phone = enr.Phone,
                                            ProfileStatus = dstatus.ProfileStatus,
                                            ApplicationStatus = dstatus.ApplicationStatus
                                        };
                    if (request.SortDirection == "asc" && request.OrderBy == "name") { leftOuterJoin = leftOuterJoin.OrderBy(p => p.Name); }
                    if (request.SortDirection == "desc" && request.OrderBy == "name") { leftOuterJoin = leftOuterJoin.OrderByDescending(p => p.Name); }
                    if (request.SortDirection == "asc" && request.OrderBy == "id") { leftOuterJoin = leftOuterJoin.OrderBy(p => p.Id); }
                    if (request.SortDirection == "desc" && request.OrderBy == "id") { leftOuterJoin = leftOuterJoin.OrderByDescending(p => p.Id); }

                    if (request.FilterName != "")
                    {
                        leftOuterJoin = leftOuterJoin.Where(p => EF.Functions.Like(p.Name, "%" + request.FilterName + "%"));
                    }

                    result.Count = leftOuterJoin.Count();
                    result.Items = leftOuterJoin.Skip(request.PageNumber * request.PageSize).Take(request.PageSize).ToList();
                    return result;
                }
                
            });
        }

       public async Task<Enrollee> RetrieveProfileAsync(string UserId)
        {
            return await Task.Run(() =>
            {
                Enrollee c = db.Enrollees.First(enrollee => enrollee.UserID == UserId);
                return c;
            });
        }

        public async Task<Enrollee> RetrieveAsync(int id)
        {
            return await Task.Run(() =>
            {
                Enrollee c = db.Enrollees.First(enrollee => enrollee.Id == id);
                return c;
            });
        }


        public async Task<Enrollee> UpdateAsync(int id, Enrollee c)
        {
            return await Task.Run(() =>
            {
                // update in database
                db.Enrollees.Update(c);
                int affected = db.SaveChanges();

                if (affected == 1)
                {
                    return Task.Run(() => c);
                }
                return null;
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() =>
            {
                // remove from database
                Enrollee c = db.Enrollees.Find(id);
                db.Enrollees.Remove(c);
                int affected = db.SaveChanges();

                if (affected == 1)
                {
                    // remove from cache
                    return Task.Run(() => true);
                }
                else
                {
                    return null;
                }
            });
        }
    }
}