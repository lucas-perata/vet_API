using System.Security.Claims;
using API.Dtos.Message;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager; 
        private readonly MessageRepository _messageRepository; 
        private readonly IMapper _mapper; 
        public MessageController(UserManager<AppUser> userManager, MessageRepository messageRepository, IMapper mapper)
        {
            _userManager = userManager; 
            _mapper = mapper; 
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 
            
            if (user.UserName == createMessageDto.RecipientUsername) return BadRequest("You cannot send a message to yourself");

            var sender = user.UserName;
            var senderId = user.Id;
            var recipient = await _userManager.FindByNameAsync(createMessageDto.RecipientUsername); 

            if(recipient is null) return NotFound(); 

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

            if(await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Failed to send"); 
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 
            
            messageParams.Username = user.UserName;

            var messages = await _messageRepository.GetMessagesForUser(messageParams); 

            Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages)); 

            return messages; 
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 

            var currentUsername = user.UserName; 

            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var message = await _messageRepository.GetMessage(id); 

            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user = await _userManager.FindByEmailAsync(email); 
            var username = user.UserName;

            if (message.SenderUsername != username && message.RecipientUsername != username) return Unauthorized();

            if(message.SenderUsername == username) message.SenderDeleted = true;

            if(message.RecipientUsername == username) message.RecipientDeleted = true; 

            if(message.SenderDeleted && message.RecipientDeleted)
            {
                _messageRepository.DeleteMessage(message); 
            }

            if(await _messageRepository.SaveAllAsync()) return Ok(); 

            return BadRequest("Problem deleting the message"); 
        }

        

    }
}