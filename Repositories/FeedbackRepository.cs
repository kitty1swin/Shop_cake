using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop_cake.Data.Models;

namespace Shop_cake.Data
{
    public class FeedbackRepository
    {
        private readonly AppDBContext _dbContext;

        public FeedbackRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return _dbContext.Feedbacks.ToList();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _dbContext.Feedbacks.Find(id);
        }

        public void AddFeedback(Feedback feedback)
        {
            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _dbContext.Entry(feedback).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteFeedback(int id)
        {
            var feedback = _dbContext.Feedbacks.Find(id);
            if (feedback != null)
            {
                _dbContext.Feedbacks.Remove(feedback);
                _dbContext.SaveChanges();
            }
        }
    }
}