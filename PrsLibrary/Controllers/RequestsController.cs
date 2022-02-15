using Microsoft.EntityFrameworkCore;
using PrsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Controllers {
    public class RequestsController {

        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context) {
            this._context = context;      
        }
        public IEnumerable<Request> GetRequestsInReview(int userId) {
            var requests = _context.Requests
                .Where( x => x.Status == "Review"
                && x.UserId != userId)
                .ToList();
            return requests;
        }

        public void SetApproved(Request request) {
            request.Status = "Approved";
            Change(request);
        }
        public void SetRejected(Request request) {
            request.Status = "Rejected";
            Change(request);
        }

        public void SetReview(Request request) {
            if(request.Total <= 50) {
                request.Status = "Approved";
            }else {
                request.Status = "Review";
            }Change(request);
        }



        public IEnumerable<Request> GetAll() {
            return _context.Requests.Include(x => x.User).ToList();    
        }
        public Request GetByPk(int id) {
            return _context.Requests.Include(x => x.User)
                        .SingleOrDefault(x => x.Id == id);
                ;
        }
        public Request Create(Request request) {
            if (request is null) {
                throw new ArgumentNullException("Request");
            }
            if (request.Id != 0) {
                throw new ArgumentException("Request id must be Zero");
            }
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }
        public void Change(Request request) {
            _context.SaveChanges();
        }
        public void Remove(int id) {
            var request = _context.Requests.Find(id);
            if (request is null) {
                throw new Exception("Request not Found.");
            }
            _context.Requests.Remove(request);
            _context.SaveChanges();
        }
    }   
}
