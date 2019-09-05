using BonusRacing.Api.Helpers;
using BonusRacing.Api.Models.GameData;
using BonusRacing.DataAccess.Services;
using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Globalization;

namespace BonusRacing.Api.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("gameData")]
    public class GameDataController : ApiController
    {
        private readonly GameDataService _gameDataService;
        private const string hashSecretKey = "6a204bd89f3c";

        public GameDataController(GameDataService gameDataService)
        {
            _gameDataService = gameDataService;
        }

        [HttpPost]
        [Route("noauth")]
        public async Task<IHttpActionResult> ReceivingGameData(GameDataReceivingModel model)
        {
            var gameData = MapGameData(model);
            await _gameDataService.ReceivingGameData(gameData);
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> ReceivingGameDataAuth(GameDataReceivingModel model)
        {
            if (!Request.Headers.Contains("hash"))
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            var hash = Request.Headers.GetValues("hash").First();
            var gameData = MapGameData(model);
            var hashFromModel = GetMd5HashString($"{model.session_id}_{model.idencard_number}_{model.earned_points.ToString(CultureInfo.InvariantCulture)}_{hashSecretKey}");
            if (hash != hashFromModel)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            await _gameDataService.ReceivingGameData(gameData);
            return Ok();
        }
        
        [HttpPost]
        [Route("echo")]
        public async Task<IHttpActionResult> ReceivingGameDataEcho(GameDataReceivingModel model)
        {
            StringBuilder message = new StringBuilder();
            var gameData = MapGameData(model);
            var hashFromModel = GetMd5HashString($"{model.session_id}_{model.idencard_number}_{model.earned_points}_{hashSecretKey}");
            message.AppendLine("Ожидаемый заголовок hash: " + hashFromModel);
            if (!Request.Headers.Contains("hash"))
            {
                message.AppendLine("Отсутcвует заголовок hash");
            }
            else
            {
                var hash = Request.Headers.GetValues("hash").First();
                message.AppendLine("Принятый заголовок hash: " + hash);
                if (hash == hashFromModel)
                {
                    message.AppendLine("Проверочный хэш правильный");
                }
                else
                {
                    message.AppendLine("Проверочный хэш не правильный");
                }
            }
            return Json(new { model = gameData, message = message.ToString()}, DefaultSerializerSettings.Instance);
        }

        [HttpOptions]
        [Route("echo")]
        public string OptionsEcho()
        {
            return null; // HTTP 200 response with empty body
        }

        [HttpOptions]
        [Route("")]
        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }

        private DecodedGameData MapGameData(GameDataReceivingModel model)
        {
            return new DecodedGameData
            {
                Id = Guid.NewGuid(),
                ReceivingDate = DateTime.UtcNow,
                SessionId = model.session_id,
                CardNumber = model.card_num,
                PlayerId = model.player_id,
                FirstName = model.first_name,
                LastName = model.last_name,
                MiddleName = model.middle_name,
                IdenCardId = model.idencard_number,
                MachId = model.mach_id,
                UpdateDate = DateTime.Parse(model.update_date),
                EarnedPoints = model.earned_points
            };
        }

        private string GetMd5HashString(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
