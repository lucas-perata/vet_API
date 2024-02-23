using System.Security.Claims;
using API.Dtos.Message;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Group = API.Entities.Group;

namespace API.SignalIR
{
    public class MessageHub : Hub
    { 
        private readonly MessageRepository _messageRepository;
        private readonly UserManager<AppUser> _userManager; 
        private readonly IMapper _mapper;
        public MessageHub(MessageRepository messageRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"];
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);

            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _messageRepository.GetMessageThread(Context.User.GetUsername(), otherUser);

            await Clients.Group(groupName).SendAsync("ReceivedMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var email = Context.User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 
            
            if (user.UserName == createMessageDto.RecipientUsername) throw new HubException("You cannot send messages to yourself");

            var sender = user.UserName;
            var senderId = user.Id;
            var recipient = await _userManager.FindByNameAsync(createMessageDto.RecipientUsername); 

            if(recipient is null) throw new HubException("Not found user");

            var message = new Message 
            {
                Sender = user,
                SenderId = senderId,
                SenderUsername = sender,
                Recipient = recipient,
                RecipientUsername = recipient.UserName,
                RecipientId = recipient.Id,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message); 

            if(await _messageRepository.SaveAllAsync()) 
            {
                var group = GetGroupName(sender, recipient.UserName);
                await Clients.Group(group).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));   
            }
        }

        private string GetGroupName(string caller, string other) 
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0; 
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

        private async Task<Group> AddToGroup(string groupName)
        {
            var email = Context.User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            var group = await _messageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, user.UserName);

            if(group is null)
            {
                group = new Group(groupName);
                _messageRepository.AddGroup(group);
            }

            group.Connections.Add(connection);

            if(await _messageRepository.SaveAllAsync()) return group;
            
            throw new HubException("Failed to add to group"); 
        }

        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _messageRepository.GetGroupConnection(Context.ConnectionId);
            var connection = await _messageRepository.GetConnection(Context.ConnectionId);
            _messageRepository.RemoveConnection(connection);
            
            if(await _messageRepository.SaveAllAsync()) return group; 

            throw new HubException("Failed to remove from group");
        }
    }
}