using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.DataLayer;

namespace WebApiForStudent.App_Data.Address.Concrete
{
    public class AddressService : IAddress
    {
        private readonly AppDbContext _db;

        public AddressService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var address = await _db.Addresses.FindAsync(id);
                _db.Remove(address);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> Get(Guid id)
        {
            var address = await _db.Addresses.FindAsync(id);
            string Jsonaddress = JsonConvert.SerializeObject
                (
                address, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
                );
            return Jsonaddress;
        }

        public async Task<string> GetAll()
        {
            var addresses = await _db.Addresses.ToListAsync();
            string Jsonaddresses = JsonConvert.SerializeObject
                (
                addresses, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Jsonaddresses;
        }

        public async Task<bool> Post(WebApiForStudent.Models.Address address)
        {
            try
            {
                address.Id = Guid.NewGuid();
                _db.Add(address);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Put(Guid id, WebApiForStudent.Models.Address address)
        {
            try
            {
                var res = await _db.Addresses.FirstOrDefaultAsync(x=>x.Id==id);
                res.Address_1 = address.Address_1;
                res.Address_2 = address.Address_2;
                res.City = address.City;
                res.State = address.State;
                _db.Entry(address).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
