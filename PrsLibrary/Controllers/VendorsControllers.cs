using PrsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Controllers {
    class VendorsControllers {
        private readonly PrsDbContext _context;

        public VendorsControllers(PrsDbContext context) {
            this._context = context;
        }
        public IEnumerable<Vendor> GetAll() {
            return _context.Vendors.ToList();
                    }

        public Vendor GetByPk(int id) {
            return _context.Vendors.Find(id);
        }
        public Vendor Create(Vendor vendor) {
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
            return vendor;
        }//Update aka change. 
        public void Change(Vendor vendor) {
            _context.SaveChanges();
        }
        public void Remove(int id) {
            var vendor = _context.Vendors.Find(id);
            if (vendor is null) {
                throw new Exception("User not found.");
            }
            _context.Vendors.Remove(vendor);
            _context.SaveChanges();
        }
    }
}
