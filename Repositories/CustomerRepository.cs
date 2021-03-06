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
    public class CustomerRepository : ICustomerRepository
    {
        // cache the customers in a thread-safe dictionary 
        // to improve performance
        private static ConcurrentDictionary<string, Customer> customersCache;

        private Northwind db;

        public CustomerRepository(Northwind db)
        {
            this.db = db;

            // pre-load customers from database as a normal 
            // Dictionary with CustomerID is the key,  
            // then convert to a thread-safe ConcurrentDictionary 
            if (customersCache == null)
            {
                customersCache = new ConcurrentDictionary<string, Customer>(
                db.Customers.ToDictionary(c => c.CustomerID));
            }
        }

        public async Task<Customer> CreateAsync(Customer c)
        {
            // normalize CustomerID into uppercase 
            c.CustomerID = c.CustomerID.ToUpper();

            // add to database using EF Core
            EntityEntry<Customer> added = await db.Customers.AddAsync(c);

            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                // if the customer is new, add it to cache, else 
                // call UpdateCache method 
                return customersCache.AddOrUpdate(c.CustomerID, c, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            // for performance, get from cache
            return await Task.Run<IEnumerable<Customer>>(
                () => customersCache.Values);
        }

        public async Task<Customer> RetrieveAsync(string id)
        {
            return await Task.Run(() =>
            {
            // for performance, get from cache
            id = id.ToUpper();
            Customer c2;
 
                c2 = db.Customers.First(customer => customer.CustomerID == id);
                /*var categories = db.Categories.Select(
                  c => new { c.CategoryID, c.CategoryName }).ToArray();
                var query = db.Products
                    .Where(product => product.UnitPrice < 10M)
                    .OrderByDescending(product => product.UnitPrice)
                    .Select(product => new
                    {
                        product.ProductID,
                        product.ProductName,
                        product.UnitPrice
                    });
                foreach (var b in query)
                {

                }*/
                // create two sequences that we want to join together 

                /*var products = db.Products.Select(
                  p => new { p.ProductID, p.ProductName, p.CategoryID }).ToArray();*/

                // join every product to its category to return 77 matches 
                /*var queryJoin = categories.Join(products,
                  category => category.CategoryID,
                  product => product.CategoryID,
                  (c, p) => new { c.CategoryName, p.ProductName, p.ProductID })
                  .OrderBy(cp => cp.ProductID);*/

                //var queryJoin = db.Products.Join()

                /*var queryJoin = db.Orders.Join(db.Customers,
                    o => o.CustomerID,
                    c => c.CustomerID,
                    (o,c) = > new { o.OrderID, c.Address });*/
                /*var queryJoin = from o in db.Orders
                                join c in db.Customers on o.CustomerID equals c.CustomerID
                                select new { OrderID = o.OrderID, Address = c.Address };
                //Customer[] arrayc = db.Customers.ToArray();
                foreach(var a in queryJoin)
                    {
                    
                    break;
                }*/
                //customersCache.TryGetValue(id, out c);
                return c2;
            });
        }

        private Customer UpdateCache(string id, Customer c)
        {
            Customer old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, c, old))
                {
                    return c;
                }
            }
            return null;
        }

        public async Task<Customer> UpdateAsync(string id, Customer c)
        {
            return await Task.Run(() =>
            {
                // normalize customer ID
                id = id.ToUpper();
                c.CustomerID = c.CustomerID.ToUpper();

                // update in database
                db.Customers.Update(c);
                int affected = db.SaveChanges();

                if (affected == 1)
                {
                    // update in cache
                    return Task.Run(() => UpdateCache(id, c));
                }
                return null;
            });
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await Task.Run(() =>
            {
                id = id.ToUpper();

                // remove from database
                Customer c = db.Customers.Find(id);
                db.Customers.Remove(c);
                int affected = db.SaveChanges();

                if (affected == 1)
                {
                    // remove from cache
                    return Task.Run(() => customersCache.TryRemove(id, out c));
                }
                else
                {
                    return null;
                }
            });
        }
    }
}