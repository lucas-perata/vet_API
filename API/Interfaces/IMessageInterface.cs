using API.Dtos.Message;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageInterface
    {
        void AddMessage(Message message); 
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id); 
        Task<PagedList<MessageDto>> GetMessagesForUser(); 
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string recipientId); 
        Task<bool> SaveAllAsync();
    }
}