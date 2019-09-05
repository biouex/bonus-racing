using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess
{
    public class GameDataMapper
    {
        private readonly CryptoServiceProvider _cryptoServiceProvider;

        public GameDataMapper(CryptoServiceProvider cryptoServiceProvider)
        {
            _cryptoServiceProvider = cryptoServiceProvider;
        }

        public EncodedGameData Map(DecodedGameData decodedGameData)
        {
            return new EncodedGameData
            {
                Id = decodedGameData.Id,
                ReceivingDate = decodedGameData.ReceivingDate,
                SessionId = decodedGameData.SessionId,
                CardNumber = decodedGameData.CardNumber,
                PlayerId = decodedGameData.PlayerId,
                FirstName = _cryptoServiceProvider.Encrypt(decodedGameData.FirstName),
                LastName = _cryptoServiceProvider.Encrypt(decodedGameData.LastName),
                MiddleName = _cryptoServiceProvider.Encrypt(decodedGameData.MiddleName),
                IdenCardId = decodedGameData.IdenCardId,
                MachId = decodedGameData.MachId,
                UpdateDate = decodedGameData.UpdateDate,
                EarnedPoints = decodedGameData.EarnedPoints
            };
        }

        public DecodedGameData Map(EncodedGameData encodedGameData)
        {
            return new DecodedGameData
            {
                Id = encodedGameData.Id,
                ReceivingDate = encodedGameData.ReceivingDate,
                SessionId = encodedGameData.SessionId,
                CardNumber = encodedGameData.CardNumber,
                PlayerId = encodedGameData.PlayerId,
                FirstName = _cryptoServiceProvider.Decrypt(encodedGameData.FirstName),
                LastName = _cryptoServiceProvider.Decrypt(encodedGameData.LastName),
                MiddleName = _cryptoServiceProvider.Decrypt(encodedGameData.MiddleName),
                IdenCardId = encodedGameData.IdenCardId,
                MachId = encodedGameData.MachId,
                UpdateDate = encodedGameData.UpdateDate,
                EarnedPoints = encodedGameData.EarnedPoints
            };
        }
    }
}
