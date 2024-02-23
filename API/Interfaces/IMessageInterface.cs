using System.Text.RegularExpressions;
using API.Dtos.Message;
using API.Entities;
using API.Helpers;
using Group = API.Entities.Group;

namespace API.Interfaces
{
    public interface IMessageInterface
    {
        void AddMessage(Message message); 
        void DeleteMessage(Message message);
        void AddGroup(Group group);
        Task<Group> GetGroupConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<Message> GetMessage(int id); 
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams); 
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername); 
        Task<bool> SaveAllAsync();
        void RemoveConnection(Connection connection);
    }
}