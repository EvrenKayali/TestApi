using MoneyBox.Business.DTO;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.ObjectMappers
{
    public class UserIdentifierMapper
    {
        public static UserIdentifierDTO ToDTO(UserIdentifier model)
        {
            return new UserIdentifierDTO
            {
                Id = model.Id,
                Code = model.Code,
                Time = model.CreateTime,
                Expire = model.Expire,
                UserId = model.UserId
            };
        }

        public static UserIdentifier ToModel(UserIdentifierDTO model)
        {
            return new UserIdentifier
            {
                Id = model.Id,
                Code = model.Code,
                CreateTime = model.Time,
                Expire = model.Expire,
                UserId = model.UserId
            };
        }

        public static List<UserIdentifierDTO> ToDTOList(List<UserIdentifier> list)
        {
            return list.Select(ToDTO).ToList();
        }

        public static List<UserIdentifier> ToModelList(List<UserIdentifierDTO> list)
        {
            return list.Select(ToModel).ToList();
        }
    }
}
