using Microsoft.EntityFrameworkCore;
using PrsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Controllers {
    public class RequestlinesController {
        private readonly PrsDbContext _context;

        public RequestlinesController(PrsDbContext context) {
            this._context = context;
        }
        private void RecalculateRequestTotal(int requestId) {
            var request = _context.Requests.Find(requestId);

            request.Total = (from rl in _context.RequestLines
                             join p in _context.Products
                             on rl.ProductId equals p.Id
                             where rl.RequestId == requestId
                             select new {
                                 LineTotal = rl.Quantity * p.Price
                             }).Sum(x => x.LineTotal);
            _context.SaveChanges();
        }
        public IEnumerable<RequestLine> GetAll() {
            return _context.RequestLines
                    .Include(x => x.Product)
                    .Include(x => x.Request)
                    .ToList();
        }
        public RequestLine GetByPK(int id) {
            return _context.RequestLines
                .Include(x => x.Product)
                .Include(x => x.Request)
                .SingleOrDefault(x => x.Id == id);
        }
        public RequestLine Create(RequestLine requestline) {
            if (requestline is null) {
                throw new ArgumentNullException("Requestline");
            }
            if (requestline.Id != 0) {
                throw new ArgumentException("Requestline.Id must be zero.");
            }
            _context.RequestLines.Add(requestline);
            _context.SaveChanges();
            return requestline;
        }
        public void Change(RequestLine requestline) {
            _context.SaveChanges();
        }
        public void Remove(int id) {
            var requestline = _context.RequestLines.Find(id);
            if (requestline is null) {
                throw new Exception("Requestline not found.");
            }
            _context.RequestLines.Remove(requestline);
            _context.SaveChanges();
        }

    }
}
