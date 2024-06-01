using Microsoft.AspNetCore.Mvc;
using Shop_cake.Data;
using Shop_cake.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_cake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly List<Feedback> feedbacks = new List<Feedback>();
        private readonly AppDBContext _db;

        public FeedbackController(AppDBContext db)
        {
            _db = db;
        }

        // GET 
        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> Get()
        {
            return _db.Feedbacks.ToArray();
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult<Feedback> Get(int id)
        {
            var feedback = _db.Feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }
            return feedback;
        }

        // POST 
        [HttpPost]
        public ActionResult<Feedback> Post(Feedback feedback)
        {
            feedback.Id = _db.Feedbacks.Count() + 1; // Генерация нового Id
            _db.Feedbacks.Add(feedback);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = feedback.Id }, feedback);
        }

        // PUT 
        [HttpPut("{id}")]
        public IActionResult Put(int id, Feedback updatedFeedback)
        {
            var existingFeedback = _db.Feedbacks.FirstOrDefault(f => f.Id == id);
            if (existingFeedback == null)
            {
                return NotFound();
            }

            existingFeedback.Rating = updatedFeedback.Rating;
            existingFeedback.Comment = updatedFeedback.Comment;
            existingFeedback.CommentDate = updatedFeedback.CommentDate;
            existingFeedback.ProductId = updatedFeedback.ProductId;
            existingFeedback.UserId = updatedFeedback.UserId;

            _db.SaveChanges();

            return NoContent();
        }
    }
}