using Shop_cake.Data.Models;
using Shop_cake.Data.Repositories;

public class FeedbackService
{
    private readonly FeedbackRepository _feedbackRepository;

    public FeedbackService(FeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public IEnumerable<Feedback> GetAllFeedbacks()
    {
        return _feedbackRepository.GetAllFeedbacks();
    }

    public Feedback GetFeedbackById(int id)
    {
        return _feedbackRepository.GetFeedbackById(id);
    }

    public void CreateFeedback(int userId, int orderId, string comment, int rating)
    {
        var feedback = new Feedback
        {
            UserId = userId,
            OrderId = orderId,
            Comment = comment,
            CommentDate = DateTime.Now,
            Rating = rating
        };
        _feedbackRepository.AddFeedback(feedback);
    }

    public void EditFeedback(int feedbackId, string newComment, int newRating)
    {
        var feedback = _feedbackRepository.GetFeedbackById(feedbackId);
        if (feedback != null)
        {
            feedback.Comment = newComment;
            feedback.Rating = newRating;
            _feedbackRepository.UpdateFeedback(feedback);
        }
    }

    public void RemoveFeedback(int feedbackId)
    {
        _feedbackRepository.DeleteFeedback(feedbackId);
    }

    public IEnumerable<Feedback> GetFeedbacksForUser(int userId)
    {
        return _feedbackRepository.GetFeedbacksByUserId(userId);
    }
}