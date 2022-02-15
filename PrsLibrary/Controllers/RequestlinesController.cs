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
            //reading request above and useing below statements to set the total
            request.Total = (from rl in _context.RequestLines //all request lines
                             join p in _context.Products // joining all together
                             on rl.ProductId equals p.Id //shows how they are put together
                             where rl.RequestId == requestId //only show where the request id matches id passed into method. 
                             select new { //what columns do we want to output? new + curly braces say some of the line items.
                                 LineTotal = rl.Quantity * p.Price // multiplying the quantity and price from lines chosen Linetotal is alias
                             }).Sum(x => x.LineTotal);//sum up all line totals this total is placed into request.total
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
            RecalculateRequestTotal(requestline.RequestId);
            return requestline;
        }
        public void Change(RequestLine requestline) {
            _context.SaveChanges();
            RecalculateRequestTotal(requestline.RequestId);
        }
        public void Remove(int id) {
            var requestline = _context.RequestLines.Find(id);
            if (requestline is null) {
                throw new Exception("Requestline not found.");
            }
            _context.RequestLines.Remove(requestline);
            _context.SaveChanges();
            RecalculateRequestTotal(requestline.RequestId);
        }

    }
}
