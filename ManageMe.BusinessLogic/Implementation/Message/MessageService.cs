using ManageMe.Common;
using ManageMe.Entities;
using ManageMe.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageMe.BusinessLogic
{
    public class MessageService : BaseService
    {
        private readonly object _algorithms;

        public MessageService(ServiceDependencies serviceDependencies, GeneralAlgorithm generalAlgorithm) : base(serviceDependencies)
        {
            _algorithms = generalAlgorithm;
        }

        public IEnumerable<IndexMessageVM>? GetMessages(int pageNumber, int pageSize, int channelId)
        {
            var model = UnitOfWork.Messages.Get()
                       .Where(m => m.ChannelId == channelId)
                       .Include(m => m.ChildrenMessages)
                       .Include(m => m.ParentMessage)
                       .Include(m => m.Author)
                       .Skip((pageNumber - 1) * pageSize)//.Take(pageSize)
                       .Select(m => Mapper.Map<IndexMessageVM>(m));

            return model.ToList();
        }

        public void Create(CreateMessageVM model)
        {
            ExecuteInTransaction(uow =>
            {
                var dbMessage = Mapper.Map<CreateMessageVM, Message>(model);

                dbMessage.Time = DateTime.Now;

                uow.Messages.Insert(dbMessage);
                uow.SaveChanges();
            });
        }

        public Message? GetMessageById(int? id)
        {
            return UnitOfWork.Messages?.Get().FirstOrDefault(e => e.Id == id);
        }

        public bool AddNewMessage(CreateMessageVM message, string authorId)
        {
            if (!UnitOfWork.Channels.Get().Any(c => c.Id == message.ChannelId && c.ChannelUsers.Any(cu => cu.UserId == authorId)))
            {
                return false;
            }

            if (message.ParentMessageId != null
                && !UnitOfWork.Messages.Get().Any(m => m.Id == message.ParentMessageId && m.ChannelId == message.ChannelId))
            {
                return false;
            }

            var dbMessage = Mapper.Map<Message>(message);
            dbMessage.AuthorId = authorId;
            dbMessage.Time = DateTime.Now;
            UnitOfWork.Messages.Insert(dbMessage);
            UnitOfWork.SaveChanges();

            return true;
        }

        public void UpdateMessage(Message message)
        {
            UnitOfWork.Messages.Update(message);
            UnitOfWork.SaveChanges();
        }

        public bool DeleteMessage(int messageId)
        {
            try
            {
                var message = UnitOfWork.Messages.Get().FirstOrDefault(m => m.Id == messageId);

                if (message == null)
                {
                    return false;
                }

                foreach (var childMessage in message.ChildrenMessages)
                {
                    DeleteMessage(childMessage.Id);
                }

                UnitOfWork.Messages.Delete(message);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UserIsAuthor(string? currentUserId, int? id)
        {
            var message = UnitOfWork.Messages.Get().FirstOrDefault(m => m.Id == id);

            if (message == null)
            {
                return false;
            }

            return message.AuthorId == currentUserId;
            
        }
    }
}
